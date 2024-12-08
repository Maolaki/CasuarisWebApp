import { Component, Input } from '@angular/core';
import { TeamDTO } from '../../models/dtos/team.dto';

@Component({
  selector: 'app-team-button',
  templateUrl: './team-button.component.html',
  styleUrl: './team-button.component.css'
})
export class TeamButtonComponent {
  @Input() team!: TeamDTO;

  onMembers() {
    console.log(`Memebers button clicked for Invite ID: ${this.team.id}`);
  }

  onLeave() {
    console.log(`Leave button clicked for Invite ID: ${this.team.id}`);
  }
}
