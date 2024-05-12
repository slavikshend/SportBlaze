import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart/cart.service';
import { UserService } from '../../services/user/user.service';
import { User } from '../../interfaces/user';
import { PaymentMethod } from '../../interfaces/payment-method';
import { DeliveryMethod } from '../../interfaces/delivery-method';
import { CartItem } from '../../interfaces/cart-item';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderService } from '../../services/order/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  currentUser: User = {
    firstName: '',
    lastName: '',
    phone: '',
    city: '',
    address: ''
  };
  paymentMethods: PaymentMethod[] = [];
  deliveryMethods: DeliveryMethod[] = [];
  duplicatedCartItems: CartItem[] = [];
  selectedPaymentMethod: string = '';
  selectedDeliveryMethod: string = '';
  orderForm: FormGroup;

  constructor(
    private cartService: CartService,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private orderService: OrderService
  ) {
    this.orderForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phone: ['', Validators.required],
      city: ['', Validators.required],
      address: ['', Validators.required],
      paymentMethod: ['', Validators.required],
      deliveryMethod: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadCurrentUser();
    this.loadPaymentMethods();
    this.loadDeliveryMethods();
    this.duplicateCartItems();
  }

  duplicateCartItems(): void {
    this.duplicatedCartItems = this.cartService.getCartItems();
  }

  loadCurrentUser(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.userService.getUserProfile().subscribe((user: User) => {
        this.currentUser = user;
      });
    }
  }

  loadPaymentMethods(): void {
    this.orderService.getPaymentMethods().subscribe((methods: PaymentMethod[]) => {
      this.paymentMethods = methods;
    });
  }

  loadDeliveryMethods(): void {
    this.orderService.getDeliveryMethods().subscribe((methods: DeliveryMethod[]) => {
      this.deliveryMethods = methods;
    });
  }

  onSubmitOrder(): void {
    // Implement submitting the order
  }

  isCartEmpty(): boolean {
    return this.cartService.getCartItems().length === 0;
  }

  calculateDiscountedPrice(price: number, discount: number): number {
    return price - (price * (discount / 100));
  }

  calculateTotalSum(): number {
    let totalSum = 0;
    this.duplicatedCartItems.forEach(item => {
      totalSum += this.calculateDiscountedPrice(item.price, item.discount) * item.quantity;
    });
    return totalSum;
  }
}
