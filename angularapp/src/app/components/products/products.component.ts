import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { Product } from '../../interfaces/product';
import { ProductService } from '../../services/product/product.service';
import { ConfirmComponent } from '../confirm/confirm.component';
import { Brand } from '../../interfaces/brand';
import { SubCategory } from '../../interfaces/subCategory';
import { SubCategoryService } from '../../services/subCategory/sub-category.service';
import { UkrainianMatPaginatorIntl } from '../brands/brands.component';
import { BrandService } from '../../services/brand/brandservice.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
  encapsulation: ViewEncapsulation.None,
  providers: [{ provide: MatPaginatorIntl, useClass: UkrainianMatPaginatorIntl }]
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  subCategories: SubCategory[] = [];
  searchQuery: string = '';
  showForm: boolean = false;
  product: Product = { id: 0, name: '', description: '', imageUrl: '', quantity: 0, price: 0, discount: 0, brandId: 0, brandName: '', brandImageUrl: '', subCategoryId: 0, subCategoryName: '', subCategoryImageUrl: '', features: [] };
  plusIcon: boolean = true;
  pageIndex: number = 0;
  pageSize: number = 5;
  totalItems: number = 0;
  pageSizeOptions: number[] = [5, 10, 20];
  sortDirection: 'asc' | 'desc' = 'asc';
  sortPriceDirection: 'asc' | 'desc' = 'asc';
  sortQuantityDirection: 'asc' | 'desc' = 'asc';

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private productService: ProductService, private brandService: BrandService, private subCategoryService: SubCategoryService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadProducts();
    this.loadBrands();
    this.loadSubCategories();
  }

  loadBrands(): void {
    this.brandService.getAllBrands().subscribe(
      (brands: Brand[]) => {
        this.brands = brands;
      }
    );
  }

  loadSubCategories(): void {
    this.subCategoryService.getAllSubCategories().subscribe(
      (subCategories: SubCategory[]) => {
        this.subCategories = subCategories;
      }
    );
  }

  loadProducts(): void {
    this.productService.getAllProducts().subscribe(
      (products: Product[]) => {
        this.products = products;
        this.totalItems = this.products.length;
        this.filterProducts();
        this.sortProductsByName();
        this.paginator.firstPage();
      }
    );
  }

  toggleSortDirection(): void {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortProductsByName();
  }

  toggleSortPriceDirection(): void {
    this.sortPriceDirection = this.sortPriceDirection === 'asc' ? 'desc' : 'asc';
    this.sortProductsByPrice();
  }

  toggleSortQuantityDirection(): void {
    this.sortQuantityDirection = this.sortQuantityDirection === 'asc' ? 'desc' : 'asc';
    this.sortProductsByQuantity();
  }

  sortProductsByPrice(): void {
    this.products.sort((a, b) => {
      if (this.sortPriceDirection === 'asc') {
        return a.price - b.price;
      } else {
        return b.price - a.price;
      }
    });
  }

  sortProductsByQuantity(): void {
    this.products.sort((a, b) => {
      if (this.sortQuantityDirection === 'asc') {
        return a.quantity - b.quantity;
      } else {
        return b.quantity - a.quantity;
      }
    });
  }

  sortProductsByName(): void {
    this.products.sort((a, b) => {
      const nameA = a.name.toUpperCase();
      const nameB = b.name.toUpperCase();
      if (this.sortDirection === 'asc') {
        return nameA.localeCompare(nameB);
      } else {
        return nameB.localeCompare(nameA);
      }
    });
  }

  handlePageEvent(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
  }

  toggleFormVisibility(): void {
    this.showForm = !this.showForm;
    this.plusIcon = !this.plusIcon;
  }

  closeForm(): void {
    this.toggleFormVisibility();
    this.resetForm();
  }

  deleteProduct(product: Product): void {
    this.productService.deleteProduct(product.id).subscribe(
      () => {
        this.products = this.products.filter(p => p.id !== product.id);
        if (this.product.id === product.id) {
          this.resetForm();
        }
      }
    );
  }

  openConfirmDialog(product: Product): void {
    const dialogRef = this.dialog.open(ConfirmComponent, {
      width: '250px',
      height: '220px',
      data: { item: product }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteProduct(product);
      }
    });
  }

  openAddProductDialog(): void {
    this.toggleFormVisibility();
  }

  editProduct(product: Product): void {
    if (!this.showForm) {
      this.toggleFormVisibility();
    }
    this.product = { ...product };
  }

  submitForm(): void {
    if (this.product.id === 0) {
      this.productService.createProduct(this.product).subscribe(
        (newProduct: Product) => {
          this.products.push(newProduct);
          this.loadProducts();
        }
      );
    } else {
      this.productService.updateProduct(this.product).subscribe(
        (updatedProduct: Product) => {
          if (updatedProduct && updatedProduct.id) {
            const index = this.products.findIndex(p => p.id === updatedProduct.id);
            if (index !== -1) {
              this.products[index] = updatedProduct;
            } else {
              console.error('Product not found in the array:', updatedProduct);
            }
          } else {
            console.error('Updated product or its id is null:', updatedProduct);
          }
        },
        error => {
          console.error('Error updating product:', error);
        }
      );
    }
  }

  filterProducts(): void {
    if (this.searchQuery) {
      this.products = this.products.filter(product =>
        product.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        product.description.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        product.brandName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        product.subCategoryName.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }
  }

  resetForm(): void {
    this.product = { id: 0, name: '', description: '', imageUrl: '', quantity: 0, price: 0, discount: 0, brandId: 0, brandName: '', brandImageUrl: '', subCategoryId: 0, subCategoryName: '', subCategoryImageUrl: '', features: [] }; 
  }

  updateSearchQuery(event: any): void {
    const value = event.target ? event.target.value : null;
    if (value !== null) {
      this.searchQuery = value;
      this.loadProducts();
    }
  }

  assignBrand(brand: Brand): void {
    if (this.product) {
      this.product.brandId = brand.id;
      this.product.brandName = brand.name;
      this.product.brandImageUrl = brand.image;
    }
  }

  assignSubCategory(subCategory: SubCategory): void {
    if (this.product) {
      this.product.subCategoryId = subCategory.id;
      this.product.subCategoryName = subCategory.name;
      this.product.subCategoryImageUrl = subCategory.imageUrl;
    }
  }

  handleFileInput(event: any): void {
    const file: File = event.target.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      this.product.imageUrl = reader.result as string;
    };

    reader.readAsDataURL(file);
  }

  increaseQuantity() {
    this.product.quantity++;
  }

  decreaseQuantity() {
    if (this.product.quantity > 0) {
      this.product.quantity--;
    }
  }

  addFeature(): void {
    this.product.features.push({ name: '', value: '' }); 
  }

  removeFeature(index: number): void { 
    this.product.features.splice(index, 1);
  }

}
