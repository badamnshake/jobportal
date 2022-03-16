import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import {
  NgbDatepickerModule,
  NgbPaginationModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
    }),
    NgxSpinnerModule,
    NgbDatepickerModule,
    NgbPaginationModule,
  ],
  exports: [ToastrModule, NgbDatepickerModule, NgbPaginationModule, NgxSpinnerModule],
})
export class SharedModule {}
