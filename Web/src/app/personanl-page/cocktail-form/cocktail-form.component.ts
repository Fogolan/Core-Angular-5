import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgModel} from '@angular/forms';
import { Cocktail } from '@app/personanl-page/cocktail-form/models/cocktail';
import { HttpClient } from '@angular/common/http';
import { CocktailService } from '@app/personanl-page/cocktail.service';

@Component({
  selector: 'app-cocktail-form',
  templateUrl: './cocktail-form.component.html',
  styleUrls: ['./cocktail-form.component.scss']
})
export class CocktailFormComponent implements OnInit {

  form: FormGroup;
  cocktail: Cocktail;
  constructor(
    private cocktailService: CocktailService,
    private fb: FormBuilder,
  ) { }

  ngOnInit() {
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
        await this.cocktailService.createCocktail(this.cocktail);
        console.log(this.form.value);
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
      ]],
      image: ['', [
        Validators.required
      ]]
    });
  }
}
