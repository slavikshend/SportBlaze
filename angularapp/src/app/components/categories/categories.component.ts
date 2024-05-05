import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Category } from '../../interfaces/category';
import { CategoryService } from '../../services/category/category.service';
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
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
  encapsulation: ViewEncapsulation.None,
  providers: [{ provide: MatPaginatorIntl, useClass: UkrainianMatPaginatorIntl }]
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  searchQuery: string = '';
  showForm: boolean = false;
  category: Category = { id: 0, name: '', image: '' };
  plusIcon: boolean = true;
  pageIndex: number = 0;
  pageSize: number = 5;
  totalItems: number = 0;
  pageSizeOptions: number[] = [5, 10, 20];
  sortDirection: 'asc' | 'desc' = 'asc';

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private categoryService: CategoryService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe(
      (categories: Category[]) => {
        this.categories = categories;
        this.totalItems = this.categories.length;
        this.filterCategories();
        this.sortCategoriesByName();
        this.paginator.firstPage();
      }
    );
  }

  toggleSortDirection(): void {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortCategoriesByName();
  }

  sortCategoriesByName(): void {
    this.categories.sort((a, b) => {
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

  deleteCategory(category: Category): void {
    this.categoryService.deleteCategory(category.id).subscribe(
      () => {
        this.categories = this.categories.filter(c => c.id !== category.id);
        if (this.category.id === category.id) {
          this.resetForm();
        }
      }
    );
  }

  openAddCategoryDialog(): void {
    this.toggleFormVisibility();
  }

  editCategory(category: Category): void {
    if (!this.showForm) {
      this.toggleFormVisibility();
    }
    this.category = { ...category };
  }

  submitForm(): void {
    if (this.category.id === 0) {
      this.categoryService.addCategory(this.category).subscribe(
        (newCategory: Category) => {
          this.categories.push(newCategory);
          this.loadCategories();
        }
      );
    } else {
      this.categoryService.editCategory(this.category).subscribe(
        (updatedCategory: Category) => {
          const index = this.categories.findIndex(c => c.id === updatedCategory.id);
          if (index !== -1) {
            this.categories[index] = updatedCategory;
          } else {
            console.error('Category not found in the array:', updatedCategory);
          }
        }
      );
    }
  }


  openConfirmDialog(category: Category): void {
    this.category = { ...category };
    const dialogRef = this.dialog.open(ConfirmComponent, {
      width: '250px',
      height: '220px',
      data: { item: this.category }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteCategory(this.category);
      }
    });
  }

  resetForm(): void {
    this.category = { id: 0, name: '', image: '' };
  }

  handleFileInput(event: any): void {
    const file: File = event.target.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      this.category.image = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  filterCategories(): void {
    if (this.searchQuery) {
      this.categories = this.categories.filter(category =>
        category.name.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }
  }

  updateSearchQuery(event: any): void {
    const value = event.target ? event.target.value : null;
    if (value !== null) {
      this.searchQuery = value;
      this.loadCategories();
    }
  }
}
