export interface HistoryRequestModel {
  params: {
    ElementsPerPage: number;
    PageNumber: number;
    Filter :string;
    SortOrder: string;
  };
}
