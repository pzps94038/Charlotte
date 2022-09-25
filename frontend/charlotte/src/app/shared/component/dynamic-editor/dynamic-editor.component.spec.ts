import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicEditorComponent } from './dynamic-editor.component';

describe('DynamicEditorComponent', () => {
  let component: DynamicEditorComponent;
  let fixture: ComponentFixture<DynamicEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DynamicEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
