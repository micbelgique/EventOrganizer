using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TwitterJson;
namespace MIC.EventOrganizer
{
    public static class twitterFunction
    {
        [FunctionName("twitterFunction")]
        public async static Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            HttpClient httpClient = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("CE7JavcbgQzbtGZqLcrYv8I2J:bzf6MQsdvBzRdBYxRuYoWhgVgYA3k5BvrS9fWBPUFK4Lf1Pmhi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",Convert.ToBase64String(byteArray));
            var res = await httpClient.PostAsync("https://api.twitter.com/oauth2/token?grant_type=client_credentials",null);
            var resString = await res.Content.ReadAsStringAsync();
            log.LogInformation(resString);
            var token = JsonConvert.DeserializeObject<BearerToken>(resString);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token.Access_token);
            var tweets = await httpClient.GetAsync("https://api.twitter.com/1.1/search/tweets.json?q=%23hitw2019");
            var tweetsString = await tweets.Content.ReadAsStringAsync();
            var tweetsData = JsonConvert.DeserializeObject<RootObject>(tweetsString);
            List<Picture> pictures = new List<Picture>(tweetsData.statuses.Count);
            foreach (var item in tweetsData.statuses)
            {
                Picture newPic = new Picture{
                    IdFromPlat = item.id,
                    Text = item.text,
                    PictureUrl = item.entities?.media?.FirstOrDefault(e=> e.type == "photo")?.media_url,
                    CreatedAt = DateTime.UtcNow,
                    Removed = false,
                    User = new UserPicture{
                        IdFromPlat = item.user.id,
                        Username = item.user.screen_name,
                        Name = item.user.name,
                        UserProfilePictureUrl = item.user.profile_image_url
                    }
                };
                pictures.Add(newPic);
            }
            httpClient.DefaultRequestHeaders.Authorization = null;
            log.LogInformation(Convert.ToString(pictures.Count));
            var picturesForApi = JsonConvert.SerializeObject(pictures);
            log.LogInformation(picturesForApi);
            var content = new StringContent(picturesForApi, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("https://hitw2019api.azurewebsites.net/api/pictures/AddTable",content);
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
