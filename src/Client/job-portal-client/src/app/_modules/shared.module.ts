import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import {
  NgbDatepickerModule,
  NgbPaginationModule,
} from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    }),
    NgbDatepickerModule,
    NgbPaginationModule,
  ],
  exports: [ToastrModule, NgbDatepickerModule, NgbPaginationModule],
})
export class SharedModule {}
