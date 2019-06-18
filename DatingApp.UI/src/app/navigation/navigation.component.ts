import { Component, OnInit } from '@angular/core';

import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavigationComponent implements OnInit {

  model: any = {};

  constructor(public authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  login(): void {
    this.authService.login(this.model)
      .subscribe(
        next => this.alertifyService.success('Logged in successfully'),
        error => this.alertifyService.error(error)
      );
  }

  loggedIn(): boolean {
    return this.authService.loggedIn();
  }

  logout(): void {
    localStorage.removeItem('token');
    this.alertifyService.message('Logged out');
  }

}
