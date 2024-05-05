import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginComponent } from '../login/login.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { RegistrationService } from '../../services/registration/registration.service';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  registrationForm: FormGroup;
  passwordHidden: boolean = true;
  errorMessage: string | null = null;
  passwordHiddenConfirm: boolean = true;

  constructor(private formBuilder: FormBuilder, private dialog: MatDialog, private dialogRef: MatDialogRef<RegistrationComponent>, private registrationService: RegistrationService) {
    this.registrationForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), this.passwordValidator]], 
      confirmPassword: ['', [Validators.required]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
    }, { validator: this.passwordMatchValidator }); 
  }

  passwordValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const password = control.value;
    const hasUpperCase = /[A-Z]/.test(password);
    const hasNumber = /\d/.test(password);
    const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password);

    if (!hasUpperCase) {
      return { 'noUpperCase': true };
    }

    if (!hasNumber) {
      return { 'noNumber': true };
    }

    if (!hasSpecialChar) {
      return { 'noSpecialChar': true };
    }
    return null;
  }

  redirectToLogin() {
    this.dialog.closeAll();
    this.dialog.open(LoginComponent, {
      width: '400px',
      height: '370px'
    });
  }

  get formControls() {
    return this.registrationForm.controls;
  }

  register() {
    if (this.registrationForm.valid) {
      const registrationData = this.registrationForm.value;
      this.errorMessage = null;
      this.registrationService.register(
        registrationData.email,
        registrationData.password,
        registrationData.firstName,
        registrationData.lastName,
        registrationData.phoneNumber
      ).subscribe(
        (response) => {
          console.log('Registration response:', response);
          if (response && response.message === 'User registered successfully') {
            console.log('Registration successful.');
            this.dialogRef.close();
            this.dialog.open(LoginComponent, {
              width: '400px',
              height: '385px',
              autoFocus: false
            });
          } else {
            console.error('User registration failed');
            this.errorMessage = 'Помилка при реєстрації. Спробуйте ще раз.';
          }
        },
        (error) => {
          console.error('Registration error:', error);
          if (error.status === 400 && error.error && error.error.message === 'User registration failed') {
            this.errorMessage = 'Користувач з такою поштою вже існує!';
          } else {
            this.errorMessage = 'Помилка при реєстрації. Спробуйте ще раз.';
          }
        }
      );
    }
  }


  passwordMatchValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (password !== confirmPassword) {
      control.get('confirmPassword')?.setErrors({ 'passwordMismatch': true });
      return { 'passwordMismatch': true };
    } else {
      control.get('confirmPassword')?.setErrors(null);
    }

    return null;
  }

  togglePasswordVisibility(): void {
    const passwordInput = document.getElementById('passwordInput') as HTMLInputElement;
    passwordInput.type = this.passwordHidden ? 'password' : 'text';
    this.passwordHidden = !this.passwordHidden;
  }

  toggleConfirmPasswordVisibility(): void {
    const confirmPasswordInput = document.getElementById('confirmPasswordInput') as HTMLInputElement;
    confirmPasswordInput.type = this.passwordHiddenConfirm ? 'password' : 'text';
    this.passwordHiddenConfirm = !this.passwordHiddenConfirm;
  }
}
