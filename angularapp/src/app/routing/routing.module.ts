import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotfoundComponent } from '../components/notfound/notfound.component';
import { BodyComponent } from '../components/body/body.component';
import { CabinetComponent } from '../components/cabinet/cabinet.component';
import { ProfileComponent } from '../components/profile/profile.component'; 
import { BrandsComponent } from '../components/brands/brands.component';
import { CategoriesComponent } from '../components/categories/categories.component';
import { SubCategoriesComponent } from '../components/sub-categories/sub-categories.component';
import { ProductsComponent } from '../components/products/products.component';
import { FavouritesComponent } from '../components/favourites/favourites.component';
import { DetailsComponent } from '../components/details/details.component';
import { OrderComponent } from '../components/order/order.component';
import { PaymentComponent } from '../components/payment/payment.component';
import { PaymentSuccessComponent } from '../components/payment-success/payment-success.component';
import { ShopLogComponent } from '../components/shop-log/shop-log.component';
import { AllOrdersComponent } from '../components/all-orders/all-orders.component';
import { MyOrdersComponent } from '../components/myorders/myorders.component';

const routes: Routes = [
  { path: '', component: BodyComponent },
  {
    path: 'cabinet',
    component: CabinetComponent,
    children: [
      { path: '', redirectTo: 'profile', pathMatch: 'full' },
      { path: 'profile', component: ProfileComponent },
      { path: 'brands', component: BrandsComponent },
      { path: 'categories', component: CategoriesComponent },
      { path: 'subcategories', component: SubCategoriesComponent },
      { path: 'products', component: ProductsComponent },
      { path: 'favourites', component: FavouritesComponent },
      { path: 'shoplog', component: ShopLogComponent },
      { path: 'allorders', component: AllOrdersComponent },
      { path: 'myorders', component: MyOrdersComponent },
    ]
  },
  { path: 'products/:id', component: DetailsComponent },
  { path: 'payment', component: PaymentComponent },
  { path: 'makeorder', component: OrderComponent },
  { path: 'paymentsuccess', component: PaymentSuccessComponent },
  { path: '**', component: NotfoundComponent },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
