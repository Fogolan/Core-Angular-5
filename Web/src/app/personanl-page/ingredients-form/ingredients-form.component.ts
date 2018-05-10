import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { Ingredient } from './models/ingredient';
import { values } from 'lodash';
import { IngredientService } from '@app/personanl-page/ingredients-form/ingredient.service';

@Component({
  selector: 'app-ingredients-form',
  templateUrl: './ingredients-form.component.html',
  styleUrls: ['./ingredients-form.component.scss'],
  providers: [IngredientService]
})
export class IngredientsFormComponent implements OnInit {

  form: FormGroup;
  ingredient: Ingredient;

  constructor(private fb: FormBuilder,
              private service: IngredientService) { }

  ngOnInit() {
    this.ingredient = new Ingredient();
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
        await this.service.updateIngredient(this.ingredient);
      }
  }

  private initForm() {
    this.form = this.fb.group({
      name: ['', [
        Validators.required
      ]],
      degrees: ['', [
        Validators.required,
        Validators.max(100),
        Validators.min(0)
      ]],
      image: ['', [
        Validators.required
      ]]
    });
  }
}
