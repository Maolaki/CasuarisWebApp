import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamDescriptionModalComponent } from './team-description-modal.component';

describe('TeamDescriptionModalComponent', () => {
  let component: TeamDescriptionModalComponent;
  let fixture: ComponentFixture<TeamDescriptionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TeamDescriptionModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TeamDescriptionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
