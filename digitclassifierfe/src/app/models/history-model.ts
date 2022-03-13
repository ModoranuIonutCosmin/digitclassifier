export interface HistoryModel {
  id: string;
  image: string;
  dateTime: Date;
  predictedDigit: number;
  predictionProbability: number;
  isFavorite: boolean;
}
