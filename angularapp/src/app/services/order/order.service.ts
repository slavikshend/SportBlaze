import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../../interfaces/order';
import { OrderModel } from '../../interfaces/order-model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private readonly baseUrl = 'https://localhost:7023/api/orders';

  constructor(private http: HttpClient) { }

  getPaymentMethods(): Observable<any> {
    const url = `${this.baseUrl}/payment-methods`;
    return this.http.get<any>(url);
  }

  getDeliveryMethods(): Observable<any> {
    const url = `${this.baseUrl}/delivery-methods`;
    return this.http.get<any>(url);
  }

  addOrder(order: Order): Observable<any> {
    return this.http.post<any>(this.baseUrl, order);
  }

  addPayment(orderId: number): Observable<any> {
    const url = `${this.baseUrl}/${orderId}/payment`;
    return this.http.post<any>(url, null);
  }

  getAllOrders(): Observable<OrderModel[]> {
    return this.http.get<OrderModel[]>(this.baseUrl);
  }

  changeOrderStatus(orderId: number, statusId: number): Observable<any> {
    const url = `${this.baseUrl}/${orderId}/status/${statusId}`;
    return this.http.put<any>(url, null);
  }

  getUserOrders(userEmail: string): Observable<OrderModel[]> {
    const url = `${this.baseUrl}/user/${userEmail}/orders`;
    return this.http.get<OrderModel[]>(url);
  }

}
