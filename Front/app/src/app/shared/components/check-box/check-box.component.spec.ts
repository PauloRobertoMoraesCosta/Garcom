import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckBox } from './check-box.component';

describe('CheckBox', () => {
  let component: CheckBox;
  let fixture: ComponentFixture<CheckBox>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CheckBox ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckBox);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
