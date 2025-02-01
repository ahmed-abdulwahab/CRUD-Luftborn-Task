import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocialAuthService, GoogleSigninButtonModule, SocialUser } from '@abacritt/angularx-social-login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-google-auth',
  imports: [CommonModule,
    GoogleSigninButtonModule],
  templateUrl: './google-auth.component.html',
  styleUrl: './google-auth.component.css'
})
export class GoogleAuthComponent implements OnInit{
  user: SocialUser | null = null;
  constructor( private authService:SocialAuthService,private router: Router) {}

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      console.log(user)
      this.user = user;
      console.log(user.idToken);
      localStorage.setItem('google_token', user.idToken);
      
      
      this.router.navigate(['/google/data']);
    });
  }
  signOut() {
    this.authService.signOut();
    localStorage.removeItem('google_token');
  }
}
