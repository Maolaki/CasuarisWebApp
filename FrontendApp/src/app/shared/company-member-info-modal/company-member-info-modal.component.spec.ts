import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyMemberInfoModalComponent } from './company-member-info-modal.component';

describe('CompanyMemberInfoModalComponent', () => {
  let component: CompanyMemberInfoModalComponent;
  let fixture: ComponentFixture<CompanyMemberInfoModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CompanyMemberInfoModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompanyMemberInfoModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
