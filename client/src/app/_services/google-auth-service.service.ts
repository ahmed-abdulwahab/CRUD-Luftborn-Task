import { inject, Injectable } from '@angular/core';
import { Member } from '../_modles/member';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GoogleAuthServiceService {
  
  private http = inject(HttpClient);
    baseUrl = environment.apiUrl;
    
  
    getMembersWithGoogleAuth(){
      const token = localStorage.getItem('google_token');
      console.log(this.baseUrl);
      console.log(token);
      const headers = new HttpHeaders({
            'Authorization': `Bearer ${token}`
      });
      return this.http.get<Member[]>(this.baseUrl + 'googleauth/googleauth',{headers});
    }
  
    
}
