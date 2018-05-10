import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailConfirmRoutingModule } from '@app/email-confirm/email-confirm-routing.module';
import { EmailConfirmationComponent } from '@app/email-confirm/email-confirmation/email-confirmation.component';

@NgModule({
  imports: [
    CommonModule,
    EmailConfirmRoutingModule
  ],
  declarations: [EmailConfirmationComponent]
})
export class EmailConfirmModule { }
