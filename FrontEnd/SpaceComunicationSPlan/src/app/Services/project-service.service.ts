import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectServiceService {
  suscription: any;
  private endpoint: string =  "https://www.galaxymessage.somee.com/api/";

  constructor(private http: HttpClient) { }

  public async connectApiPost(controllerMethod: string, body: any, callback: any, showError: boolean = false) {
    
    this.http.post<any>(this.endpoint + controllerMethod, body)
      .subscribe(res => {
        callback(res);
      },
        err => {
          callback(err);
          
        }
      );
  }
  public async connectApiGet(controllerMethod: string, callback: any, showError: boolean = false) {
    this.suscription = this.http.get<any>(this.endpoint + controllerMethod, { observe: 'response'})
      .subscribe(res => {
        callback(res);
      },
        err => {
          callback(err);
        });

  }
}
