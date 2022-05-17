import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FactorySettingComponent } from './factory-setting.component';

describe('FactorySettingComponent', () => {
  let component: FactorySettingComponent;
  let fixture: ComponentFixture<FactorySettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FactorySettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FactorySettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
