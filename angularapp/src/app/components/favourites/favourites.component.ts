import { Component } from '@angular/core';
import { FavouritesService } from '../../services/favourites/favourites.service';
import { SimplifiedProduct } from '../../interfaces/simplified-product';
import { Router } from '@angular/router';

@Component({
  selector: 'app-favourites',
  templateUrl: './favourites.component.html',
  styleUrls: ['./favourites.component.css']
})
export class FavouritesComponent {
  favoriteProducts: SimplifiedProduct[] = [];

  constructor(private favouritesService: FavouritesService, private router: Router) { }

  ngOnInit(): void {
    this.loadFavoriteProducts();
  }

  loadFavoriteProducts(): void {
    this.favouritesService.getFavoriteProducts().subscribe((products: SimplifiedProduct[]) => {
      this.favoriteProducts = products;
    });
  }

  removeFromFavorites(product: SimplifiedProduct): void {
    this.favouritesService.deleteFromFavourites(product.id).subscribe(() => {
      this.favoriteProducts = this.favoriteProducts.filter(p => p.id !== product.id);
    });
  }

  redirectToProductPage(productId: number): void {
    this.router.navigate(['/products', productId]);
  }
}
