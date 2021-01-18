import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { AuthService } from '../auth.service';
import {first} from 'rxjs/operators';
import {Router} from '@angular/router';

@Component({
  selector: 'app-email-login',
  templateUrl: './email-login.component.html',
  styleUrls: ['./email-login.component.scss']
})
export class EmailLoginComponent implements OnInit {
  form: FormGroup;
  type: 'login' | 'signup' | 'reset' = 'signup';
  loading = false;

  serverMessage: string;

  constructor(private fb: FormBuilder, private as: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      firstName: ['', []],
      lastName: ['', []],
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [Validators.required, Validators.pattern('(?=^.{6,50}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;\'?/&gt;.&lt;,])(?!.*\\s).*$')]
      ],
      passwordConfirm: ['', []]
    });
  }

  changeType(val: 'login' | 'signup' | 'reset'): void {
    this.type = val;
  }

  get isLogin(): boolean {
    return this.type === 'login';
  }

  get isSignup(): boolean {
    return this.type === 'signup';
  }

  get isPasswordReset(): boolean {
    return this.type === 'reset';
  }

  get firstName(): AbstractControl | null {
    return this.form.get('firstName');
  }

  get lastName(): AbstractControl | null {
    return this.form.get('lastName');
  }

  get email(): AbstractControl | null {
    return this.form.get('email');
  }
  get password(): AbstractControl | null {
    return this.form.get('password');
  }

  get passwordConfirm(): AbstractControl | null {
    return this.form.get('passwordConfirm');
  }

  get passwordDoesMatch(): boolean {
    if (this.type !== 'signup') {
      return true;
    } else {
      return this.password.value === this.passwordConfirm.value;
    }
  }

  async onSubmit(): Promise<void> {
    this.loading = true;

    const firstName = this.firstName.value;
    const lastName = this.lastName.value;
    const email = this.email.value;
    const password = this.password.value;

    if (this.isLogin) {
      this.as.login(email, password).subscribe(() => {
        this.router.navigateByUrl('/login');
        this.serverMessage = '';
      }, err => {
        this.serverMessage = err.error.message;
      });
    }

    if (this.isSignup) {
      this.as.register(firstName, lastName, email, password).subscribe(() => {
        this.router.navigateByUrl('/login');
      }, err => {
        this.serverMessage = err.error.errors[0];
      });
    }

    this.loading = false;
  }
}
