import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {PredictionsExplorerComponent} from "./components/predictions-explorer/predictions-explorer.component";
import {SinglePredictionRatingPanelComponent} from "./components/single-prediction-rating-panel/single-prediction-rating-panel.component";

const routes: Routes = [
  {path: '', component: PredictionsExplorerComponent },
  {path: 'popular', component: PredictionsExplorerComponent },
  {path: ':id', component: SinglePredictionRatingPanelComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GlobalRoutingModule { }
