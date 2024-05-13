export interface OrderModel {
  id: number;
  userEmail: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  deliveryAddress: string;
  deliveryName: string;
  paymentName: string;
  status: string;
  orderDate: Date;
  isPaymentSuccessful: boolean;
  total: number;
  orderItems: OrderItemModel[];
}

export interface OrderItemModel {
  name: string;
price: number;
  quantity: number;
}
