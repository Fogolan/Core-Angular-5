import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoaderComponent } from './loader/loader.component';
import { UploadPhotoComponent } from '@app/shared';
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
  imports: [
    CommonModule,
    FileUploadModule,
  ],
  declarations: [
    LoaderComponent,
    UploadPhotoComponent
  ],
  exports: [
    LoaderComponent,
    UploadPhotoComponent
  ]
})
export class SharedModule { }
