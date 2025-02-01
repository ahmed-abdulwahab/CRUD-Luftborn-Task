import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_modles/member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-member-list',
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  private memberService = inject(MembersService);
  sortBy: string = 'score';
  members: Member[] = [];
  ngOnInit(): void {
    this.loadMembers();
  }
  loadMembers() {
    this.memberService.getMembers(this.sortBy).subscribe({
      next: (members) => (this.members = members),
    });
  }
  onScoreUpdated() {
    this.loadMembers();
  }
  setSortBy(sortBy: string) {
    this.sortBy = sortBy;
    this.loadMembers(); 
  }
  

}
