import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasicDataSettingComponent } from './basic-data-setting.component';

describe('BasicDataSettingComponent', () => {
  let component: BasicDataSettingComponent;
  let fixture: ComponentFixture<BasicDataSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasicDataSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasicDataSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
