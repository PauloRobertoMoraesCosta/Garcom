import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsuarioManutencaoComponent } from './usuario.component';

describe('UsuarioManutencaoComponent', () => {
  let component: UsuarioManutencaoComponent;
  let fixture: ComponentFixture<UsuarioManutencaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsuarioManutencaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsuarioManutencaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
