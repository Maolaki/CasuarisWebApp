import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentPage = '';

  constructor(private router: Router) {

    this.router.events.subscribe(() => {
      this.currentPage = this.router.url.split('/')[1] || 'home';
    });
  }
}
