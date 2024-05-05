import { Component } from '@angular/core';
import { ProductService } from '../../services/product/product.service';
import { FavouritesService } from '../../services/favourites/favourites.service';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.css']
})
export class BodyComponent {

  specialOfferProducts: any[] = [];

  constructor(
    private productService: ProductService,
    private favouritesService: FavouritesService
  ) { }

  ngOnInit(): void {
    this.loadSpecialOfferProducts();
  }

  loadSpecialOfferProducts() {
    this.productService.getSpecialOfferProducts().subscribe((products: any[]) => {
      this.specialOfferProducts = products;
    });
  }

  toggleFavourite(product: any) {
    if (product.favorite) {
      this.removeFromFavourites(product);
    } else {
      this.addToFavourites(product);
    }
  }

  addToFavourites(product: any) {
    this.favouritesService.addToFavourites(product.id).subscribe(() => {
      product.favorite = true;
    });
  }

  removeFromFavourites(product: any) {
    this.favouritesService.deleteFromFavourites(product.id).subscribe(() => {
      product.favorite = false;
    });
  }
}
