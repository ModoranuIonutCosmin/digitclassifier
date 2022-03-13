import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './modules/home/home/home.component';
import { LoginComponent } from './modules/auth/login/login.component';
import { RegisterComponent } from './modules/auth/register/register.component';
import { AuthGuard } from './core/guard/auth.guard';
import { DrawingBoardPageComponent } from './modules/draw/drawing-board/drawing-board-page.component';
import { HistoryComponent } from './modules/history/history/history.component';
import {FavoritesComponent} from "./modules/favorites/favorites/favorites.component";

const routes: Routes = [
  { path: '', component: DrawingBoardPageComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  // { path: 'draw', component: DrawingBoardPageComponent, canActivate: [AuthGuard] },
  { path: 'history', component: HistoryComponent , canActivate: [AuthGuard]},
  { path: 'favorites', component: FavoritesComponent , canActivate: [AuthGuard]},
  { path: 'global', loadChildren: () => import('./modules/global/global.module').then(m => m.GlobalModule)},
  // otherwise redirect to home
  { path: '**', redirectTo: '' },
];

export const appRoutingModule = RouterModule.forRoot(routes);
