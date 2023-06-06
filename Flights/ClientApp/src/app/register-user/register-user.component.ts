import {Component, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {UserService} from '../api/services';
import {AuthService} from '../auth/auth.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  constructor(private userService: UserService,
              private fb: FormBuilder,
              private authService: AuthService,
              private router: Router,
  ) {

  }

  form =
    this.fb.group({
      email: ['', Validators.compose([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
      ])],
      firstName: ['', Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(35),
      ])],
      lastName: ['', Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(35),
      ])],
      isFemale: [true, Validators.required],
    })

  ngOnInit(): void {

  }

  checkUser(): void {
    const params =
      {email: this.form.get('email')?.value ?? 'cannot found'}

    this.userService
      .findUser(params)
      .subscribe(
        this.login, e => {
          if (e.status != 404) {
            console.error('e from checkUser() : ', e)
          }
        })
  }

  register(): void {

    if (this.form.invalid) return;

    console.log('Form Values : ', this.form.value);
    this.userService
      .registerUser(
        {
          body: this.form.value
        }
      )
      .subscribe(
        this.login, console.error
      );
  }

  private login = () => {
    this.authService.loginUser({email: this.form.get('email')?.value ?? 'cannot found '})
    this.router.navigate(['/search-flights'])
  }
}
