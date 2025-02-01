import { Component, EventEmitter, inject, input, Output } from '@angular/core';
import { Member } from '../../_modles/member';
import { RouterLink } from '@angular/router';
import { MembersService } from '../../_services/members.service';

@Component({
  selector: 'app-member-card',
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
export class MemberCardComponent {
  member = input.required<Member>();
  memberService = inject(MembersService);
  @Output() scoreUpdated = new EventEmitter<void>(); 
  increaseScore() {
    const newScore = this.member().score + 1; 
    this.memberService.updateScore(this.member().id, newScore).subscribe({
      next: () => {
        this.scoreUpdated.emit();
      },
      error: (err) => console.error("Error updating score", err)
    });
  }
}
