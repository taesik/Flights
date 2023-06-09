import { Component, OnInit } from '@angular/core';
import {FlightRm} from "../api/models/flight-rm";
import {FlightService} from "../api/services/flight.service";

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit{
  searchResult:any =[
  ]
  constructor(private flightService:FlightService) {

  }
  ngOnInit(): void {
  }

  search(): void {
    this.flightService
      .searchFlight({})
      .subscribe(response=>{
         this.searchResult = response,
           this.handleError
      })
  }

  private handleError(error: any): void {
    console.log('Response error Status: ' + error.status);
    console.log('Response error Status Text: ' + error.statusText);
    console.log(error, 'Error ');
  }

}
