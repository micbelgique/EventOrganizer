import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EndPointGetterService } from './end-point-getter.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TimeTableOngService {

  constructor(
    private httpClient: HttpClient,
    private endPointGetter: EndPointGetterService
  ) { }
  public updateTimetableOng(timeTableOng: any): Observable<any> {
    const httpHeaders = new HttpHeaders(
      {
        Authorization: 'Bearer ' + this.endPointGetter.getBearerToken()
      }
    );
    return this.httpClient.put(this.endPointGetter.getEndPoint() + 'TimeTableOngs', timeTableOng, {headers: httpHeaders});
  }
}
