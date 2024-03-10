import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherService } from 'src/app/services/weather.service';
import { IWeather } from 'src/app/models/Weather.model';
import { IWeatherDate } from 'src/app/models/WeatherDate.model';
import { format } from 'date-fns';
import { ru } from 'date-fns/locale';
import { IWeatherParams } from 'src/app/models/WeatherParams.model';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
  host: {
    class: 'main-page-component'
  }
})
export class MainPageComponent implements OnInit {

  public testDate: IWeatherDate = {
    date: new Date(Date.UTC(2010, 0, 4, 0, 0, 0))
  };
  public weathers: IWeather[] = [];
  public testWeathers: IWeather[] = [];
  public activeWeatherIndex: number = 0;

  public weatherParams: IWeatherParams[] = [];

  constructor(private weatherService: WeatherService) { }

  public ngOnInit(): void {
    this.weatherService.getWeatherWithDate(this.testDate).subscribe({
      next: (weathers: IWeather[]) => {
        if (weathers) {
          this.weathers = weathers;
          this.setWeatherParams(this.weathers[this.activeWeatherIndex]);
        }
      }
    });
  }

  public toggleActive(index: number) {
    this.activeWeatherIndex = index;
    this.setWeatherParams(this.weathers[this.activeWeatherIndex]);
  }

  public formatDate(date: Date): string {
    return format(date, 'd MMMM yyyy', { locale: ru });
  }

  private setWeatherParams(activeWeather: IWeather) {
    this.weatherParams = [
      { name: 'Влажность, %', value: activeWeather.humidity },
      { name: 'Точка росы, гр. Ц.', value: activeWeather.td },
      { name: 'Давление, мм рт. ст.', value: activeWeather.pressure },
      { name: 'Направление ветра', value: activeWeather.windDirection },
      { name: 'Скорость ветра, м/c', value: activeWeather.windSpeed === 0 ? '-' : activeWeather.windSpeed },
      { name: 'Облачность, %', value: activeWeather.cloudy === 0 ? '-' : activeWeather.cloudy },
      { name: 'Нижняя граница облачности, м', value: activeWeather.h },
      { name: 'Горизонтальная видимость, км', value: activeWeather.vv === 0 ? '-' : activeWeather.vv }
    ];
  }
}
