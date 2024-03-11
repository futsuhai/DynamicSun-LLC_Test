import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IWeatherDate } from 'src/app/models/WeatherDate.model';
import { IWeather } from 'src/app/models/Weather.model';
import { IWeatherParams } from 'src/app/models/WeatherParams.model';
import { WeatherService } from 'src/app/services/weather.service';
import { format } from 'date-fns';
import { ru } from 'date-fns/locale';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-weather-page',
  standalone: true,
  imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatButtonModule],
  templateUrl: './weather-page.component.html',
  styleUrls: ['./weather-page.component.scss'],
  host: {
    class: 'weather-component'
  }
})
export class WeatherPageComponent {

  @ViewChild('datepicker') datePicker!: MatDatepicker<Date>;
  public selectedDate: IWeatherDate = {
    date: new Date()
  };
  public weathers: IWeather[] = [];
  public activeWeatherIndex: number = 0;
  public weatherParams: IWeatherParams[] = [];

  constructor(private weatherService: WeatherService) { }

  public openDatePicker() {
    this.datePicker.open();
  }

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
    activeWeather.weatherCondition ? '' : activeWeather.weatherCondition = '-';
    this.weatherParams = [
      { name: 'Влажность', value: (activeWeather.humidity.toString() + '%') },
      { name: 'Точка росы', value: (activeWeather.td.toString() + '°') },
      { name: 'Давление', value: (activeWeather.pressure.toString() + 'мм рт. ст.') },
      { name: 'Направление ветра', value: activeWeather.windDirection === " " ? '-' : activeWeather.windDirection },
      { name: 'Скорость ветра', value: activeWeather.windSpeed === 0 ? '-' : (activeWeather.windSpeed.toString() + 'м/c') },
      { name: 'Облачность', value: activeWeather.cloudy === 0 ? '-' : (activeWeather.cloudy.toString() + '%') },
      { name: 'Нижняя граница облачности', value: (activeWeather.h.toString() + 'м') },
      { name: 'Горизонтальная видимость', value: activeWeather.vv === 0 ? '-' : (activeWeather.vv.toString() + 'км') }
    ];
  }
}
