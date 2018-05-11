import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-cocktails',
  templateUrl: './my-cocktails.component.html',
  styleUrls: ['./my-cocktails.component.scss']
})
export class MyCocktailsComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  navigate() {
    this.router.navigate(['/cocktail']);
  }

}
