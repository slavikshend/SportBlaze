import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { RegistrationComponent } from '../registration/registration.component';
import { LoginService } from '../../services/login/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  passwordHidden: boolean = true;
    errorMessage: any;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<LoginComponent>,
    private dialog: MatDialog,
    private loginService: LoginService
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }
  togglePasswordVisibility(event: Event): void {
  event.preventDefault();
  const passwordInput = document.getElementById('passwordInput') as HTMLInputElement;
  passwordInput.type = this.passwordHidden ? 'password' : 'text';
  this.passwordHidden = !this.passwordHidden;
}


  get formControls() {
    return this.loginForm.controls;
  }

  login() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.errorMessage = null;
      this.loginService.login(email, password).subscribe(
        token => {
          console.log('Login successful. Token:', token);
          this.dialogRef.close();
        },
        error => {
          console.error('Login error:', error.error);
          if (error.status === 401) {
            this.errorMessage = 'Логін або пароль невірні.';
            console.error('Login error:', error);
          } else {
            this.errorMessage = 'Логін або пароль невірні.';
            console.error('Login error:', error);

          }
        }
      );
    }
  }



  openRegistrationDialog() {
    this.dialogRef.close(); 
    this.dialog.open(RegistrationComponent, {
      width: '400px',
      height: '450px',
      autoFocus: false 
    });
  }
}
