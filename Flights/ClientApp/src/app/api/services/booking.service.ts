/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { BookingRm } from '../models/booking-rm';

@Injectable({
  providedIn: 'root',
})
export class BookingService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation listBooking
   */
  static readonly ListBookingPath = '/Booking/{email}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `listBooking$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  listBooking$Plain$Response(params: {
    email: string;
  }): Observable<StrictHttpResponse<Array<BookingRm>>> {

    const rb = new RequestBuilder(this.rootUrl, BookingService.ListBookingPath, 'get');
    if (params) {
      rb.path('email', params.email, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<BookingRm>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `listBooking$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  listBooking$Plain(params: {
    email: string;
  }): Observable<Array<BookingRm>> {

    return this.listBooking$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<Array<BookingRm>>) => r.body as Array<BookingRm>)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `listBooking()` instead.
   *
   * This method doesn't expect any request body.
   */
  listBooking$Response(params: {
    email: string;
  }): Observable<StrictHttpResponse<Array<BookingRm>>> {

    const rb = new RequestBuilder(this.rootUrl, BookingService.ListBookingPath, 'get');
    if (params) {
      rb.path('email', params.email, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<BookingRm>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `listBooking$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  listBooking(params: {
    email: string;
  }): Observable<Array<BookingRm>> {

    return this.listBooking$Response(params).pipe(
      map((r: StrictHttpResponse<Array<BookingRm>>) => r.body as Array<BookingRm>)
    );
  }

}
