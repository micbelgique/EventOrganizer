import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EndPointGetterService {
  private link: string;
  private bearerToken: string;
  constructor() {
    this.link = 'https://hitw2019api.azurewebsites.net/api/';
   }
   public getEndPoint(): string {
     return this.link;
   }
   public getBearerToken(): string {
     return this.bearerToken;
   }
   public setBearerToken(newToken: string): void {
     this.bearerToken = newToken;
   }
}
