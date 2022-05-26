import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicSildeToggleComponent } from './dynamic-silde-toggle.component';

describe('DynamicSildeToggleComponent', () => {
  let component: DynamicSildeToggleComponent;
  let fixture: ComponentFixture<DynamicSildeToggleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DynamicSildeToggleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicSildeToggleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
