import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CocktailFormComponent } from '@app/personanl-page/cocktail-form/cocktail-form.component';
import { PersonalPageRoutingModule } from '@app/personanl-page/personal-page-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CocktailService } from '@app/personanl-page/cocktail.service';

@NgModule({
  imports: [
    CommonModule,
    PersonalPageRoutingModule,
    ReactiveFormsModule
  ],
  providers:[CocktailService],
  declarations: [CocktailFormComponent]
})
export class PersonalPageModule { }
