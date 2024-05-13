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

  updateOrderStatus(order: OrderModel, statusId: number): void {
    console.log('Changing order status:', order.id, statusId);
    this.orderService.ChangeOrderStatus(order.id, statusId).subscribe(
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
}
