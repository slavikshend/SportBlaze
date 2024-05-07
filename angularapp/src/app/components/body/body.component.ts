import { Component } from '@angular/core';
import { ProductService } from '../../services/product/product.service';
import { FavouritesService } from '../../services/favourites/favourites.service';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.css']
})
export class BodyComponent {

  specialOfferProducts: any[] = [];

  constructor(
    private productService: ProductService,
    private favouritesService: FavouritesService,
    private dialog: MatDialog
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

}
