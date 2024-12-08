import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { TeamDTO } from '../../models/dtos/team.dto';
import { UnionService } from '../../services/api-services/union.service';
import { NavigationStateService } from '../../services/navigation-state.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrl: './teams.component.css'
})
export class TeamsComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;
  teams: TeamDTO[] = [];
  currentPage = 1;
  pageSize = 10;
  paginatedTeams: TeamDTO[] = [];

  constructor(
    private navigationService: NavigationStateService,
    private unionService: UnionService
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });

    this.updatePageSize();
    this.loadInvites();
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }

  loadInvites(): void {
    //const query = {
    //  username: null,
    //  pageNumber: this.currentPage,
    //  pageSize: this.pageSize
    //};

    //this.unionService.getTeams(query).subscribe((teams) => {
    //  this.teams = teams;
    //  this.paginate();
    //});

    this.teams = Array.from({ length: 50 }, (_, i) => ({
      id: i + 1,
      name: `Team ${i + 1}`,
      description: `Description for Team ${i + 1}`,
      members: this.getRandomMembers()
    }));

    this.paginate();
  }

  // Генератор случайных участников
  getRandomMembers(): string[] {
    const numberOfMembers = Math.floor(Math.random() * 5) + 1; // Число участников от 1 до 5
    const members = [];
    for (let i = 0; i < numberOfMembers; i++) {
      members.push(`Member ${Math.floor(Math.random() * 1000)}`); // Генерация случайного имени
    }
    return members;
  }

  // -----------------------------------------------------------

  paginate(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedTeams = this.teams.slice(startIndex, endIndex);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.paginate();
    this.loadInvites();
  }

  @HostListener('window:resize', ['$event'])
  onResize(): void {
    this.updatePageSize();
  }

  updatePageSize(): void {
    const width = window.innerWidth;
    if (width < 600) {
      this.pageSize = 10;
    } else if (width < 1200) {
      this.pageSize = 15;
    } else {
      this.pageSize = 20;
    }
    this.paginate();
  }
}
