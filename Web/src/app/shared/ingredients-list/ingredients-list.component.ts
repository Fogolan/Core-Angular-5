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
  @Input() selectedItems: IngredientDto[] = [];
  @Output() selectedItemsChanges: EventEmitter<IngredientDto[]> = new EventEmitter();

  ingredientsList: IngredientDto[];

  constructor(private httpClient: HttpClient, private router: Router) {}

  async ngOnInit() {
    if (this.url) {
      this.ingredientsList = await this.getIngredients(this.url);
    }
  }

  async getIngredients(url: string): Promise<IngredientDto[]> {
    return await this.httpClient.get<IngredientDto[]>(url).toPromise();
  }

  toggleSelection(selectedItem: IngredientDto) {
    if (some(this.selectedItems, item => item.id === selectedItem.id)) {
      remove(this.selectedItems, item => item.id === selectedItem.id);
    } else {
      this.selectedItems.push(selectedItem);
    }
    this.selectedItemsChanges.emit(this.selectedItems);
  }

  openIngredient(id: number) {
    if (id) {
      this.router.navigate([`/ingredient/${id}`], { replaceUrl: true });
    }
  }

  isChecked(itemId: number): boolean {
    return some(this.selectedItems, item => item.id === itemId);
  }
}
