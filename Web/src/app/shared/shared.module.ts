import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoaderComponent } from './loader/loader.component';
import { UploadPhotoComponent } from '@app/shared';
import { FileUploadModule } from 'ng2-file-upload';
import { IngredientsListComponent } from '@app/shared/ingredients-list/ingredients-list.component';

@NgModule({
  imports: [
    CommonModule,
    FileUploadModule,
  ],
  declarations: [
    LoaderComponent,
    UploadPhotoComponent,
    IngredientsListComponent
  ],
  exports: [
    LoaderComponent,
    UploadPhotoComponent,
    IngredientsListComponent
  ]
})
export class SharedModule { }
