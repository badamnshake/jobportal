import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import {
  NgbDatepickerModule,
  NgbModule,
  NgbPaginationModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
    }),
    NgbModule,
    NgxSpinnerModule,
    NgbDatepickerModule,
    NgbPaginationModule,
    FontAwesomeModule,
  ],
  exports: [
    ToastrModule,
    NgbModule,
    NgbDatepickerModule,
    NgbPaginationModule,
    NgxSpinnerModule,
    FontAwesomeModule,
  ],
})
export class SharedModule {}
