export interface FavoritesRequestModel {
  params: {
    ElementsPerPage: number;
    PageNumber: number;
    Filter :string;
    SortOrder: string;
  };
}
