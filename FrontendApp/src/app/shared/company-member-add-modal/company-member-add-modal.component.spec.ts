import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyMemberAddModalComponent } from './company-member-add-modal.component';

describe('CompanyMemberAddModalComponent', () => {
  let component: CompanyMemberAddModalComponent;
  let fixture: ComponentFixture<CompanyMemberAddModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CompanyMemberAddModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompanyMemberAddModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
