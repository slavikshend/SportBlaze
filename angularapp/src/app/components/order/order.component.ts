import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart/cart.service';
import { UserService } from '../../services/user/user.service';
import { User } from '../../interfaces/user';
import { PaymentMethod } from '../../interfaces/payment-method';
import { DeliveryMethod } from '../../interfaces/delivery-method';
import { CartItem } from '../../interfaces/cart-item';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderService } from '../../services/order/order.service';
import { Order } from '../../interfaces/order';
import { Router } from '@angular/router';

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
    private orderService: OrderService,
    private router: Router
  ) {
    this.orderForm = this.formBuilder.group({
      email: ['', Validators.email], // Add email field with validators
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

  loadCurrentUser(): void {
    const token = localStorage.getItem('token');
    if (token) {
      const userEmail = localStorage.getItem('userEmail');
      if (userEmail) {
        this.orderForm.get('email')?.setValue(userEmail);
      }
      this.userService.getUserProfile().subscribe((user: User) => {
        this.currentUser = user;
        this.orderForm.patchValue({
          firstName: user.firstName,
          lastName: user.lastName,
          phone: user.phone,
          city: user.city,
          address: user.address
        });
      });
    }
  }



  loadPaymentMethods(): void {
    this.orderService.getPaymentMethods().subscribe((methods: PaymentMethod[]) => {
      this.paymentMethods = methods;
    });
  }
  duplicateCartItems(): void {
    this.duplicatedCartItems = this.cartService.getCartItems();
  }
  loadDeliveryMethods(): void {
    this.orderService.getDeliveryMethods().subscribe((methods: DeliveryMethod[]) => {
      this.deliveryMethods = methods;
    });
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
  onSubmitOrder(): void {
    if (this.orderForm.valid) {
      const paymentMethodId = this.orderForm.value.paymentMethod;
      const isPaymentHeldOnline = paymentMethodId === 2;

      const order: Order = {
        email: this.orderForm.value.email,
        firstName: this.orderForm.value.firstName,
        lastName: this.orderForm.value.lastName,
        phone: this.orderForm.value.phone,
        address: this.orderForm.value.address,
        city: this.orderForm.value.city,
        paymentMethodId: paymentMethodId,
        deliveryMethodId: this.orderForm.value.deliveryMethod,
        orderStatusId: 1,
        orderDate: new Date(),
        orderAddress: this.orderForm.value.address,
        total: this.calculateTotalSum(),
        cartItems: this.duplicatedCartItems.map(item => ({ id: item.id, quantity: item.quantity })),
      };

      this.orderService.addOrder(order).subscribe(
        (id:number) => {
          if (isPaymentHeldOnline) {
            localStorage.setItem('orderId', id.toString());
           this.router.navigate(['/payment']);
          }
          console.log('Order added successfully');
        },
        error => {
          console.error('Error adding order:', error);
        }
      );
    }
  }
}
