import { Component, OnInit, ViewChild } from '@angular/core';
import { OrderItemModel, OrderModel } from '../../interfaces/order-model';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { OrderService } from '../../services/order/order.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html',
  styleUrls: ['./all-orders.component.css']
})
export class AllOrdersComponent implements OnInit {
  orders: OrderModel[] = [];
  searchQuery: string = '';
  pageIndex: number = 0;
  pageSize: number = 5;
  totalItems: number = 0;
  pageSizeOptions: number[] = [5, 10, 20];
  sortDirection: 'asc' | 'desc' = 'asc';
  sortField: string = 'total';
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

  constructor(private orderService: OrderService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.orderService.getAllOrders().subscribe(
      (orders: OrderModel[]) => {
        this.orders = orders;
        console.log('Orders:', this.orders);
        this.totalItems = this.orders.length;
        this.filterOrders();
        this.sortOrdersByDate();
        this.paginator.firstPage();
      }
    );
  }

  toggleSortDirection(): void {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortOrdersByDate();
  }

  sortOrdersByDate(): void {
    this.orders.sort((a, b) => {
      const dateA = new Date(a.orderDate);
      const dateB = new Date(b.orderDate);
      if (this.sortDirection === 'asc') {
        return dateA.getTime() - dateB.getTime();
      } else {
        return dateB.getTime() - dateA.getTime();
      }
    });
  }

  handlePageEvent(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
  }

  updateOrderStatus(order: OrderModel, statusText: string): void {
    console.log('Changing order status:', order.id, statusText);
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
    if(this.searchQuery) {
    this.orders = this.orders.filter(order =>
      order.id.toString().includes(this.searchQuery)
    );
  }
}

updateSearchQuery(event: any): void {
  const value = event.target ? event.target.value : null;
  if(value !== null) {
  this.searchQuery = value;
  this.loadOrders();
}
}

  sortByTotal(): void {
    this.sortField = 'total';
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortOrders();
  }

  sortOrders(): void {
    this.orders.sort((a, b) => {
      if (this.sortField === 'total') {
        return this.sortDirection === 'asc' ? a.total - b.total : b.total - a.total;
      } else if (this.sortField === 'orderDate') {
        const dateA = new Date(a.orderDate);
        const dateB = new Date(b.orderDate);
        return this.sortDirection === 'asc' ? dateA.getTime() - dateB.getTime() : dateB.getTime() - dateA.getTime();
      }
      return 0;
    });
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
