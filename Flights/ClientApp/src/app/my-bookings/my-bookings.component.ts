import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {BookingRm} from "../api/models/booking-rm";
import {BookingService} from '../api/services';
import {AuthService} from '../auth/auth.service';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent implements OnInit {

  bookings!: BookingRm[];

  constructor(
    private bookingService: BookingService,
    private authService: AuthService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.bookingService.listBooking({email: this.authService.currentUser?.email ?? ''})
      .subscribe(r => this.bookings = r, this.handleError);
  }

  private handleError(err: any) {
    console.log('Response Error, Status: ', err.status);
    console.log('Response Error, Status Text: ', err.statusText);
    console.log(err);
  }
}
