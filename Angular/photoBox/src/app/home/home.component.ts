import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild('video')
  public video;

  @ViewChild('canvas')
  public canvas;
  @ViewChild('imgPic')
  public imgPic;
  public num: number;
  public pictureTaked = false;
  public picture: string;
  constructor() { }

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
    if (this.num > 0) {
      this.delay(1000).then(
        () => {
          this.num = this.num - 1;
          this.takePicture();
        }
      );
    } else {
      this.num = null;
      this.pictureTaked = true;
      const ctx = this.canvas.nativeElement.getContext('2d');
      ctx.drawImage(this.video.nativeElement, 0, 0, this.video.nativeElement.width, this.video.nativeElement.height);
      this.canvas.nativeElement.toBlob((result) => {
        this.picture = URL.createObjectURL(result);
        console.log(this.picture);
        this.imgPic.nativeElement.addEventListener('load', () => URL.revokeObjectURL(this.picture));
        this.imgPic.nativeElement.src = this.picture;
      });
    }
  }
  public retryPicture() {
    this.num = 5;
    this.pictureTaked = false;
  }
  async delay(ms: number) {
    await new Promise( resolve => setTimeout(resolve, ms) );
}

}
