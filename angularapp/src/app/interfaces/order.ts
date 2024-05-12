export interface Order {
  email: string;
  firstName: string;
  lastName: string;
  phone: string;
  address: string;
city: string;
  paymentMethodId: number;
  deliveryMethodId: number;
  orderStatusId: number;
  orderDate: Date;
  orderAddress: string;
  total: number;
  cartItems: { id: number; quantity: number }[];
}
