import { Component, EventEmitter, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { RegistrationComponent } from '../registration/registration.component';
import { LoginComponent } from '../login/login.component';
import { Router } from '@angular/router';
import { CartService } from '../../services/cart/cart.service';
import { ProductService } from '../../services/product/product.service';
import { SearchService } from '../../services/search/search.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  showLoginMenu: boolean = false;
  userFirstName: string | null = null;
  userRole: string | null = null;
  showCart: boolean = false;
  cartItemCount: number = 0; 
  searchValue: string = '';
  constructor(private searchService: SearchService, private dialog: MatDialog, private router: Router, private cartService: CartService, private productService: ProductService) { }

  ngOnInit(): void {
    this.showLoginMenu = false;
    window.addEventListener('storage', () => {
      this.userFirstName = localStorage.getItem('userFirstName');
      this.userRole = localStorage.getItem('userRole');
    });
    this.userFirstName = localStorage.getItem('userFirstName');
    this.userRole = localStorage.getItem('userRole');
    this.cartService.cartItems$.subscribe(items => {
      this.cartItemCount = items.length;
    });
  }

  searchProducts(): void {
    this.searchService.sendSearchQuery(this.searchValue);
  }

  openLoginDialog(): void {
    this.dialog.open(LoginComponent, {
      width: '400px',
      height: '385px',
      autoFocus: false
    });
  }

  clearSearchInput(): void {
    this.searchValue = '';
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
    localStorage.removeItem('userFirstName');
    localStorage.removeItem('userRole');
    this.userFirstName = null;
    this.showLoginMenu = !this.showLoginMenu;
    this.router.navigate(['/']);
    window.location.reload(); 
  }

  toggleCart(): void { 
    this.showCart = !this.showCart;
  }


}
