import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalService } from './services/modal-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  currentPage = '';
  isAuthorized = false;
  errorMessage: string | null = null;

  constructor(private router: Router, private modalService: ModalService) {
    this.router.events.subscribe(() => {
      this.currentPage = this.router.url.split('/')[1] || 'home';
      this.isAuthorized = !!localStorage.getItem('username');
    });
  }

  ngOnInit(): void {
    // Подписка на изменения в ModalService
    this.modalService.errorMessage$.subscribe((message) => {
      this.errorMessage = message;
    });
  }

  closeModal(): void {
    this.modalService.clearError();
  }
}
