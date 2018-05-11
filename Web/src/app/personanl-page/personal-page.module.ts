import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CocktailFormComponent } from '@app/personanl-page/cocktail-form/cocktail-form.component';
import { PersonalPageRoutingModule } from '@app/personanl-page/personal-page-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CocktailService } from './cocktail-form/cocktail.service';
import { IngredientsFormComponent } from '@app/personanl-page/ingredients-form/ingredients-form.component';
import { SharedModule } from '@app/shared';
import { IngretientsComponent } from '@app/personanl-page/ingretients/ingretients.component';

@NgModule({
  imports: [
    CommonModule,
    PersonalPageRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [CocktailService],
  declarations: [
    CocktailFormComponent,
    IngredientsFormComponent,
    IngretientsComponent
  ]
})
export class PersonalPageModule { }
