using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EventOrganizer.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using InstagramJson;
namespace MIC.EventOrganizer
{
    public static class instagramFunction
    {
        [FunctionName("instagramFunction")]
        public async static Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            HttpClient httpClient = new HttpClient();
            var res = await httpClient.GetAsync("https://www.instagram.com/explore/tags/hitw2019/?__a=1");
            var resString = await res.Content.ReadAsStringAsync();
            var instaPost = JsonConvert.DeserializeObject<RootObject>(resString);
            log.LogInformation(resString);
            var pictures = new List<Picture>();
            foreach (var item in instaPost.graphql.hashtag.edge_hashtag_to_media.edges)
            {
                var pic = new Picture{
                    PictureUrl = item.node.display_url,
                    Removed = false,
                    IdFromPlat = Convert.ToInt64(item.node.id),
                    CreatedAt = DateTime.UtcNow
                };
                pictures.Add(pic);
            }
            var picturesForApi = JsonConvert.SerializeObject(pictures);
            log.LogInformation(picturesForApi);
            var content = new StringContent(picturesForApi, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("https://hitw2019api.azurewebsites.net/api/pictures/AddTable",content);
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
