import { Component, Input } from '@angular/core';
import { CartItem } from '../../interfaces/cart-item';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  @Input() cartItems: CartItem[] = [];

  isCartEmpty(): boolean {
    return this.cartItems.length === 0;
  }

  placeOrder(): void {
    console.log('Placing order...');
  }
}
