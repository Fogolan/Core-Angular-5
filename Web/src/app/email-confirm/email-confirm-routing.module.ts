import { Routes, RouterModule } from '@angular/router';
import { extract, Route } from '@app/core';
import { EmailConfirmationComponent } from '@app/email-confirm/email-confirmation/email-confirmation.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  Route.withShell([
      { path: 'confirm', component: EmailConfirmationComponent, data: { title: extract('Personal Page') } }
  ])
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
  })
  export class EmailConfirmRoutingModule { }
