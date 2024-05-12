import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaymentMethod } from '../../interfaces/payment-method';
import { DeliveryMethod } from '../../interfaces/delivery-method';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private readonly baseUrl = 'https://localhost:7023/api/feedbacks';

  constructor(private http: HttpClient) { }

  getPaymentMethods(): Observable<PaymentMethod[]> {
    const url = `${this.baseUrl}/payment-methods`;
    return this.http.get<PaymentMethod[]>(url);
  }

  getDeliveryMethods(): Observable<DeliveryMethod[]> {
    const url = `${this.baseUrl}/delivery-methods`; 
    return this.http.get<DeliveryMethod[]>(url);
  }
}
