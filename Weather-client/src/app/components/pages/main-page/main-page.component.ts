import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

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
export class MainPageComponent {

  activeIndex: number = 0;

  public names: string[] = [
    'Влажность %',
    'Точка росы',
    'Давление мм рт. ст.',
    'Направление ветра',
    'Скорость ветра м/c',
    'Облачность %',
    'Нижняя граница облачности',
    'Горизонтальная видимость',
  ];
  public values: string[] = [
    '89',
    '-6,9',
    '737',
    'З,ЮЗ',
    '1',
    '100',
    '800',
    '-',
  ];

  public toggleActive(index: number) {
    this.activeIndex = this.activeIndex === index ? -1 : index;
  }
}
