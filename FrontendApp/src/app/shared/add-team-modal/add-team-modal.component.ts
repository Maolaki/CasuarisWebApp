import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UnionService } from '../../services/api-services/union.service';

@Component({
  selector: 'app-add-team-modal',
  templateUrl: './add-team-modal.component.html',
  styleUrls: ['./add-team-modal.component.css']
})
export class AddTeamModalComponent {
  @Input() isVisible = false;
  @Output() close = new EventEmitter<void>();
  team = { name: '', description: '' };

  constructor(private unionService: UnionService) { }

  addTeam(): void {
    const command = {
      username: localStorage.getItem('username'),
      companyId: parseInt(localStorage.getItem('companyId') || '', 10) || null,
      name: this.team.name,
      description: this.team.description
    };

    this.unionService.addTeam(command).subscribe(() => {
      alert('Команда успешно добавлена');
      this.closeModal();
    }, error => {
      alert('Ошибка при добавлении команды: ' + error.message);
    });
  }

  closeModal(): void {
    this.isVisible = false;
    this.close.emit();
  }
}
