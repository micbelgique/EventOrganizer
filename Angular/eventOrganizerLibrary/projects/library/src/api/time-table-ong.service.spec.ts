import { TestBed } from '@angular/core/testing';

import { TimeTableOngService } from './time-table-ong.service';

describe('TimeTableOngService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TimeTableOngService = TestBed.get(TimeTableOngService);
    expect(service).toBeTruthy();
  });
});
