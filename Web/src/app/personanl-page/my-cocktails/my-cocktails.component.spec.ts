import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyCocktailsComponent } from './my-cocktails.component';

describe('MyCocktailsComponent', () => {
  let component: MyCocktailsComponent;
  let fixture: ComponentFixture<MyCocktailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyCocktailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyCocktailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
