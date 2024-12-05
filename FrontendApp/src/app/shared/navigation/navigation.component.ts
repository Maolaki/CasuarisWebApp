import { Component } from '@angular/core';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
})
export class NavigationComponent {
  closeNavigation() {
    const nav = document.querySelector('.navigation');
    nav?.classList.remove('open');
  }
}
