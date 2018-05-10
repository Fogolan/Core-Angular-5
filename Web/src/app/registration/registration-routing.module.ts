import { extract } from '@app/core';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { RegistrationFormComponent } from '@app/registration/registration-form/registration-form.component';

const routes: Routes = [
    { path: 'registration', component: RegistrationFormComponent, data: { title: extract('Registration') }}
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
  })

  export class RegistrationRoutingModule {}
