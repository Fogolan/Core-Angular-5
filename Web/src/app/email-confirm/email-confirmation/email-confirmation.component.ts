import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.scss']
})
export class EmailConfirmationComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private router: Router) { }

  isConfirmationSuccess: boolean;
  isConfirmationMessageVisible = false;

  ngOnInit() {
    this.setConfirmationStatus();
  }

  setConfirmationStatus() {
    const params = this.route.snapshot.queryParams;
    if (params.confirmSuccess !== undefined) {
      this.isConfirmationSuccess = params.confirmSuccess;
    } else {
      this.isConfirmationMessageVisible = true;
    }
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }

}
