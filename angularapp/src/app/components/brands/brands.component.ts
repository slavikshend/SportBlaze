import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Brand } from '../../interfaces/brand';
import { BrandService } from '../../services/brand/brandservice.service';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { ConfirmComponent } from '../confirm/confirm.component';

export class UkrainianMatPaginatorIntl extends MatPaginatorIntl {
  override itemsPerPageLabel = 'Елементів на сторінці:';
  override nextPageLabel = 'Наступна';
  override previousPageLabel = 'Попередня';
  override firstPageLabel = 'Перша';
  override lastPageLabel = 'Остання';

  override getRangeLabel = (page: number, pageSize: number, length: number): string => {
    if (length === 0 || pageSize === 0) {
      return `0 з ${length}`;
    }
    length = Math.max(length, 0);
    const startIndex = page * pageSize;
    const endIndex = startIndex < length ?
      Math.min(startIndex + pageSize, length) :
      startIndex + pageSize;
    return `${startIndex + 1} - ${endIndex} з ${length}`;
  };
}

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.css'],
  encapsulation: ViewEncapsulation.None,
  providers: [{ provide: MatPaginatorIntl, useClass: UkrainianMatPaginatorIntl }]

})
export class BrandsComponent implements OnInit {
  brands: Brand[] = [];
  searchQuery: string = '';
  showForm: boolean = false;
  brand: Brand = { id: 0, name: '', image: '' };
  plusIcon: boolean = true;
  pageIndex: number = 0;
  pageSize: number = 5;
  totalItems: number = 0;
  pageSizeOptions: number[] = [5, 10, 20];
  sortDirection: 'asc' | 'desc' = 'asc';

  @ViewChild(MatPaginator)
    paginator!: MatPaginator;
  constructor(private brandService: BrandService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadBrands();
  }

  loadBrands(): void {
    this.brandService.getAllBrands().subscribe(
      (brands: Brand[]) => {
        this.brands = brands;
        this.totalItems = this.brands.length;
        this.filterBrands();
        this.sortBrandsByName();
        this.paginator.firstPage();
      }
    );
  }

  toggleSortDirection(): void {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortBrandsByName();
  }

  sortBrandsByName(): void {
    this.brands.sort((a, b) => {
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

  opecloseForm(): void {
    this.toggleFormVisibility();
    this.resetForm();
  }


  deleteBrand(brand: Brand): void {
      this.brandService.deleteBrand(brand.id).subscribe(
        () => {
          this.brands = this.brands.filter(b => b.id !== brand.id);
          if (this.brand.id === brand.id) {
            this.resetForm();
          }
        }
      );
  }

  openAddBrandDialog(): void {
    this.toggleFormVisibility();
  }

  editBrand(brand: Brand): void {
    if (!this.showForm) {
      this.toggleFormVisibility();
    }
    this.brand = { ...brand };
  }

  submitForm(): void {
    if (this.brand.id === 0) {
      this.brandService.addBrand(this.brand).subscribe(
        (newBrand: Brand) => {
          this.brands.push(newBrand);
          this.loadBrands();
        }
      );
    } else {
      this.brandService.editBrand(this.brand).subscribe(
        (updatedBrand: Brand) => {
          if (updatedBrand && updatedBrand.id) {
            const index = this.brands.findIndex(b => b.id === updatedBrand.id);
            if (index !== -1) {
              this.brands[index] = updatedBrand;
            } else {
              console.error('Brand not found in the array:', updatedBrand);
            }
          } else {
            console.error('Updated brand or its id is null:', updatedBrand);
          }
        },
        error => {
          console.error('Error updating brand:', error);
        }
      );
    }
  }

  openConfirmDialog(brand: Brand): void {
    this.brand = { ...brand };
    const dialogRef = this.dialog.open(ConfirmComponent, {
      width: '250px',
      height: '220px',
      data: { item: this.brand }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteBrand(this.brand);
      }
    });
  }

  resetForm(): void {
    this.brand = { id: 0, name: '', image: '' };
  }

  handleFileInput(event: any): void {
    const file: File = event.target.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      this.brand.image = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  filterBrands(): void {
    if (this.searchQuery) {
      this.brands = this.brands.filter(brand =>
        brand.name.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }
  }

  updateSearchQuery(event: any): void {
    const value = event.target ? event.target.value : null;
    if (value !== null) {
      this.searchQuery = value;
      this.loadBrands();
    }
  }
}
