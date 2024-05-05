import { Component, OnInit } from '@angular/core';
import { User } from '../../interfaces/user';
import { UserService } from '../../services/user/user.service';
import { MatDialog } from '@angular/material/dialog';
import { PasswordChangeComponent } from '../password-change/password-change.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User | null = null;
  message: string = '';

  constructor(private userService: UserService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.userService.getUserProfile().subscribe(
      (user: User) => {
        this.user = user;
        console.log('User profile loaded:', user);
      },
      error => {
        console.error('Error loading user profile:', error);
      }
    );
  }

  openPasswordChangeDialog(): void {
    const dialogRef = this.dialog.open(PasswordChangeComponent, {
      width: '390px',
      height: '440px'
    });
  }

  saveProfile(): void {
    if (this.user) {
      this.userService.updateUserProfile(this.user).subscribe(
        response => {
          console.log('Profile updated successfully:', response);
          this.message = 'Профіль успішно оновлено.';
          setTimeout(() => {
            this.message = '';
          }, 6500);
        },
        error => {
          console.error('Error updating user profile:', error);
          if (error.status === 400 && error.error.message === "Password change failed") {
            this.message = 'Помилка при зміні паролю.';
          } else {
            this.message = 'Помилка при оновленні профілю.';
          }
        }
      );
    }
  }

}
