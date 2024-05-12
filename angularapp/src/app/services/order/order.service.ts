import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaymentMethod } from '../../interfaces/payment-method';
import { DeliveryMethod } from '../../interfaces/delivery-method';
import { Order } from '../../interfaces/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private readonly baseUrl = 'https://localhost:7023/api/orders';

  constructor(private http: HttpClient) { }

  getPaymentMethods(): Observable<PaymentMethod[]> {
    const url = `${this.baseUrl}/payment-methods`;
    return this.http.get<PaymentMethod[]>(url);
  }

  getDeliveryMethods(): Observable<DeliveryMethod[]> {
    const url = `${this.baseUrl}/delivery-methods`;
    return this.http.get<DeliveryMethod[]>(url);
  }

  addOrder(order: Order): Observable<any> {
    return this.http.post<any>(this.baseUrl, order);
  }

  addPayment(orderId: number): Observable<any> {
    const url = `${this.baseUrl}/${orderId}/payment`;
    return this.http.post<any>(url, null); 
  }
}
