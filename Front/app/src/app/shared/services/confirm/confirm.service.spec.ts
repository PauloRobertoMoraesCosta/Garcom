import { TestBed, inject } from '@angular/core/testing';

import { Confirm } from './confirm.service';

describe('Confirm', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Confirm]
    });
  });

  it('should be created', inject([Confirm], (service: Confirm) => {
    expect(service).toBeTruthy();
  }));
});
