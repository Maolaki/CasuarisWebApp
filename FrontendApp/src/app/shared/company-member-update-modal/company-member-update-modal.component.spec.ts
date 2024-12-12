import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyMemberUpdateModalComponent } from './company-member-update-modal.component';

describe('CompanyMemberUpdateModalComponent', () => {
  let component: CompanyMemberUpdateModalComponent;
  let fixture: ComponentFixture<CompanyMemberUpdateModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CompanyMemberUpdateModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompanyMemberUpdateModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
