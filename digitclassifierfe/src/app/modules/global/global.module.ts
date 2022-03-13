import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GlobalRoutingModule } from './global-routing.module';
import { PredictionsExplorerComponent } from './components/predictions-explorer/predictions-explorer.component';
import {NgxPaginationModule} from "ngx-pagination";
import {MaterialModule} from "../material/material.module";
import {GlobalPredictionsService} from "../../services/global-predictions.service";
import { SinglePredictionRatingPanelComponent } from './components/single-prediction-rating-panel/single-prediction-rating-panel.component';


@NgModule({
  declarations: [
    PredictionsExplorerComponent,
    SinglePredictionRatingPanelComponent
  ],
  imports: [
    CommonModule,
    GlobalRoutingModule,
    MaterialModule,
    NgxPaginationModule
  ],
  providers: [GlobalPredictionsService]
})
export class GlobalModule { }
