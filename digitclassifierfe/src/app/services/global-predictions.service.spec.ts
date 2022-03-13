import { TestBed } from '@angular/core/testing';

import { GlobalPredictionsService } from './global-predictions.service';

describe('GlobalPredictionsService', () => {
  let service: GlobalPredictionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalPredictionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
