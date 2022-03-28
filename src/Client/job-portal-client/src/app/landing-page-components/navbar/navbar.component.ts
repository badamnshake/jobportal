import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Employer } from 'src/app/_models/employer';
import { EmployerService } from 'src/app/_services/employer.service';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  model: any = {};
  loggedIn: boolean = false;
  employerInfo: Employer;

  constructor(
    public accountService: AccountService,
    private router: Router,
    private employerService: EmployerService,
    private modalService: NgbModal,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  // after loggin in you are transfered to vacancy list
  login() {
    this.accountService.login(this.model).subscribe(() => {
      this.router.navigateByUrl('/vacancy-list');
    });
  }

  logout() {
    if (confirm('Are you sure you want to Log Out')) {
      this.accountService.logout();
      this.router.navigateByUrl('/');
    }
  }
  // shows employer details in a modal
  // when modal is closed employer info is reset
  showEmployerDetails(content: any) {
    this.employerService.getEmployerMe().subscribe({
      next: (response) => {
        this.employerInfo = response;
        this.modalService
          .open(content, { ariaLabelledBy: 'modal-basic-title' })
          .result.then(() => {
            this.employerInfo == null;
          });
      },
      error: () => {
        this.router.navigateByUrl('/employer-profile-edit');
        this.toastr.error('Please create a profile first');
      },
    });
  }
}
