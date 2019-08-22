import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EndPointGetterService } from './end-point-getter.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OngService {

  constructor(
    private httpClient: HttpClient,
    private endPointGetter: EndPointGetterService
  ) { }
  public getAllOngs(): Observable<any> {
    return this.httpClient.get(this.endPointGetter.getEndPoint() + 'ong');
  }
}
