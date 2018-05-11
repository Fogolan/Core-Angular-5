import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyIngredientsComponent } from './my-ingredients.component';

describe('MyIngredientsComponent', () => {
  let component: MyIngredientsComponent;
  let fixture: ComponentFixture<MyIngredientsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyIngredientsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyIngredientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
