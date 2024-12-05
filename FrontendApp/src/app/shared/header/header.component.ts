import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  toggleNavigation() {
    const nav = document.querySelector('.navigation');
    nav?.classList.toggle('open');
  }
}
