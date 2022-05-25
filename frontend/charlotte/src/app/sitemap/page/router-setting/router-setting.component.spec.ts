import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RouterSettingComponent } from './router-setting.component';

describe('RouterSettingComponent', () => {
  let component: RouterSettingComponent;
  let fixture: ComponentFixture<RouterSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RouterSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RouterSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
