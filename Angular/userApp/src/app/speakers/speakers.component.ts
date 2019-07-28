import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-speakers',
  templateUrl: './speakers.component.html',
  styleUrls: ['./speakers.component.css']
})
export class SpeakersComponent implements OnInit {
  isFriday: boolean;
  constructor() {
    this.isFriday = true;
   }

  ngOnInit() {
  }
  change() {
    this.isFriday = !this.isFriday;
  }

}
