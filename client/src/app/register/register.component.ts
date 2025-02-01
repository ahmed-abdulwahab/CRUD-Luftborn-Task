import { Component, inject, Input, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  private accountService = inject(AccountService);
    private router = inject(Router);
  cancelRegister = output<boolean>();

  model: any = {}
    
    register(){
      this.accountService.register(this.model).subscribe({
        next : _ => {
          this.router.navigateByUrl('/members')
        },
         error : error => console.log(error)
      })
    }
    
}
