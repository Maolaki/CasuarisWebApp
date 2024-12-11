import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddInvitationModalComponent } from './add-invitation-modal.component';

describe('AddInvitationModalComponent', () => {
  let component: AddInvitationModalComponent;
  let fixture: ComponentFixture<AddInvitationModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddInvitationModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddInvitationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
