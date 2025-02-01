import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { GoogleAuthServiceService } from '../_services/google-auth-service.service';
import { Member } from '../_modles/member';
import { MemberCardComponent } from '../member/member-card/member-card.component';
import { SocialAuthService } from '@abacritt/angularx-social-login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-data-with-google-auth',
  imports: [MemberCardComponent],
  templateUrl: './data-with-google-auth.component.html',
  styleUrl: './data-with-google-auth.component.css'
})
export class DataWithGoogleAuthComponent implements OnInit {
  userData: any;
    private http = inject(HttpClient);
    googleAuthService = inject(GoogleAuthServiceService);
    authService = inject(SocialAuthService);
    members: Member[] = [];
    route = inject(Router);
  ngOnInit(): void {}
  getUserWithGoogleAuthSSO() {

    this.googleAuthService.getMembersWithGoogleAuth().subscribe({
      next: members => this.members = members,
      error: error => console.log(error)
    })
  }
  signOut() {
    
    this.authService.signOut();
    localStorage.removeItem('google_token');
    console.log("success");
    this.route.navigate(['/google'])
    
  }
}
