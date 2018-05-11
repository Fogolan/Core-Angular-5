import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Route, extract } from '@app/core';
import { CocktailFormComponent } from '@app/personanl-page/cocktail-form/cocktail-form.component';
import { IngredientsFormComponent } from '@app/personanl-page/ingredients-form/ingredients-form.component';
import { IngretientsComponent } from '@app/personanl-page/ingretients/ingretients.component';

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
      path: 'ingredients',
      component: IngretientsComponent,
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
