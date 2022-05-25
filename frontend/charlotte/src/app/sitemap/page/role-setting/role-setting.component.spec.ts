import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleSettingComponent } from './role-setting.component';

describe('RoleSettingComponent', () => {
  let component: RoleSettingComponent;
  let fixture: ComponentFixture<RoleSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoleSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
