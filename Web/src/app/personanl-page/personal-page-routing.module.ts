import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Route, extract } from '@app/core';
import { CocktailFormComponent } from '@app/personanl-page/cocktail-form/cocktail-form.component';

const routes: Routes = [
  Route.withShell([
    { path: 'personalpage', component: CocktailFormComponent, data: { title: extract('Personalpage') } }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class PersonalPageRoutingModule { }
