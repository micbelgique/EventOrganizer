import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-speakers',
  templateUrl: './speakers.component.html',
  styleUrls: ['./speakers.component.css']
})
export class SpeakersComponent implements OnInit {
  isVendredi: boolean;
  constructor() {
    this.isVendredi = true;
   }

  ngOnInit() {
  }
  change() {
    this.isVendredi = !this.isVendredi;
  }

}
