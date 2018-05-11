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
  @Input()
  set selectedItems(items: IngredientDto[]) {
    this.setSelectedItems = items;
    if (!this.url) {
      this.ingredientsList = items;
    }
  }

  setSelectedItems: IngredientDto[] = [];
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
    if (some(this.setSelectedItems, item => item.id === selectedItem.id)) {
      remove(this.setSelectedItems, item => item.id === selectedItem.id);
    } else {
      this.setSelectedItems.push(selectedItem);
    }
    this.selectedItemsChanges.emit(this.setSelectedItems);
  }

  openIngredient(id: number) {
    if (id) {
      this.router.navigate([`/ingredient/${id}`], { replaceUrl: true });
    }
  }

  isChecked(itemId: number): boolean {
    return some(this.setSelectedItems, item => item.id === itemId);
  }
}
