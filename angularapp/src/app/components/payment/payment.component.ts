import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CartService } from '../../services/cart/cart.service';
import { OrderService } from '../../services/order/order.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  amount: number = 0;
  transactionID: string = '';
  @ViewChild('paymentRef', { static: true }) paymentRef!: ElementRef;
  buttonRendered: boolean = false;

  constructor(private router: Router, private cartService: CartService, private http: HttpClient, private orderService: OrderService ) { }

  ngOnInit(): void {
    this.amount = this.cartService.getTotalSum();
    this.http.get<any>('https://v6.exchangerate-api.com/v6/16be51cec7c4b9e0914052a6/latest/UAH')
      .subscribe(data => {
        const exchangeRate = data.conversion_rates.USD;
        const amountInUSD = this.amount * exchangeRate;

        window.paypal.Buttons(
          {
            createOrder: (data: any, actions: any) => {
              return actions.order.create({
                purchase_units: [
                  {
                    amount: {
                      value: amountInUSD.toFixed(2).toString(),
                      currency: 'USD'
                    }
                  }
                ]
              });
            },
            onApprove: (data: any, actions: any) => {
              return actions.order.capture().then((details: any) => {
                if (details.status === 'COMPLETED') {
                  this.transactionID = details.id;
                  const orderId = localStorage.getItem('orderId');
                  if (orderId) {
                    const orderIdNumber = parseInt(orderId, 10);
                    this.orderService.addPayment(orderIdNumber).subscribe(
                      (response: any) => {
                        console.log('Payment added successfully:', response);
                      },
                      (error: any) => {
                        console.error('Error adding payment:', error);
                      }
                    );
                  }
                  //this.router.navigate(['confirm']);
                  console.log('Payment completed:', details);
                }
              });
            },


            onError: (error: any) => {
              console.log(error);
            }
          }
        ).render(this.paymentRef.nativeElement);
      });
  }
}
