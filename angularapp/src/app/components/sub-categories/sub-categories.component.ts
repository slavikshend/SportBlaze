import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { SubCategory } from '../../interfaces/subCategory';
import { SubCategoryService } from '../../services/subCategory/sub-category.service';
import { UkrainianMatPaginatorIntl } from '../brands/brands.component';
import { ConfirmComponent } from '../confirm/confirm.component';
import { Category } from '../../interfaces/category';
import { CategoryService } from '../../services/category/category.service';

@Component({
  selector: 'app-sub-categories',
  templateUrl: './sub-categories.component.html',
  styleUrls: ['./sub-categories.component.css'],
  encapsulation: ViewEncapsulation.None,
  providers: [{ provide: MatPaginatorIntl, useClass: UkrainianMatPaginatorIntl }]
})
export class SubCategoriesComponent implements OnInit {
  subCategories: SubCategory[] = [];
  categories: Category[] = [];
  searchQuery: string = '';
  showForm: boolean = false;
  showCategorySelection: boolean = false;
  subCategory: SubCategory = { id: 0, name: '', description: '', imageUrl: '', categoryName: '', categoryId: 0, categoryImageUrl: '' };
  plusIcon: boolean = true;
  pageIndex: number = 0;
  pageSize: number = 5;
  totalItems: number = 0;
  pageSizeOptions: number[] = [5, 10, 20];
  sortDirection: 'asc' | 'desc' = 'asc';

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private categoryService: CategoryService, private subCategoryService: SubCategoryService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadSubCategories();
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe(
      (categories: Category[]) => {
        this.categories = categories;
      }
    );
  }

  loadSubCategories(): void {
    this.subCategoryService.getAllSubCategories().subscribe(
      (subCategories: SubCategory[]) => {
        this.subCategories = subCategories;
        this.totalItems = this.subCategories.length;
        this.filterSubCategories();
        this.sortSubCategoriesByName();
        this.paginator.firstPage();
      }
    );
  }

  toggleSortDirection(): void {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortSubCategoriesByName();
  }

  sortSubCategoriesByName(): void {
    this.subCategories.sort((a, b) => {
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

  deleteSubCategory(subCategory: SubCategory): void {
    this.subCategoryService.deleteSubCategory(subCategory.id).subscribe(
      () => {
        this.subCategories = this.subCategories.filter(s => s.id !== subCategory.id);
        if (this.subCategory.id === subCategory.id) {
          this.resetForm();
        }
      }
    );
  }

  assignCategory(category: Category): void {
    if (this.subCategory) {
      this.subCategory.categoryId = category.id;
      this.subCategory.categoryName = category.name;
      this.subCategory.categoryImageUrl = category.image;
      console.log()
      this.closeCategorySelection();
    }
  }

  openConfirmDialog(subCategory: SubCategory): void {
    const dialogRef = this.dialog.open(ConfirmComponent, {
      width: '250px',
      height: '220px',
      data: { item: subCategory }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteSubCategory(subCategory);
      }
    });
  }

  openAddSubCategoryDialog(): void {
    this.toggleFormVisibility();
  }

  editSubCategory(subCategory: SubCategory): void {
    if (!this.showForm) {
      this.toggleFormVisibility();
    }
    this.subCategory = { ...subCategory };
  }

  submitForm(): void {
    if (this.subCategory.id === 0) {
      this.subCategoryService.createSubCategory(this.subCategory).subscribe(
        (newSubCategory: SubCategory) => {
          this.subCategories.push(newSubCategory);
          this.loadSubCategories();
        }
      );
    } else {
      this.subCategoryService.updateSubCategory(this.subCategory).subscribe(
        (updatedSubCategory: SubCategory) => {
          if (updatedSubCategory && updatedSubCategory.id) {
            const index = this.subCategories.findIndex(s => s.id === updatedSubCategory.id);
            if (index !== -1) {
              this.subCategories[index] = updatedSubCategory;
            } else {
              console.error('SubCategory not found in the array:', updatedSubCategory);
            }
          } else {
            console.error('Updated subCategory or its id is null:', updatedSubCategory);
          }
        },
        error => {
          console.error('Error updating subCategory:', error);
        }
      );
    }
  }

  filterSubCategories(): void {
    if (this.searchQuery) {
      this.subCategories = this.subCategories.filter(subCategory =>
        subCategory.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        subCategory.description.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        subCategory.categoryName.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }
  }

  resetForm(): void {
    this.subCategory = { id: 0, name: '', description: '', imageUrl: '', categoryName: '', categoryId: 0, categoryImageUrl: '' };
  }

  updateSearchQuery(event: any): void {
    const value = event.target ? event.target.value : null;
    if (value !== null) {
      this.searchQuery = value;
      this.loadSubCategories();
    }
  }

  openCategorySelection(subCategory: SubCategory): void {
    this.subCategory = subCategory;
    this.showCategorySelection = true;
  }

  closeCategorySelection(): void {
    this.showCategorySelection = false;
  }

  handleFileInput(event: any): void {
    const file: File = event.target.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      this.subCategory.imageUrl = reader.result as string;
    };

    reader.readAsDataURL(file);
  }
}
