import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Picture } from './picture';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  datasFromApi: Picture[];
  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.getPictures();
  }
  getPictures() {
    this.httpClient.get('https://hitw2019api.azurewebsites.net/api/pictures').subscribe(
      (data: Picture[]) => {
        this.datasFromApi = data;
        console.log(this.datasFromApi);
      }
    );
  }
  banAPicture(id: number) {
    this.httpClient.put('https://hitw2019api.azurewebsites.net/api/pictures/' + id, null).subscribe(
      (data) => {
        console.log(data);
      }
    );
  }
}
