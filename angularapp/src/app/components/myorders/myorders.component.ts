import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { OrderModel } from '../../interfaces/order-model';
import { OrderService } from '../../services/order/order.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './myorders.component.html',
  styleUrls: ['./myorders.component.css']
})
export class MyOrdersComponent implements OnInit {
  orders: OrderModel[] = [];
  searchQuery: string = '';
  pageIndex: number = 0;
  pageSize: number = 5;
  totalItems: number = 0;
  pageSizeOptions: number[] = [5, 10, 20];
  orderStatuses: string[] = [
    'Нове замовлення',
    'Підтверджено',
    'Скасовано клієнтом',
    'Скасовано магазином',
    'У дорозі',
    'Діставлено',
    'Прийнято клієнтом'
  ];

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    const userEmail = localStorage.getItem('userEmail');
    this.orderService.getUserOrders(userEmail!).subscribe(
      (orders: OrderModel[]) => {
        this.orders = orders;
        this.totalItems = this.orders.length;
        this.filterOrders();
        this.paginator.firstPage();
      }
    );
  }

  handlePageEvent(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
  }

  updateOrderStatus(order: OrderModel, statusText: string): void {
    const statusId = this.getStatusIdByText(statusText);
    this.orderService.changeOrderStatus(order.id, statusId).subscribe(
      (response: any) => {
        console.log('Order status changed successfully');
      },
      (error: any) => {
        console.error('Error changing order status:', error);
      }
    );
  }

  filterOrders(): void {
    if (this.searchQuery) {
      this.orders = this.orders.filter(order =>
        order.id.toString().includes(this.searchQuery)
      );
    }
  }

  updateSearchQuery(event: any): void {
    const value = event.target ? event.target.value : null;
    if (value !== null) {
      this.searchQuery = value;
      this.loadOrders();
    }
  }

  getStatusIdByText(statusText: string): number {
    switch (statusText) {
      case 'Нове замовлення':
        return 1;
      case 'Підтверджено':
        return 2;
      case 'Скасовано клієнтом':
        return 3;
      case 'Скасовано магазином':
        return 4;
      case 'У дорозі':
        return 5;
      case 'Діставлено':
        return 6;
      case 'Прийнято клієнтом':
        return 7;
      default:
        return 0;
    }
  }
}
