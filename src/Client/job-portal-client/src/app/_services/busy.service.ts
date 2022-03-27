import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})

/* -------------------------------------------------------------------------- */
/*                              loading indicator                             */
/* -------------------------------------------------------------------------- */

// this service indicated loading and shows spinner when fetching the request
// it uses Ngx Spinner
// docs link:  https://www.npmjs.com/package/ngx-spinner

export class BusyService {
  busyRequestCount = 0;

  constructor(private spinnerService: NgxSpinnerService) {}

  busy() {
    this.busyRequestCount++;
    this.spinnerService.show();
  }
  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinnerService.hide();
    }
  }
}
