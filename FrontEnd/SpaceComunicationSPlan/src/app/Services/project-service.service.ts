import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectServiceService {

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
}
