import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public isThursday: boolean;
  public isFriday: boolean;
  public isSaturday: boolean;
  public isSunday: boolean;
  constructor() {
    this.isThursday = true;
    this.isFriday = false;
    this.isSaturday = false;
    this.isSunday = false;
  }

  ngOnInit() {
  }
  change(id: number) {
    switch (id) {
      case 0:
        this.isThursday = !this.isThursday;
        this.isFriday = false;
        this.isSaturday = false;
        this.isSunday = false;
        break;
      case 1:
        this.isThursday = false;
        this.isFriday = !this.isFriday;
        this.isSaturday = false;
        this.isSunday = false;
        break;
      case 2:
        this.isThursday = false;
        this.isFriday = false;
        this.isSaturday = !this.isSaturday;
        this.isSunday = false;
        break;
      case 3:
        this.isThursday = false;
        this.isFriday = false;
        this.isSaturday = false;
        this.isSunday = !this.isSunday;
        break;
      default:
        break;
    }
  }
}
