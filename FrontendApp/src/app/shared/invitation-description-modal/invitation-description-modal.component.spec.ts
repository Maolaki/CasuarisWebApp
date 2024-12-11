import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitationDescriptionModalComponent } from './invitation-description-modal.component';

describe('InvitationDescriptionModalComponent', () => {
  let component: InvitationDescriptionModalComponent;
  let fixture: ComponentFixture<InvitationDescriptionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InvitationDescriptionModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvitationDescriptionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
