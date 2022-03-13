export interface FavoritesModel {
  id: string;
  image: string;
  dateTime: string;
  predictedDigit: number;
  predictionProbability: number;
  isFavorite: boolean;
}
