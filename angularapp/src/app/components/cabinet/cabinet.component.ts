import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cabinet',
  templateUrl: './cabinet.component.html',
  styleUrls: ['./cabinet.component.css']
})
export class CabinetComponent implements OnInit {
  isAdmin: boolean = false;

ngOnInit(): void {
    this.isAdmin = localStorage.getItem('userRole') === 'Admin';
  }

}

