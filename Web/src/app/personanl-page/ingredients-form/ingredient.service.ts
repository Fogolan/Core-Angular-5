import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Ingredient } from './models/ingredient';

@Injectable()
export class IngredientService {
  constructor(private httpClient: HttpClient) {}

  async updateIngredient(ingredient: Ingredient) {
    ingredient.degrees = +ingredient.degrees;
    if (ingredient.id === 0) {
        await this.httpClient.post<number>('/Ingredient', ingredient).toPromise();
    } else {
      await this.httpClient.put<number>(`/ingredient/${ingredient.id}`, ingredient).toPromise();
    }
  }

  async getById(id: number): Promise<Ingredient> {
    return await this.httpClient
      .get<Ingredient>(`/ingredient/${id}`)
      .toPromise();
  }

  async deleteById(id: number): Promise<number> {
    return await this.httpClient
      .delete<number>(`/ingredient/${id}`)
      .toPromise();
  }
}
