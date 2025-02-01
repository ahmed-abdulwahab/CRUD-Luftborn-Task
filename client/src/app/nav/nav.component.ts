import { Component, inject } from '@angular/core';
import {FormsModule} from '@angular/forms'
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-nav',
  imports: [FormsModule,BsDropdownModule,RouterLink,RouterLinkActive,TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  private router = inject(Router);
 accountServices = inject(AccountService);
  model : any = {};
  login(){
    this.accountServices.login(this.model).subscribe({
      next : _ => {
        this.router.navigateByUrl('/members')
      },
       error : error => console.log(error)
    });
  }
  Logout(){
    this.accountServices.Logout();
    this.router.navigateByUrl('/');
  }
}
