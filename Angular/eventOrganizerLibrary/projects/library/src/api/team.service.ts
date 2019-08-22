import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EndPointGetterService } from './end-point-getter.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  constructor(
    private httpClient: HttpClient,
    private endPointGetter: EndPointGetterService
  ) { }
  public getAllTeams(): Observable<any> {
    return this.httpClient.get(this.endPointGetter.getEndPoint() + 'teams');
  }
  public createTeam(team: any): Observable<any> {
    const httpHeaders = new HttpHeaders(
      {
        Authorization: 'Bearer ' + this.endPointGetter.getBearerToken()
      }
    );
    return this.httpClient.post(this.endPointGetter.getEndPoint() + 'teams', team, {headers: httpHeaders});
  }
  public updateTeam(userId: number, team: any): Observable<any> {
    const httpHeaders = new HttpHeaders(
      {
        Authorization: 'Bearer ' + this.endPointGetter.getBearerToken()
      }
    );
    return this.httpClient.put(this.endPointGetter.getEndPoint() + 'teams/' + userId, team, {headers: httpHeaders});
  }
}
