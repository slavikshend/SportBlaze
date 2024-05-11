import { Component } from '@angular/core';
import { ProductService } from '../../services/product/product.service';
import { FavouritesService } from '../../services/favourites/favourites.service';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { CartService } from '../../services/cart/cart.service';
import { Product } from '../../interfaces/product';
import { SimplifiedProduct } from '../../interfaces/simplified-product';
import { CartItem } from '../../interfaces/cart-item';
import { Router } from '@angular/router';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.css']
})
export class BodyComponent {

  specialOfferProducts: any[] = [];
  showCart: boolean = false; 
  constructor(
    private productService: ProductService,
    private favouritesService: FavouritesService,
    private dialog: MatDialog,
    private cartService: CartService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.loadSpecialOfferProducts();
    } else {
      this.loadSpecialOfferProductsAnon();
    }
  }

  loadSpecialOfferProducts() {
    this.productService.getSpecialOfferProducts().subscribe((products: any[]) => {
      this.specialOfferProducts = products;
      this.logProducts(products);
    });
  }

  loadSpecialOfferProductsAnon() {
    this.productService.getSpecialOfferProductsAnon().subscribe((products: any[]) => {
      this.specialOfferProducts = products;
      this.logProducts(products);
    });
  }

  logProducts(products: any[]) {
    products.forEach(product => {
      console.log('Product:', product);
    });
  }

  toggleFavourite(product: any) {
    const token = localStorage.getItem('token');
    if (!token) {
      this.openLoginDialog();
      return;
    }

    if (product.isFavourite) {
      this.removeFromFavourites(product);
    } else {
      this.addToFavourites(product);
    }
  }

  addToFavourites(product: any) {
    this.favouritesService.addToFavourites(product.id).subscribe(() => {
      product.isFavourite = true;
    });
  }

  removeFromFavourites(product: any) {
    this.favouritesService.deleteFromFavourites(product.id).subscribe(() => {
      product.isFavourite = false;
    });
  }

  openLoginDialog(): void {
    this.dialog.open(LoginComponent, {
      width: '400px',
      height: '385px',
      autoFocus: false
    });
  }

  addToCart(product: SimplifiedProduct): void {
    const cartItem: CartItem = {
      id: product.id,
      name: product.name,
      quantity: 1,
      imageUrl: product.imageUrl,
      discount: product.discount,
      price: product.price
    };

    this.cartService.addToCart(cartItem);
    this.showCart = true;
  }
  redirectToProductPage(productId: number): void {
    this.router.navigate(['/products', productId]);
  }

  calculateDiscountedPrice(price: number, discount: number): number {
    return price - (price * (discount / 100));
  }
}
