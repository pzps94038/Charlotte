import { TestBed } from '@angular/core/testing';

import { ProductTypeService } from './product-type.service';

describe('ProductTypeService', () => {
  let service: ProductTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
