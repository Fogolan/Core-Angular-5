import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Route, extract } from '@app/core';
import { CocktailFormComponent } from '@app/personanl-page/cocktail-form/cocktail-form.component';
import { IngredientsFormComponent } from '@app/personanl-page/ingredients-form/ingredients-form.component';
import { IngretientsComponent } from '@app/personanl-page/ingretients/ingretients.component';
import { CocktailsComponent } from '@app/personanl-page/cocktails/cocktails.component';
import { CocktailsListComponent } from '@app/personanl-page/cocktails-list/cocktails-list.component';
import { MyCocktailsComponent } from '@app/personanl-page/my-cocktails/my-cocktails.component';
import { MyIngredientsComponent } from '@app/personanl-page/my-ingredients/my-ingredients.component';

const routes: Routes = [
  Route.withShell([
    {
      path: 'ingredient',
      component: IngredientsFormComponent,
      data: { title: extract('Ingredient') },
      children: [
        {
          path: ':id',
          component: IngredientsFormComponent
        }
      ]
    },
    {
      path: 'cocktail',
      component: CocktailFormComponent,
      data: { title: extract('Cocktail') },
      children: [
        {
          path: ':id',
          component: CocktailFormComponent
        }
      ]
    },
    {
      path: 'cocktailview',
      component: CocktailFormComponent,
      data: {
        title: extract('Cocktail'),
        readonly: true
      },
      children: [
        {
          path: ':id',
          component: CocktailFormComponent,
          data: { readonly: true }
        }
      ]
    },
    {
      path: 'ingredients',
      component: IngretientsComponent,
      data: { title: extract('Ingredients') }
    },
    {
      path: 'cocktails',
      component: CocktailsListComponent,
      data: { title: extract('Cocktails') }
    },
    {
      path: 'mycocktails',
      component: MyCocktailsComponent,
      data: { title: extract('Cocktails') }
    },
    {
      path: 'myingredients',
      component: MyIngredientsComponent,
      data: { title: extract('Ingredients') }
    }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class PersonalPageRoutingModule {}
