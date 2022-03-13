import {HistoryModel} from "./history-model";

export interface RatingStatsModel {
  prediction: HistoryModel,
  averageRating: number,
  ratingVotesCount: number
}
