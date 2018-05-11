import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CocktailItem } from '@app/personanl-page/cocktails/models/cocktailItem';

@Component({
  selector: 'app-cocktails',
  templateUrl: './cocktails.component.html',
  styleUrls: ['./cocktails.component.scss']
})
export class CocktailsComponent implements OnInit {
  @Input() readonly: boolean;
  @Input() url: string;

  cocktails: CocktailItem[] = [];

  constructor(private httpClient: HttpClient, private router: Router) {}

  async ngOnInit() {
    if (this.url) {
      this.cocktails = await this.getIngredients(this.url);
    }
  }

  openCocktail(id: number) {
    console.log(this.readonly);
    if (id) {
      if (!this.readonly === false) {
        this.router.navigate([`/cocktail/${id}`], { replaceUrl: true });
      } else {
        this.router.navigate([`/cocktailview/${id}`], { replaceUrl: true });
      }
    }
  }

  async getIngredients(url: string): Promise<CocktailItem[]> {
    return await this.httpClient.get<CocktailItem[]>(url).toPromise();
  }
}
