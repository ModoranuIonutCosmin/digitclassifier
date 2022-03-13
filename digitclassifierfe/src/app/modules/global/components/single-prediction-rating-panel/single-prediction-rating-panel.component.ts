import {Component, OnInit} from '@angular/core';
import {RatingStatsModel} from "../../../../models/rating-stats-model";
import {DomSanitizer} from "@angular/platform-browser";
import {GlobalPredictionsService} from "../../../../services/global-predictions.service";
import {flatMap, switchMap} from "rxjs/operators";
import {Observable, pipe} from "rxjs";
import {ActivatedRoute, Params} from "@angular/router";
import {LoadingService} from "../../../../services/loading.service";

@Component({
  selector: 'app-single-prediction-rating-panel',
  templateUrl: './single-prediction-rating-panel.component.html',
  styleUrls: ['./single-prediction-rating-panel.component.scss']
})
export class SinglePredictionRatingPanelComponent implements OnInit {
  myRating: number = 0;
  litStars: number[] = [0, 0, 0, 0, 0];
  data!: RatingStatsModel;
  predictionId: string = "";
  loader$: Observable<boolean> = this.loaderService.loading$;

  constructor(public loaderService: LoadingService,
              private globalService: GlobalPredictionsService,
              private route: ActivatedRoute,
              public _sanitizer: DomSanitizer) {
    this.loaderService.show();
  }

  ngOnInit(): void {
    this.route.params.pipe(
      flatMap((params: Params, index: number) => {
        this.predictionId = params['id'] || '';

        this.globalService.gatherRatingForPrediction(this.predictionId)
          .subscribe(data => {
              this.data = data
              this.loaderService.hide();
            }
          );

        return this.globalService.gatherMyPredictionRating(this.predictionId as string);
      }))
      .subscribe(myRating => {
        this.myRating = myRating.ratingStars;

        this.updateRating();
      })


  }

  submitRating(rating: number) {
    if (this.myRating != 0) {
      return;
    }

    //indexi sunt de la 0
    rating++;

    this.globalService.submitRatingForPrediction(this.predictionId, rating)
      .subscribe(result => {
        this.myRating = rating;

        this.globalService.gatherRatingForPrediction(this.predictionId)
          .subscribe(data => this.data = data);

        this.updateRating();
      });
  }

  updateRating() {
    let ratingCount: number = this.myRating;
    this.litStars = [0, 0, 0, 0, 0];

    for (let star = 0; star < ratingCount; star++) {
      this.litStars[star] = 1;
    }
  }
}
