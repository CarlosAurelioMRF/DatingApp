import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor() { }

  ngOnInit() {
  }

  registerToggle(): void {
    this.registerMode = true;
  }

  cancelRegisterMode(registerMode: boolean): void {
    this.registerMode = registerMode;
  }
}
