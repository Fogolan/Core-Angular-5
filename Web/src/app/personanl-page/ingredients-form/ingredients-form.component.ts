import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { Ingredient } from './models/ingredient';
import { values, cloneDeep } from 'lodash';
import { IngredientService } from '@app/personanl-page/ingredients-form/ingredient.service';
import { UploadPhotoComponent } from '@app/shared';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-ingredients-form',
  templateUrl: './ingredients-form.component.html',
  styleUrls: ['./ingredients-form.component.scss'],
  providers: [IngredientService]
})
export class IngredientsFormComponent implements OnInit {

  form: FormGroup;
  ingredient: Ingredient;
  initialState = new Ingredient();
  private subscriptions: Subscription[] = [];

  constructor(private fb: FormBuilder,
              private service: IngredientService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    if (this.route.firstChild) {
      this.subscriptions.push(this.route.firstChild.params.subscribe(params => this.loadIngredient(+params['id'])));
    } else {
      this.loadIngredient(0);
    }
  }

  isControlInvalid(controlName: string): boolean {
    const control = this.form.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  urlChange(url: string) {
    this.ingredient.imageSrc = url;
  }

  isNewIngredient(): boolean {
    return this.ingredient && !this.ingredient.id;
  }

  async onSubmit() {
    const controls = this.form.controls;
    if (this.form.invalid) {
      Object.keys(controls)
        .forEach(controlName => controls[controlName].markAsTouched());
      return;
    } else {
        await this.service.updateIngredient(this.ingredient);
        this.router.navigateByUrl('/ingredients');
      }
  }

  async delete() {
    await this.service.deleteById(this.ingredient.id);
    this.router.navigateByUrl('/ingredients');
  }

  private loadIngredient(id: number) {
    this.ingredient = null;
    this.initForm();
    if (id) {
      this.service.getById(id).then((data: Ingredient) => this.initIngredient(data));
    } else {
      this.initIngredient(new Ingredient());
    }
  }

  private initIngredient(ingredient: Ingredient) {
    this.initialState = ingredient;
    this.resetForm();
  }

  private resetForm() {
    this.ingredient = cloneDeep(this.initialState);
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
        Validators.max(99),
        Validators.min(0)
      ]]
    });
  }
}
