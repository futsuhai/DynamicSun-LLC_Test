import { Injectable } from '@angular/core';
import { RestService } from './rest.service';
import { Observable } from 'rxjs';
import { IWeatherDate } from '../models/WeatherDate.model';
import { IWeather } from '../models/Weather.model';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  get api(): string {
    return `/api/weather`;
  }

  constructor(private restService: RestService) { }

  public createWeathersFromFiles(files: File[]): Observable<void> {
    const endpoint: string = `${this.api}/createWeathersFromFiles`;
    return this.restService.restPOST<void>(endpoint, files);
  }

  public getWeatherWithDate(weatherDate: IWeatherDate): Observable<IWeather[]> {
    const endpoint: string = `${this.api}/getWeatherWithDate`;
    return this.restService.restPUT<IWeather[]>(endpoint, weatherDate);
  }

  public createWether(weather: IWeather): Observable<void> {
    const endpoint: string = `${this.api}/createWeather`;
    return this.restService.restPOST<void>(endpoint, weather);
  }

  public createWeatherRange(weathers: IWeather[]): Observable<void> {
    const endpoint: string = `${this.api}/createWeatherRange`;
    return this.restService.restPOST<void>(endpoint, weathers);
  }
}
