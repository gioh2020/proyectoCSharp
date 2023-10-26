import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectServiceService {
  suscription: any;

  constructor(private http: HttpClient) { }

  public async connectApiPost(controllerMethod: string, body: any, callback: any, showError: boolean = false) {
    let endpoint = "https://localhost:7067/api/";

    
    this.http.post<any>(endpoint + controllerMethod, body)
      .subscribe(res => {
        callback(res);
      },
        err => {
          callback(err);
          
        }
      );
  }
  public async connectApiGet(controllerMethod: string, callback: any, showError: boolean = false) {
    let endpoint = "https://localhost:7067/api/";

    
    this.suscription = this.http.get<any>(endpoint + controllerMethod, { observe: 'response'})
      .subscribe(res => {
        callback(res);
      },
        err => {
          callback(err);
        });

  }
}
