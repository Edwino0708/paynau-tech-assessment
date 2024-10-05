import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { PersonPageComponent } from './pages/person-page/person-page.component';
import { PersonCreateComponent } from './modules/person/person-create/person-create.component';

export const routes: Routes =
[
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path:'home',
    component: HomePageComponent,
    pathMatch: 'prefix'
  },
  {
    path: 'services/person',
    children: [
      {
        path: '',
        component: PersonPageComponent
      },
      {
        path: 'create',
        component: PersonCreateComponent
      },
    ]
  }
];
