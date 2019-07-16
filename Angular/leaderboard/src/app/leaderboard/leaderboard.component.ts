import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent implements OnInit {

  datas: any[];
  datasFromApi: any;
  constructor(private httpClient: HttpClient) {
    this.getNewPictures();
  }

  ngOnInit() {
  }
  changePictures() {
    this.datas = [];
    this.datas.push(this.datasFromApi[this.getRandomInt(this.datasFromApi.length)]);
  }
  getNewPictures() {
    this.httpClient.get('https://hitw2019api.azurewebsites.net/api/pictures').subscribe(
      (data) => {
        this.datasFromApi = data;
        this.datas = [];
        this.changePictures();
        this.delay(5000).then(() => {
          this.getNewPictures();
        });
      }
    );
  }
  getRandomInt(max) {
    return Math.floor(Math.random() * Math.floor(max));
  }
  async delay(ms: number) {
    await new Promise( resolve => setTimeout(resolve, ms) );
}
}
