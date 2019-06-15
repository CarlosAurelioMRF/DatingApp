import { Component, EventEmitter, OnInit, Output } from '@angular/core';

import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styles: []
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();

  model: any = {};

  constructor(private authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model)
      .subscribe(() => this.alertifyService.success('registration successfully'), error => this.alertifyService.error(error));
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
