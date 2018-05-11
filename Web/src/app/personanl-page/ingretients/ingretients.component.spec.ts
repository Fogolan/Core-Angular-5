import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IngretientsComponent } from './ingretients.component';

describe('IngretientsComponent', () => {
  let component: IngretientsComponent;
  let fixture: ComponentFixture<IngretientsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IngretientsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IngretientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
