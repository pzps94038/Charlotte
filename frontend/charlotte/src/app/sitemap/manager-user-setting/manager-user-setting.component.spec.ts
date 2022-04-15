import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerUserSettingComponent } from './manager-user-setting.component';

describe('ManagerUserSettingComponent', () => {
  let component: ManagerUserSettingComponent;
  let fixture: ComponentFixture<ManagerUserSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManagerUserSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagerUserSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
