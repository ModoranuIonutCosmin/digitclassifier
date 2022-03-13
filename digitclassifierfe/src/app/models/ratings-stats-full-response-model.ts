import {RatingStatsModel} from "./rating-stats-model";

export interface RatingsStatsFullResponseModel {
  predictionsStats: RatingStatsModel[],
  totalPredictionsCount: number
}
