/// <reference types="@angular/localize" />

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';

export const config = {
  apiUrl: 'http://localhost:7023'
};

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
