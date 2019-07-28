import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {
  teamName: string;
  private httpHeaders: HttpHeaders;
  user = JSON.parse(localStorage.getItem('user'));
  teams = [];
  private location: Location;
  private router: Router;
  constructor(
    private httpClient: HttpClient
  ) {
    this.getTeams();
  }

  ngOnInit() {
  }
  getTeamOfUser() {
    this.user.userTeam = this.teams.find(x => x.id === this.user.userTeam.id);
    localStorage.setItem('user', JSON.stringify(this.user));
  }
  getTeams() {
    this.httpClient.get('https://hitw2019api.azurewebsites.net/api/Teams').subscribe(
      (res: any[]) => {
        this.teams = res;
        if (this.user) {
          if (this.user.userTeam) {
            this.getTeamOfUser();
          } else {
            this.user.userTeam = null;
          }
        }
      }
    );
  }
  joinOrLeaveATeam(num: number) {
    this.httpHeaders = new HttpHeaders({
      Authorization: 'Bearer ' + this.user.token
    });
    const team = this.teams.find(x => x.id === num);
    // tslint:disable-next-line:max-line-length
    this.httpClient.put('https://hitw2019api.azurewebsites.net/api/teams/' + this.user.id, team, {headers: this.httpHeaders}).subscribe(
      (res) => {
          this.updateUser();
      }
    );
  }
  updateUser() {
    this.httpHeaders = new HttpHeaders({
      Authorization: 'Bearer ' + this.user.token
    });
    this.httpClient.get('https://hitw2019api.azurewebsites.net/api/user/getall', {headers: this.httpHeaders}).subscribe(
      (res: any[]) => {
        const token = this.user.token;
        this.user = res.find(x => x.id === this.user.id);
        this.user.token = token;
        localStorage.setItem('user', JSON.stringify(this.user));
        this.getTeams();
      }
    );
  }
  createTeam() {
    const t = {
      name: this.teamName
    };
    this.httpHeaders = new HttpHeaders({
      Authorization: 'Bearer ' + this.user.token
    });
    this.httpClient.post('https://hitw2019api.azurewebsites.net/api/teams', t, {headers: this.httpHeaders}).subscribe(
      (res: any) => {
        this.teamName = null;
        this.teams.push(res);
        this.joinOrLeaveATeam(res.id);
      });
  }
}
