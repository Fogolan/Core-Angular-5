import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegistrationRoutingModule } from '@app/registration/registration-routing.module';
import { RegistrationFormComponent } from '@app/registration/registration-form/registration-form.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TranslateModule,
    NgbModule,
    RegistrationRoutingModule
  ],
  declarations: [
    RegistrationFormComponent
  ]
})
export class RegistrationModule { }
