import { Routes } from '@angular/router';
import { UploadPageComponent } from './components/pages/upload-page/upload-page.component';
import { NotFoundPageComponent } from './components/pages/not-found-page/not-found-page.component';
import { WeatherPageComponent } from './components/pages/weather-page/weather-page.component';

export const APP_ROUTES: Routes = [
    {
        path: '',
        title: 'main',
        component: WeatherPageComponent
    },
    {
        path: 'weather',
        title: 'Weather',
        component: WeatherPageComponent
    },
    {
        path: 'upload',
        title: 'Upload',
        component: UploadPageComponent
    },
    {
        path: '**',
        component: NotFoundPageComponent
    }
];
