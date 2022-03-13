import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { appRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './modules/home/home/home.component';
import { ErrorInterceptor } from './core/interceptor/error.interceptor';
import { JwtInterceptor } from './core/interceptor/jwt.interceptor';
import { LoginComponent } from './modules/auth/login/login.component';
import { RegisterComponent } from './modules/auth/register/register.component';
import { AlertComponent } from './shared/alert/alert.component';
import { FormsModule } from '@angular/forms';

import { HeaderMenuComponent } from './modules/header-menu/header-menu.component';
import { HeaderMenuContentComponent } from './modules/header-menu/header-menu-content/header-menu-content.component';
import { DrawingCanvasComponent } from './modules/draw/drawing-board/drawing-canvas/drawing-canvas.component';
import { DrawingBoardPageComponent } from './modules/draw/drawing-board/drawing-board-page.component';
import { HistoryComponent } from './modules/history/history/history.component';
import { FavoritesComponent } from './modules/favorites/favorites/favorites.component';
import {DatePipe} from "@angular/common";
import { NavComponent } from './modules/nav/nav.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import {MatCardModule} from "@angular/material/card";
import {MatSortModule} from "@angular/material/sort";

import { MatPaginatorModule} from "@angular/material/paginator";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {GlobalPredictionsService} from "./services/global-predictions.service";
import {MaterialModule} from "./modules/material/material.module";

@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    appRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatSortModule,
    MatCardModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MaterialModule
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    AlertComponent,
    DrawingCanvasComponent,
    DrawingBoardPageComponent,
    HeaderMenuComponent,
    HeaderMenuContentComponent,
    HistoryComponent,
    FavoritesComponent,
    NavComponent,
    // TableComponent,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
       DatePipe,
    GlobalPredictionsService
    // provider used to create fake backend
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
