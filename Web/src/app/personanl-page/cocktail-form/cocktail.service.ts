import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cocktail } from '@app/personanl-page/cocktail-form/models/cocktail';

@Injectable()
export class CocktailService {
  constructor(private httpClient: HttpClient) {}

  async createCocktail(cocktail: Cocktail) {
    cocktail.amount = +cocktail.amount;
    cocktail.degrees = +cocktail.degrees;
    await this.httpClient.post<number>('/cocktail', { cocktailDto: cocktail }).toPromise();
  }
}
