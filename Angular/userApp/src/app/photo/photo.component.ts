import { Component, OnInit } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { NotifierService } from 'angular-notifier';
@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit {
  private httpheaders: HttpHeaders;
  public picturesApi = [];
  public username = localStorage.getItem('username') === null ? 'Obiwan Kenoby': localStorage.getItem('username');
  public loading: boolean;
  public load = false;
  constructor(
    private httpClient: HttpClient,
    private notifier: NotifierService
  ) { }

  ngOnInit() {
  }
  SendPicture(event: any) {
    if (event.target.files && event.target.files.length) {
      const files = event.target.files;
      if (this.username.length > 0) {
        this.sendPictureToBlob(files);
        this.load = true;
      }
    }
  }
  sendPictureToBlob(pictures: any[]) {
    // tslint:disable-next-line:prefer-for-of
    for (let index = 0; index < pictures.length; index++) {
      const element = pictures[index];
      this.httpheaders = new HttpHeaders({
        'x-ms-blob-type': 'BlockBlob',
        'Content-type' : element.type,
        'x-ms-date': Date.now() + '',
      });
      const elementName = element.lastModified + element.size;
      localStorage.setItem('username', this.username);
      // tslint:disable-next-line:max-line-length
      this.httpClient.put('https://hitw2019blob.blob.core.windows.net/hitw2019/' + elementName + '?sv=2018-03-28&ss=b&srt=sco&sp=rwdlac&se=2019-08-30T22:28:34Z&st=2019-07-16T14:28:34Z&sip=1.1.1.1-255.255.255.254&spr=https,http&sig=5A23dxWWJZql%2FIOnyqhPCUkJ5Agjhu9mUHQYL7C1Q%2Fw%3D', element, {headers: this.httpheaders})
        .subscribe(
          (res: any) => {
            const picture = {
              idFromPlat: elementName,
              pictureUrl: 'https://hitw2019blob.blob.core.windows.net/hitw2019/' + elementName,
              removed: false,
              user: {
                username: this.username,
                name: this.username,
              }
            };
            this.picturesApi.push(picture);
            if (index === pictures.length - 1) {
              this.loading = false;
              this.load = false;
            }
          },(err: any) => {
            this.notifier.notify('error', err);
            this.loading = false;
            this.load = false;
          }
        );
    }
  }
  sendPictureToApi() {
    this.load = true;
    this.loading = true;
    this.httpClient.post('https://hitw2019api.azurewebsites.net/api/pictures/AddTable', this.picturesApi).subscribe(
          (data) => {
            this.notifier.notify('success', 'your pictures had been added');
            this.picturesApi = [];
            this.load = false;
            this.loading = false;
          });
  }
}
