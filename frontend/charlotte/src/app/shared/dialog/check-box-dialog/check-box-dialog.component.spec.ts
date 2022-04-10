import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckBoxDialogComponent } from './check-box-dialog.component';

describe('CheckBoxDialogComponent', () => {
  let component: CheckBoxDialogComponent;
  let fixture: ComponentFixture<CheckBoxDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CheckBoxDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckBoxDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
