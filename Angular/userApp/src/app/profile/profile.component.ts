import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NotifierService } from 'angular-notifier';
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
    private httpClient: HttpClient,
    private notifier: NotifierService
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
        this.notifier.notify('error', 'You need to login you');
      }
    );
  }
  connect() {
    // tslint:disable-next-line:max-line-length
    this.httpClient.get('https://hitw2019api.azurewebsites.net/api/User?username=' + this.username + '&password=' + this.password).subscribe(
      (res: any) => {
        if (res == null) {
          this.notifier.notify('error', 'Username or password incorrect');
        } else {
          this.userLocal = res;
          console.log(res);
          localStorage.setItem('user', JSON.stringify(this.userLocal));
          this.connected = true;
          this.notifier.notify('success', 'succefully logged in');
        }
      },
      (err: any) => {
        this.notifier.notify('error', 'An error occured');
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
        if (res == null) {
          this.notifier.notify('error', 'an account already exsist with this username');
        } else {
          this.userLocal = res;
          localStorage.setItem('user', JSON.stringify(this.userLocal));
          this.connected = true;
          this.notifier.notify('success', 'succefully account created and logged in');
        }
      },
      (err: any) => {
        this.notifier.notify('error', 'An error occured');
      }
    );
  }
  logOut() {
    this.connected = false;
    localStorage.removeItem('user');
  }
}
