import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_modles/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgForm, FormsModule } from '@angular/forms';
@Component({
  selector: 'app-member-edit',
  imports: [TabsModule,FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {
  accountService = inject(AccountService);
  memberService = inject(MembersService);
  member?:Member;
  @ViewChild('editForm') editForm: NgForm | undefined;

  ngOnInit(): void {
    this.loadMember();
  }


  loadMember(){
    const user = this.accountService.currentUser();
    if(!user) return;
    this.memberService.getMember(user?.username).subscribe({
      next: member => this.member = member
    })
  }

  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: _ => {
        this.editForm?.reset(this.member);
      }
    })
  }

}
