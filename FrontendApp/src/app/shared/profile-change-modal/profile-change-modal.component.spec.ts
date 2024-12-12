import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileChangeModalComponent } from './profile-change-modal.component';

describe('ProfileChangeModalComponent', () => {
  let component: ProfileChangeModalComponent;
  let fixture: ComponentFixture<ProfileChangeModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProfileChangeModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileChangeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
