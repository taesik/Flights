import { Component,OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {FlightService} from "../api/services/flight.service";
import {FlightRm} from "../api/models/flight-rm";
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit{

  constructor(private route:ActivatedRoute,
              private flightService:FlightService,
              private router:Router,
              private authService:AuthService,

              ) {

  }

  flightId:string = 'not loaded';
  flight: FlightRm = {}

  ngOnInit(): void {
    if (!this.authService.currentUser) {
      this.router.navigate(['/register-user']);
    }
    this.route
      .paramMap
      .subscribe((p)=> {
          this.findFlight(p.get("flightId"));
        }
      )
  }
  private findFlight = (flightId:string | null) => {
    this.flightId = flightId ?? 'not passed';
    this.flightService
      .findFlight({id:this.flightId})
      .subscribe(flight=> this.flight = flight,
          this.handleError
        )
  }
  private handleError = (error: any): void => {
    if(error.status ==400) {
      alert('Flight not found');
      // I must use Arrow function here not to set this.router undefined
      this.router
        .navigate(['/search-flights'])
        .then(r => {

      })
    }

    console.log('Response error Status: ' + error.status);
    console.log('Response error Status Text: ' + error.statusText);
    console.log(error, 'Error ');
  }
}
