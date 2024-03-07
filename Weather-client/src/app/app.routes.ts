import { Routes } from '@angular/router';
import { MainPageComponent } from './components/pages/main-page/main-page.component';
import { UploadPageComponent } from './components/pages/upload-page/upload-page.component';
import { NotFoundPageComponent } from './components/pages/not-found-page/not-found-page.component';

export const APP_ROUTES: Routes = [
    {
        path: '',
        title: 'main',
        component: MainPageComponent
    },
    {
        path: 'main',
        title: 'main',
        component: MainPageComponent
    },
    {
        path: 'upload',
        title: 'Quick Talk',
        component: UploadPageComponent
    },
    {
        path: '**',
        component: NotFoundPageComponent
    }
];
