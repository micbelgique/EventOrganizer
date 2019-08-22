import { TestBed } from '@angular/core/testing';

import { OngService } from './ong.service';

describe('OngService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OngService = TestBed.get(OngService);
    expect(service).toBeTruthy();
  });
});
