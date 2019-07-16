import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild('video', { static: true })
  public video;

  @ViewChild('canvas', { static: true })
  public canvas;
  @ViewChild('imgPic', { static: true })
  public imgPic;
  public num: number;
  public pictureTaked = false;
  public picture: string;
  public delays = false;
  private blob: Blob;
  private httpheaders;
  constructor(
    private httpClient: HttpClient
  ) { }

  ngOnInit() {
    this.num = 5;
  }
  // tslint:disable-next-line:use-life-cycle-interface
  public ngAfterViewInit() {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia({ video: true }).then(stream => {
            this.video.nativeElement.srcObject = stream;
            this.video.nativeElement.play();
        });
    }
  }
  public takePicture() {
    this.canvas.nativeElement.height = this.video.nativeElement.clientHeight;
    this.canvas.nativeElement.width = this.video.nativeElement.clientWidth;
    if (this.num > 0) {
      this.delay(1000).then(
        () => {
          this.num = this.num - 1;
          this.takePicture();
          this.delays = true;
        }
      );
    } else {
      this.delays = false;
      this.num = null;
      this.pictureTaked = true;
      const ctx = this.canvas.nativeElement.getContext('2d');
      // tslint:disable-next-line:max-line-length
      ctx.drawImage(this.video.nativeElement, 0, 0, this.video.nativeElement.clientWidth, this.video.nativeElement.clientHeight);
      this.canvas.nativeElement.toBlob((result: any) => {
        this.picture = (window.URL ? URL : webkitURL).createObjectURL(result);
        console.log(this.picture);
        this.imgPic.nativeElement.addEventListener('load', () => URL.revokeObjectURL(this.picture));
        this.imgPic.nativeElement.src = this.picture;
        this.imgPic.nativeElement.style.display = 'block';
        console.log(result);
        this.blob = result;
      });
    }
  }
  public retryPicture() {
    this.num = 5;
    this.pictureTaked = false;
    this.delays = false;
    this.imgPic.nativeElement.style.display = 'none';
  }
  public sendPicture() {
    // tslint:disable-next-line:one-variable-per-declaration
    this.httpheaders = new HttpHeaders({
      'x-ms-blob-type': 'BlockBlob',
      'Content-type' : this.blob.type,
      'x-ms-date': Date.now() + '',
    });
    const namePhoto = Date.now();
    // tslint:disable-next-line:max-line-length
    this.httpClient.put('https://hitw2019blob.blob.core.windows.net/hitw2019/' + namePhoto + '?sv=2018-03-28&ss=b&srt=sco&sp=rwdlac&se=2019-08-30T22:28:34Z&st=2019-07-16T14:28:34Z&sip=1.1.1.1-255.255.255.254&spr=https,http&sig=5A23dxWWJZql%2FIOnyqhPCUkJ5Agjhu9mUHQYL7C1Q%2Fw%3D', this.blob, {headers: this.httpheaders}).subscribe(
      (res) => {
        console.log(res);
        console.log('https://hitw2019blob.blob.core.windows.net/hitw2019/' + namePhoto);
        const picture = {
          idFromPlat: namePhoto,
          pictureUrl: 'https://hitw2019blob.blob.core.windows.net/hitw2019/' + namePhoto,
          removed: false,
          user: {
            username: 'PhotoBox',
            name: 'PhotoBox',
            userPictureUrl: 'https://hitw2019blob.blob.core.windows.net/hitw2019/' + namePhoto
          }
        };
        console.log(picture);
        this.httpClient.post('https://hitw2019api.azurewebsites.net/api/pictures/AddPicture', picture).subscribe(
          (data) => {
            console.log(data);
            this.retryPicture();
          }
        )
      }
    );

  }
  async delay(ms: number) {
    await new Promise( resolve => setTimeout(resolve, ms) );
}

}
