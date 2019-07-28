import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NotifierService } from 'angular-notifier';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-ong',
  templateUrl: './ong.component.html',
  styleUrls: ['./ong.component.css']
})
export class OngComponent implements OnInit {
  user = JSON.parse(localStorage.getItem('user'));
  ongs = [];
  private httpHeaders: HttpHeaders;
  constructor(
    private httpClient: HttpClient,
    private notifier: NotifierService,
    private router: Router,
    private location: Location
  ) { }

  ngOnInit() {
    this.getOngs();
  }
  getOngs() {
    this.httpClient.get('https://hitw2019api.azurewebsites.net/api/ong').subscribe(
      (res: any[]) => {
        this.ongs = res;
      }
    );
  }
  takeThisTime(timetable: number, ong: number) {
    this.httpHeaders = new HttpHeaders({
      Authorization: 'Bearer ' + this.user.token
    });
    if (!this.ongs.find(x => x.id === ong).timeTables.find(w =>w.selectedTeam && w.selectedTeam.id === this.user.userTeam.id)) {
      const tt = this.ongs.find(x => x.id === ong).timeTables.find(w => w.id === timetable);
      tt.selectedTeam = this.user.userTeam;
      console.log(tt);
      this.httpClient.put('https://hitw2019api.azurewebsites.net/api/timetableongs', tt, {headers: this.httpHeaders}).subscribe(
        (res) => {
          if (res) {
            this.notifier.notify('success', 'succefully Booked');
          } else {
            this.notifier.notify('error', 'an error occured');
          }
          this.getOngs();
        }, (err) => {
          this.location.replaceState('/'); // clears browser history so they can't navigate with back button
          this.router.navigate(['profile']);
        }
      );
    }
  }
}
