import { Component, Input } from '@angular/core';
import { CartItem } from '../../interfaces/cart-item';
import { CartService } from '../../services/cart/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  constructor(private cartService: CartService, private router: Router) { }

  ngOnInit(): void {
    this.cartService.cartItems$.subscribe(items => {
      this.cartItems = items;
    });
  }

  showCart: boolean = true;
  @Input() cartItems: CartItem[] = [];

  isCartEmpty(): boolean {
    return this.cartItems.length === 0;
  }

  calculateDiscountedPrice(price: number, discount: number): number {
    return price - (price * (discount / 100));
  }

  closeCart(): void {
    this.showCart = false;
  }

  placeOrder(): void {
    this.showCart = false;
    this.router.navigate(['/makeorder']);
  }

  removeItem(item: CartItem): void {
    this.cartService.removeItemFromCart(item);
  }

  decreaseQuantity(item: CartItem): void {
    if (item.quantity > 1) {
      item.quantity--;
    }
  }

  increaseQuantity(item: CartItem): void {
    if (item.quantity < 10) {
      item.quantity++;
    }
  }

  calculateTotalSum(): number {
    let totalSum = 0;
    this.cartItems.forEach(item => {
      totalSum += this.calculateDiscountedPrice(item.price, item.discount) * item.quantity;
    });
    return totalSum;
  }


}
