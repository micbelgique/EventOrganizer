import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EndPointGetterService } from './end-point-getter.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PictureService {

  constructor(
    private httpClient: HttpClient,
    private endPointGetter: EndPointGetterService
  ) { }
  public getAllPictures(): Observable<any> {
    return this.httpClient.get(this.endPointGetter.getEndPoint() + 'pictures');
  }
  public getAPicture(id: number): Observable<any> {
    return this.httpClient.get(this.endPointGetter.getEndPoint() + 'pictures/' + id);
  }
  public addTableOfPictures(pictures: any[]): Observable<any> {
    return this.httpClient.post(this.endPointGetter.getEndPoint() + 'pictures/AddTable', pictures);
  }
  public addPicture(picture: any): Observable<any> {
    return this.httpClient.post(this.endPointGetter.getEndPoint() + 'pictures/AddPicture', picture);
  }
  public banPicture(id: number): Observable<any> {
    return this.httpClient.put(this.endPointGetter.getEndPoint() + 'pictures/' + id, null);
  }
}
