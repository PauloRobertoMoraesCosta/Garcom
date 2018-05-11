import { TestBed, inject } from '@angular/core/testing';

import { PerfilWebapiService } from './perfil-webapi.service';

describe('PerfilWebapiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PerfilWebapiService]
    });
  });

  it('should be created', inject([PerfilWebapiService], (service: PerfilWebapiService) => {
    expect(service).toBeTruthy();
  }));
});
