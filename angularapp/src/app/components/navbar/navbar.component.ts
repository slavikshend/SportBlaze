import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { RegistrationComponent } from '../registration/registration.component';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  showLoginMenu: boolean = false;
  userEmail: string | null = null;
  constructor(private dialog: MatDialog) { }

  openLoginDialog(): void {
    this.dialog.open(LoginComponent, {
      width: '400px',
      height: '380px',
      autoFocus: false
    });
  }

  toggleLoginMenu(): void {
    this.showLoginMenu = !this.showLoginMenu;
  }

  openRegistrationDialog(): void {
    this.dialog.open(RegistrationComponent, {
      width: '400px',
      height: '450px',
      autoFocus: false
    });
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
  }
}

