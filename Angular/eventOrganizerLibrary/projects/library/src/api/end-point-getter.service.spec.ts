import { TestBed } from '@angular/core/testing';

import { EndPointGetterService } from './end-point-getter.service';

describe('EndPointGetterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EndPointGetterService = TestBed.get(EndPointGetterService);
    expect(service).toBeTruthy();
  });
});
