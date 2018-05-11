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
      data: { title: extract('Ingredients') },
      children: [
        {
          path: ':id',
          component: IngredientsFormComponent
        }
      ]
    },
    {
      path: 'personalpage',
      component: CocktailFormComponent,
      data: { title: extract('Personalpage') }
    },
    {
      path: 'ingredients',
      component: IngretientsComponent,
      data: { title: extract('Personalpage') }
    }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class PersonalPageRoutingModule {}
