import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherService } from 'src/app/services/weather.service';
import { IWeather } from 'src/app/models/Weather.model';
import { IWeatherDate } from 'src/app/models/WeatherDate.model';
import { format } from 'date-fns';
import { ru } from 'date-fns/locale';
import { IWeatherParams } from 'src/app/models/WeatherParams.model';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatButtonModule],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
  host: {
    class: 'main-page-component'
  }
})
export class MainPageComponent {

  public selectedDate: IWeatherDate = {
    date: new Date()
  };
  public weathers: IWeather[] = [];
  public activeWeatherIndex: number = 0;
  public weatherParams: IWeatherParams[] = [];

  constructor(private weatherService: WeatherService) { }

  public toggleActive(index: number) {
    this.activeWeatherIndex = index;
    this.setWeatherParams(this.weathers[this.activeWeatherIndex]);
  }

  public formatDate(date: Date): string {
    return format(date, 'd MMMM yyyy', { locale: ru });
  }

  public selectDate(): void {
    const utcDate = new Date(Date.UTC(this.selectedDate.date.getFullYear(), this.selectedDate.date.getUTCMonth(), this.selectedDate.date.getUTCDate() + 1, 0, 0, 0));
    this.selectedDate.date = utcDate;
    this.weatherService.getWeatherWithDate(this.selectedDate).subscribe({
      next: (weathers: IWeather[]) => {
        if (weathers) {
          this.weathers = weathers;
          this.setWeatherParams(this.weathers[this.activeWeatherIndex]);
        }
      }
    });
  }

  private setWeatherParams(activeWeather: IWeather) {
    this.weatherParams = [
      { name: 'Влажность, %', value: activeWeather.humidity },
      { name: 'Точка росы, гр. Ц.', value: activeWeather.td },
      { name: 'Давление, мм рт. ст.', value: activeWeather.pressure },
      { name: 'Направление ветра', value: activeWeather.windDirection === " " ? '-' : activeWeather.windDirection },
      { name: 'Скорость ветра, м/c', value: activeWeather.windSpeed === 0 ? '-' : activeWeather.windSpeed },
      { name: 'Облачность, %', value: activeWeather.cloudy === 0 ? '-' : activeWeather.cloudy },
      { name: 'Нижняя граница облачности, м', value: activeWeather.h },
      { name: 'Горизонтальная видимость, км', value: activeWeather.vv === 0 ? '-' : activeWeather.vv }
    ];
  }
}
