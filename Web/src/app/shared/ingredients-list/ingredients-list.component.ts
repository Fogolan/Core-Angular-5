import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IngredientDto } from '@app/shared/ingredients-list/models/ingredientDto';
import { HttpClient } from '@angular/common/http';
import { some, remove } from 'lodash';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ingredients-list',
  templateUrl: './ingredients-list.component.html',
  styleUrls: ['./ingredients-list.component.scss']
})
export class IngredientsListComponent implements OnInit {
  @Input() url: string;
  @Input() readonly = true;
  @Output() selectedItemsChanges: EventEmitter<number[]> = new EventEmitter();

  ingredientsList: IngredientDto[];
  selectedItems: number[] = [];

  constructor(private httpClient: HttpClient, private router: Router) {}

  async ngOnInit() {
    if (this.url) {
      this.ingredientsList = await this.getIngredients(this.url);
    }
  }

  async getIngredients(url: string): Promise<IngredientDto[]> {
    return await this.httpClient.get<IngredientDto[]>(url).toPromise();
  }

  toggleSelection(id: number) {
    if (some(this.selectedItems, item => item === id)) {
      remove(this.selectedItems, item => item === id);
    } else {
      this.selectedItems.push(id);
    }
    this.selectedItemsChanges.emit(this.selectedItems);
  }

  openIngredient(id: number) {
    if (id) {
      this.router.navigate([`/ingredient/${id}`], { replaceUrl: true });
    }
  }
}
