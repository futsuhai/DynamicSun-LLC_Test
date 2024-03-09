import { Injectable } from '@angular/core';
import { RestService } from './rest.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  get api(): string {
    return `/api/weather`;
  }

  constructor(private restService: RestService) { }

  public ping(): Observable<void> {
    const endpoint: string = `${this.api}/ping`;
    return this.restService.restGET<void>(endpoint);
  }
}
