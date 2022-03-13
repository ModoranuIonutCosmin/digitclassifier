import { HistoryWithPages } from './history-with-pages';

export interface HistoryResponseModel {
  Successful: boolean;
  ErrorMessage: string;
  Response: HistoryWithPages;
}
