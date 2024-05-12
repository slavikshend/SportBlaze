import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartItem } from '../../interfaces/cart-item';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItemsSubject = new BehaviorSubject<CartItem[]>([]);
  cartItems$ = this.cartItemsSubject.asObservable();

  constructor() {
    this.loadCartItemsFromStorage();
  }

  addToCart(item: CartItem): void {
    const currentCartItems = this.cartItemsSubject.getValue();
    const updatedCartItems = [...currentCartItems, item];
    this.cartItemsSubject.next(updatedCartItems);
    this.saveCartItemsToStorage(updatedCartItems);
  }

  clearCart(): void {
    this.cartItemsSubject.next([]);
    this.saveCartItemsToStorage([]);
  }

  getCartItems(): CartItem[] {
    return this.cartItemsSubject.getValue();
  }

  removeItemFromCart(item: CartItem): void {
    const currentCartItems = this.cartItemsSubject.getValue();
    const updatedCartItems = currentCartItems.filter(cartItem => cartItem.id !== item.id);
    this.cartItemsSubject.next(updatedCartItems);
    this.saveCartItemsToStorage(updatedCartItems);
  }

  getTotalSum(): number {
    const cartItems = this.cartItemsSubject.getValue();
    return cartItems.reduce((total, item) => total + (item.price * item.quantity), 0);
  }

  private loadCartItemsFromStorage(): void {
    const storedCartItems = localStorage.getItem('cartItems');
    if (storedCartItems) {
      this.cartItemsSubject.next(JSON.parse(storedCartItems));
    }
  }

  private saveCartItemsToStorage(cartItems: CartItem[]): void {
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
  }
}
