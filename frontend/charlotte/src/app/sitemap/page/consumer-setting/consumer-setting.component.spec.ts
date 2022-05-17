import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsumerSettingComponent } from './consumer-setting.component';

describe('ConsumerSettingComponent', () => {
  let component: ConsumerSettingComponent;
  let fixture: ComponentFixture<ConsumerSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsumerSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsumerSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
