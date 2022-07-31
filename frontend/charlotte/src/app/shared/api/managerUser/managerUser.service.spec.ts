import { TestBed } from '@angular/core/testing';

import { ManagerUserService } from './managerUser.service';

describe('UserService', () => {
  let service: ManagerUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ManagerUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
