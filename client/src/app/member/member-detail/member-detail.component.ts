import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Member } from '../../_modles/member';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-member-detail',
  imports: [],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit{
  
  memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  accountService = inject(AccountService);
  private router = inject(Router);
  member?:Member;
  isAdmin?:boolean;

  ngOnInit(): void {
    this.loadMember();
    if(this.accountService.currentUser()?.role === "admin")
      this.isAdmin = true;
  }


  loadMember(){
    const username = this.route.snapshot.paramMap.get('username');
    if(!username) return;
    this.memberService.getMember(username).subscribe({
      next: member => this.member = member
    })
  }

  deleteMember(id:number){
    this.memberService.deleteMember(id).subscribe({
      next : _ => {
        this.router.navigateByUrl('/members')
      },
       error : error => console.log(error)
    })
  }
}
