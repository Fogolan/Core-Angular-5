import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cocktail } from '@app/personanl-page/cocktail-form/models/cocktail';

@Injectable()
export class CocktailService {
  constructor(private httpClient: HttpClient) {}

  async updateCocktail(cocktail: Cocktail) {
    cocktail.amount = +cocktail.amount;
    cocktail.degrees = +cocktail.degrees;
    if (!cocktail.id) {
      await this.httpClient.post<number>('/cocktail', { cocktailDto: cocktail }).toPromise();
    } else {
      await this.httpClient.put<number>('/cocktail', { cocktailDto: cocktail }).toPromise();
    }
  }

  async getById(id: number): Promise<Cocktail> {
    return await this.httpClient
      .get<Cocktail>(`/cocktail/${id}`)
      .toPromise();
  }

  async deleteById(id: number): Promise<number> {
    return await this.httpClient
      .delete<number>(`/cocktail/${id}`)
      .toPromise();
  }
}
