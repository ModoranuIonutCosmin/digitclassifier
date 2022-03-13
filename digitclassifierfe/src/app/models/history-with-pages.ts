import { HistoryModel } from './history-model';

export interface HistoryWithPages {
  historyResponseList: HistoryModel[];
  numberOfPages: number;
}
