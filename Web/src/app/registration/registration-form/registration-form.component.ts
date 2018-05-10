import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { I18nService } from '@app/core';
import { FormBuilder, Validators, FormControl, FormGroup, EmailValidator } from '@angular/forms';
import {Registration} from './models/registration';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.scss']
})
export class RegistrationFormComponent implements OnInit {
  form: FormGroup;
  registration: Registration = new Registration();
  error: string;

  constructor(private router: Router,
              private i18nService: I18nService,
              private fb: FormBuilder,
              private http: HttpClient) { }

  ngOnInit() {
    this.initForm();
  }

  private initForm() {
    this.form = this.fb.group({
      UserName: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.pattern(/[a-zA-Z0-9]/), Validators.min(6)]],
      RepeatPassword: ['', [Validators.required, this.matchOtherValidator('Password')]]
    });
  }

  matchOtherValidator(otherControlName: string) {
    let thisControl: FormControl;
    let otherControl: FormControl;

    return function matchOtherValidate(control: FormControl) {
      if (!control.parent) {
        return null;
      }

      if (!thisControl) {
        thisControl = control;
        otherControl = control.parent.get(otherControlName) as FormControl;
        if (!otherControl) {
          throw new Error('matchOtherValidator(): other control is not found in parent group');
        }
        otherControl.valueChanges.subscribe(() => {
          thisControl.updateValueAndValidity();
        });
      }

      if (!otherControl) {
        return null;
      }

      if (otherControl.value !== thisControl.value) {
        return {
          matchOther: true
        };
      }

      return null;
    };
  }

  isControlInvalid(controlName: string): boolean {
    const control = this.form.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  setLanguage(language: string) {
    this.i18nService.language = language;
  }

  get currentLanguage(): string {
    return this.i18nService.language;
  }

  get languages(): string[] {
    return this.i18nService.supportedLanguages;
  }

  async submit() {
    this.error = null;
     await this.http.post('/user', this.registration)
      .toPromise()
      .then(() => this.router.navigate(['/confirm']))
      .catch(() => {
        this.error = 'Please enter unique username';
      });

  }
}
