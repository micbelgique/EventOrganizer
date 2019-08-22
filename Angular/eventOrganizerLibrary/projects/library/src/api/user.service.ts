import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EndPointGetterService } from './end-point-getter.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private httpClient: HttpClient,
    private endPointGetter: EndPointGetterService
  ) { }
  public connectUser(username: string, password: string): Observable<any> {
    return this.httpClient.get(this.endPointGetter.getEndPoint() + 'user?username=' + username + '&password=' + password);
  }
  public registerUser(user: any): Observable<any> {
    return this.httpClient.post(this.endPointGetter.getEndPoint() + '/user', user);
  }
  public getAllUsers(): Observable<any> {
    const httpHeaders = new HttpHeaders({
      Authorization: 'Bearer ' + this.endPointGetter.getBearerToken()
    });
    return this.httpClient.get(this.endPointGetter.getEndPoint() + 'user/getAll', {headers: httpHeaders});
  }
}
