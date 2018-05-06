import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CocktailFormComponent } from '@app/personanl-page/cocktail-form/cocktail-form.component';
import { PersonalPageRoutingModule } from '@app/personanl-page/personal-page-routing.module';

@NgModule({
  imports: [
    CommonModule,
    PersonalPageRoutingModule
  ],
  declarations: [CocktailFormComponent]
})
export class PersonalPageModule { }
