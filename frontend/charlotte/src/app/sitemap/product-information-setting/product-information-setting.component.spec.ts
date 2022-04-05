import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductInformationSettingComponent } from './product-information-setting.component';

describe('ProductInformationSettingComponent', () => {
  let component: ProductInformationSettingComponent;
  let fixture: ComponentFixture<ProductInformationSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductInformationSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductInformationSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
