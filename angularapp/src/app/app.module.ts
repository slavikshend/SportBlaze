import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field'; 
import { MatInputModule } from '@angular/material/input'; 
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { FormsModule } from '@angular/forms';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { CabinetComponent } from './components/cabinet/cabinet.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FooterComponent } from './components/footer/footer.component';
import { BodyComponent } from './components/body/body.component';
import { AppRoutingModule } from './routing/routing.module';
import { MatMenuModule } from '@angular/material/menu';
import { TokenInterceptor } from './token.interceptor';
import { PasswordChangeComponent } from './components/password-change/password-change.component';
import { ProfileComponent } from './components/profile/profile.component';
import { BrandsComponent } from './components/brands/brands.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatPaginatorModule } from '@angular/material/paginator';
import { ConfirmComponent } from './components/confirm/confirm.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { SubCategoriesComponent } from './components/sub-categories/sub-categories.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { ProductsComponent } from './components/products/products.component';
import { MatStepperModule } from '@angular/material/stepper';
import { FavouritesComponent } from './components/favourites/favourites.component';
import { DetailsComponent } from './components/details/details.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CartComponent } from './components/cart/cart.component';
import { OrderComponent } from './components/order/order.component';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { PaymentComponent } from './components/payment/payment.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegistrationComponent,
    NotfoundComponent,
    FooterComponent,
    BodyComponent,
    CabinetComponent,
PasswordChangeComponent,
ProfileComponent,
BrandsComponent,
ConfirmComponent,
SubCategoriesComponent,
CategoriesComponent,
SubCategoriesComponent,
ProductsComponent,
FavouritesComponent,
DetailsComponent,
CartComponent,
OrderComponent,
PaymentComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatDialogModule,
    MatToolbarModule,
    HttpClientModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatMenuModule,
    MatGridListModule,
    MatStepperModule,
    NgbModule,
    MatSelectModule,
    MatFormFieldModule,
    MatOptionModule
    
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
