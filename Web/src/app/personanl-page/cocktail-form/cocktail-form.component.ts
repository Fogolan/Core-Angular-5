import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgModel} from '@angular/forms';
import { Cocktail } from '@app/personanl-page/cocktail-form/models/cocktail';
import { HttpClient } from '@angular/common/http';
import { CocktailService } from './cocktail.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { cloneDeep } from 'lodash';
import { IngredientDto } from '@app/shared/ingredients-list/models/ingredientDto';

@Component({
  selector: 'app-cocktail-form',
  templateUrl: './cocktail-form.component.html',
  styleUrls: ['./cocktail-form.component.scss']
})
export class CocktailFormComponent implements OnInit {

  form: FormGroup;
  cocktail: Cocktail;
  initialState = new Cocktail();
  private subscriptions: Subscription[] = [];

  constructor(
    private cocktailService: CocktailService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    if (this.route.firstChild) {
      this.subscriptions.push(this.route.firstChild.params.subscribe(params => this.loadCocktail(+params['id'])));
    } else {
      this.loadCocktail(0);
    }
    this.cocktail = new Cocktail();
    this.initForm();
  }

  isControlInvalid(controlName: string): boolean {
    const control = this.form.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  async onSubmit() {
    const controls = this.form.controls;
    if (this.form.invalid) {
      Object.keys(controls)
        .forEach(controlName => controls[controlName].markAsTouched());
      return;
    } else {
        await this.cocktailService.updateCocktail(this.cocktail);
        console.log(this.form.value);
      }
  }

  ingredientsChange(ingredients: IngredientDto[]) {
    this.cocktail.ingredients = ingredients;
  }

  urlChange(url: string) {
    this.cocktail.imageSrc = url;
  }

  isNewIngredient(): boolean {
    return this.cocktail && !this.cocktail.id;
  }

  async delete() {
    await this.cocktailService.deleteById(this.cocktail.id);
  }

  private loadCocktail(id: number) {
    this.cocktail = null;
    this.initForm();
    if (id) {
      this.cocktailService.getById(id).then((data: Cocktail) => this.initCocktail(data));
    } else {
      this.initCocktail(new Cocktail());
    }
  }

  private initCocktail(cocktail: Cocktail) {
    this.initialState = cocktail;
    this.resetForm();
  }

  private resetForm() {
    this.cocktail = cloneDeep(this.initialState);
    if (this.form != null) {
      this.form.markAsPristine();
      this.form.markAsUntouched();
    }
  }

  private initForm() {
    this.form = this.fb.group({
      name: ['', [
        Validators.required
      ]],
      degrees: ['', [
        Validators.required,
        Validators.max(100)
      ]],
      amount: ['', [
        Validators.required,
        Validators.max(50000)
      ]]
    });
  }
}
