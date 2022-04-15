import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductTypeSettingComponent } from './product-type-setting.component';

describe('ProductTypeSettingComponent', () => {
  let component: ProductTypeSettingComponent;
  let fixture: ComponentFixture<ProductTypeSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductTypeSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductTypeSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
