import { TestBed } from '@angular/core/testing';

import { DashbordService } from './dashbord.service';

describe('DashbordService', () => {
  let service: DashbordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DashbordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
