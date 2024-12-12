import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CompanyRole } from '../../enums/company-role.enum';
import { UpdateCompanyMemberCommand } from '../../models/commands/unionservice/update-company-member.command';
import { UnionService } from '../../services/api-services/union.service';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-company-member-update-modal',
  templateUrl: './company-member-update-modal.component.html',
  styleUrls: ['./company-member-update-modal.component.css', '../../../styles/modal.css']
})
export class CompanyMemberUpdateModalComponent implements OnInit {
  private modalId = 'company-member-update-modal';
  isVisible = false;
  currentRole = CompanyRole;

  updateCommand: UpdateCompanyMemberCommand = {
    memberId: 0,
    username: null,
    companyId: null,
    salary: null,
    workHours: null,
    workDays: null
  };

  companyRole = CompanyRole;

  constructor(
    private modalService: ModalService,
    private unionService: UnionService
  ) { }

  ngOnInit(): void {
    this.modalService.modalState$(this.modalId).subscribe(state => {
      this.isVisible = state.isVisible;
      if (state.data) {
        this.updateCommand.memberId = state.data.memberId;
        this.updateCommand.salary = state.data.salary;
        this.updateCommand.workHours = state.data.workHours;
        this.updateCommand.workDays = state.data.workDays;
      }

      this.updateCommand.username = localStorage.getItem('username');
      this.updateCommand.companyId = Number(localStorage.getItem('companyId')) || null;
    });
  }

  closeModal(): void {
    this.modalService.closeModal(this.modalId);
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.unionService.updateCompanyMember(this.updateCommand).subscribe(
        () => {
          this.closeModal();
          location.reload();
        }
      );
    }
  }
}
