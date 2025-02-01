import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from "../_modles/member"

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  

  getMembers(sortBy: string = 'lastActive') {
    return this.http.get<Member[]>(`${this.baseUrl}users?sortBy=${sortBy}`);
  }
  updateScore(memberId: number, newScore: number) {
    return this.http.put(`${this.baseUrl}users/${memberId}/score`, { score: newScore });
  }
  getMember(username : string){
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }
  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member)
  }
  deleteMember(id: number) {
    return this.http.delete(`${this.baseUrl}users/${id}`);
  }
}
