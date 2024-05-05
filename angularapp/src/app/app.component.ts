import { HttpClient } from '@angular/common/http';
import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'angularapp';
  showScrollToTopBtn: boolean = false;

  @HostListener('window:scroll', [])
  onWindowScroll() {
    this.showScrollToTopBtn = window.scrollY > window.innerHeight;
  }
  scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
