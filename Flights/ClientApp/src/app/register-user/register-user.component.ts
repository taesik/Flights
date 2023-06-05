import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { UserService } from '../api/services';
import {Log} from "oidc-client";

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  constructor(private userService: UserService,
              private fb: FormBuilder
              ) {

  }

  form =
    this.fb.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true],
  })

  ngOnInit(): void {
  }

  register(): void {
    console.log('Form Values : ', this.form.value);
    this.userService.registerUser(
      {
        body: this.form.value
      }
    ).subscribe(_=> {
      console.log('form posted to server');
    })
  }



}
