import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  public userLocal: any;
  username: string;
  password: string;
  connected: boolean;
  private httpHeader: HttpHeaders;
  constructor(
    private httpClient: HttpClient
  ) {
    if (!localStorage.getItem('user')) {
      this.connected = false;
    } else {
      this.userLocal = JSON.parse(localStorage.getItem('user'));
      this.updateLocalUser();
    }
  }

  ngOnInit() {
  }
  updateLocalUser() {
    this.httpHeader = new HttpHeaders({
      Authorization: 'Bearer ' + this.userLocal.token
    });
    this.httpClient.get('https://hitw2019api.azurewebsites.net/api/User/GetAll', {headers: this.httpHeader}).subscribe(
      (res: any[]) => {
        const token = this.userLocal.token;
        this.userLocal = res.find(x => x.username = this.userLocal.username);
        this.userLocal.token = token;
        localStorage.setItem('user', JSON.stringify(this.userLocal));
        this.connected = true;
      },
      (err) => {
        this.userLocal = null;
        this.connected = false;
        localStorage.removeItem('user');
      }
    );
  }
  connect() {
    // tslint:disable-next-line:max-line-length
    this.httpClient.get('https://hitw2019api.azurewebsites.net/api/User?username=' + this.username + '&password=' + this.password).subscribe(
      (res: any) => {
        this.userLocal = res;
        localStorage.setItem('user', JSON.stringify(this.userLocal));
        this.connected = true;
      }
    );
  }
  register() {
    const user = {
      username: this.username,
      password: this.password
    };
    this.httpClient.post('https://hitw2019api.azurewebsites.net/api/User', user).subscribe(
      (res: any) => {
        this.userLocal = res;
        localStorage.setItem('user', JSON.stringify(this.userLocal));
        this.connected = true;
      }
    );
  }
}
