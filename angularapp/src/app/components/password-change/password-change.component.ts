import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PasswordService } from '../../services/password/password.service';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.css']
})
export class PasswordChangeComponent {
  passwordChangeForm: FormGroup;
  errorMessage: string | null = null;
  passwordHidden: boolean = true;
  passwordHiddenConfirm: boolean = true;
  passwordHiddenOld: boolean = true;

  constructor(
    private dialogRef: MatDialogRef<PasswordChangeComponent>,
    private formBuilder: FormBuilder, private passwordService: PasswordService 
  ) {
    this.passwordChangeForm = this.formBuilder.group({
      oldPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(8), this.passwordValidator]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  submitForm(): void {
    if (this.passwordChangeForm.valid) {
      const oldPassword = this.passwordChangeForm.value.oldPassword;
      const newPassword = this.passwordChangeForm.value.newPassword;
      this.passwordService.changePassword(oldPassword, newPassword)
        .subscribe(
          () => {
            this.passwordChangeForm.reset();
            this.dialogRef.close();
          },
          error => {
            this.errorMessage = error.message;
          }
        );
    }
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

  passwordMatchValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const newPassword = control.get('newPassword')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (newPassword !== confirmPassword) {
      control.get('confirmPassword')?.setErrors({ 'passwordMismatch': true });
      return { 'passwordMismatch': true };
    } else {
      control.get('confirmPassword')?.setErrors(null);
    }
    return null;
  }

  togglePasswordVisibility(): void {
    const newPasswordInput = document.getElementById('newPasswordInput') as HTMLInputElement;
    newPasswordInput.type = this.passwordHidden ? 'password' : 'text';
    this.passwordHidden = !this.passwordHidden;
  }

  toggleConfirmPasswordVisibility(): void {
    const confirmPasswordInput = document.getElementById('confirmPasswordInput') as HTMLInputElement;
    confirmPasswordInput.type = this.passwordHiddenConfirm ? 'password' : 'text';
    this.passwordHiddenConfirm = !this.passwordHiddenConfirm;
  }

  toggleOldPasswordVisibility(): void {
    const oldPasswordInput = document.getElementById('oldPasswordInput') as HTMLInputElement;
    oldPasswordInput.type = this.passwordHiddenOld ? 'password' : 'text';
    this.passwordHiddenOld = !this.passwordHiddenOld;
  }
}
