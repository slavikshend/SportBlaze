import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product/product.service';
import { FeedbackService } from '../../services/feedback/feedback.service';
import { ProductDetails } from '../../interfaces/product-details';
import { Feedback } from '../../interfaces/feedback';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  productId: number = 0;
  product: ProductDetails = {
    id: 0,
    name: '',
    description: '',
    imageUrl: '',
    quantity: 0,
    price: 0,
    discount: 0,
    brand: '',
    subCategory: '',
    feedbacks: [],
    features: []
  };
  averageRating: number = 0;
  newFeedback: Feedback = { rating: 0, comment: '', date: new Date(), email: '' }; // Use the Feedback interface

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private feedbackService: FeedbackService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.productId = +params['id'];
      this.getProductDetails(this.productId);
    });

    const userEmail = localStorage.getItem('userEmail');
    if (userEmail) {
      this.newFeedback.email = userEmail;
    }

    this.getAllProductFeedbacks();
  }

  getProductDetails(id: number): void {
    this.productService.getProductDetailsById(id).subscribe(
      (product: ProductDetails) => {
        this.product = product;
        this.calculateAverageRating();
      },
      (error) => {
        console.error('Error fetching product details:', error);
      }
    );
  }

  calculateAverageRating(): void {
    let totalRating = 0;
    this.product.feedbacks.forEach(feedback => {
      totalRating += feedback.rating;
    });
    this.averageRating = totalRating / this.product.feedbacks.length;
  }

  getStarIcon(index: number): string {
    return index <= this.averageRating ? 'star' : 'star_border';
  }

  calculateDiscountedPrice(originalPrice: number, discount: number): number {
    return originalPrice - (originalPrice * (discount / 100));
  }

  addToCart(): void {
    console.log('Product added to cart:', this.product);
  }

  setRating(rating: number): void {
    this.newFeedback.rating = rating;
    console.log('Rating:', rating);
  }

  submitFeedback(): void {
    this.newFeedback.date = new Date();
    this.feedbackService.addFeedback(this.productId, this.newFeedback).subscribe(
      (addedFeedback: Feedback) => {
        this.product.feedbacks.push(addedFeedback);
        this.calculateAverageRating();
        this.newFeedback = { rating: 0, comment: '', date: new Date(), email: this.newFeedback.email };
      },
      (error) => {
        console.error('Error adding feedback:', error);
      }
    );
  }

  getAllProductFeedbacks(): void {
    this.feedbackService.getAllProductFeedbacks(this.productId).subscribe(
      (feedbacks: Feedback[]) => {
        this.product.feedbacks = feedbacks;
        console.log(this.product.feedbacks);
        this.calculateAverageRating();
      },
      (error: any) => {
        console.error('Error fetching feedbacks:', error);
      }
    );
  }
}
