import { Component, OnInit } from '@angular/core';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-invite-description-modal',
  templateUrl: './invite-description-modal.component.html',
  styleUrls: ['./invite-description-modal.component.css', '../../../styles/modal.css']
})
export class InviteDescriptionModalComponent implements OnInit {
  private modalId = 'invite-description-modal';
  isVisible = false;
  description!: string;

  constructor(private modalService: ModalService) { }

  ngOnInit(): void {
    this.modalService.modalState$(this.modalId).subscribe(state => {
      this.isVisible = state.isVisible;
      this.description = state.data?.description || null;
    });
  }

  closeModal() {
    this.modalService.closeModal(this.modalId);
  }
}
