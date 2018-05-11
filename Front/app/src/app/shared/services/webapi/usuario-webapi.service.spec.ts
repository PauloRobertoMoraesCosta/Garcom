import { TestBed, inject } from '@angular/core/testing';

import { UsuarioWebapiService } from './usuario-webapi.service';

describe('UsuarioWebapiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UsuarioWebapiService]
    });
  });

  it('should be created', inject([UsuarioWebapiService], (service: UsuarioWebapiService) => {
    expect(service).toBeTruthy();
  }));
});
