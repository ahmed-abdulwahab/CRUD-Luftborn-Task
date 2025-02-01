import { Component, inject} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  private router = inject(Router);
  registerMode = false;
  users : any;

  

  registerToggle(){
    this.router.navigateByUrl('/register');
  }
  cancelRegisterMode(event:boolean){
    this.registerMode = event;
  }
  
}
