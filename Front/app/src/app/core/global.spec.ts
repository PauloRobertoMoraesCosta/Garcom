import { TestBed, inject } from '@angular/core/testing';

import { Global } from './global';

describe('Global', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Global]
    });
  });

  it('should be created', inject([Global], (service: Global) => {
    expect(service).toBeTruthy();
  }));
});
