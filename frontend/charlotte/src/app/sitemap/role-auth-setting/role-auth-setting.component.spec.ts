import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleAuthSettingComponent } from './role-auth-setting.component';

describe('RoleAuthSettingComponent', () => {
  let component: RoleAuthSettingComponent;
  let fixture: ComponentFixture<RoleAuthSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoleAuthSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleAuthSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
