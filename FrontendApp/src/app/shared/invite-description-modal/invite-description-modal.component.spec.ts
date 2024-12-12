import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InviteDescriptionModalComponent } from './invite-description-modal.component';

describe('InviteDescriptionModalComponent', () => {
  let component: InviteDescriptionModalComponent;
  let fixture: ComponentFixture<InviteDescriptionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InviteDescriptionModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InviteDescriptionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
