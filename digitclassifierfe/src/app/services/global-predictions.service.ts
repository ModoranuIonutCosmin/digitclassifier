import {Injectable} from '@angular/core';
import {RatingsStatsFullResponseModel} from "../models/ratings-stats-full-response-model";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {RatingStatsModel} from "../models/rating-stats-model";
import {MyRatingModel} from "../models/my-rating-model";
import {SubmitRatingRequestModel} from "../models/submit-rating-request-model";
import {ApiPaths} from "../../environments/apiPaths";

@Injectable()
export class GlobalPredictionsService {

  constructor(private httpClient: HttpClient) {

  }

  gatherPredictionsRatingStatsPaginated(pageNumber: number, countPerPage: number)
    : Observable<RatingsStatsFullResponseModel> {
    return this.httpClient
      .get<RatingsStatsFullResponseModel>(environment.apiUrl + ApiPaths.GetRatingsPaginated +
      `?Page=${pageNumber}&Count=${countPerPage}`);
  }
  gatherRatingForPrediction(predictionId: string)
    : Observable<RatingStatsModel> {
    return this.httpClient
      .get<RatingStatsModel>(environment.apiUrl + ApiPaths.GetRatingById +
        `${predictionId}`);
  }

  gatherMyPredictionRating(predictionId: string)
    : Observable<MyRatingModel> {
    return this.httpClient
      .get<MyRatingModel>(environment.apiUrl + ApiPaths.GetMyRating +
        `/${predictionId}`);
  }

  submitRatingForPrediction(predictionId: string, rating: number)
    : Observable<SubmitRatingRequestModel> {
    return this.httpClient
      .post<SubmitRatingRequestModel>(environment.apiUrl + ApiPaths.PostSubmitRating,
        {
          historyId: predictionId,
          starsAmount: rating
        });
  }
}
