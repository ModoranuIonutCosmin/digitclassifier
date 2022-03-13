import { Injectable } from '@angular/core';
import {Subject} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {HistoryRequestModel} from "../models/history-request";
import {environment} from "../../environments/environment";
import {HistoryService} from "./history.service";

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {

  private _numberPerPage = 5;
  private _pageNumber = 0;
  public favoritesSubject: Subject<any> =
    new Subject<any>();

  constructor(private _http: HttpClient,private historyService: HistoryService) {}

  getFavorites( filter = '', sortOrder = 'asc',
              pageNumber = 0, pageSize = 5): void {
    const req: HistoryRequestModel = {
      params: {
        ElementsPerPage: pageSize,
        PageNumber: pageNumber,
        Filter :filter,
        SortOrder:sortOrder,
      },
    };
    this._http
      .get<any>(`${environment.apiUrl}/favorites`, req)
      .toPromise()
      .then((response) => {
        this.favoritesSubject.next(response);

      });
  }

  deleteFavoriteEntry(id: string,filter = '', sortOrder = 'asc',
                      pageNumber = 0, pageSize = 5): void {
    this._http
      .put<any>(`${environment.apiUrl}/favorites/delete/${id}`,null)
      .toPromise()
      .then((response) => {
        this.historyService.getHistory(filter,sortOrder,pageNumber,pageSize)
        this.getFavorites(filter,sortOrder,pageNumber,pageSize);
      });
  }
  addFavoriteEntry(id: string,filter = '', sortOrder = 'asc',
                   pageNumber = 0, pageSize = 5): void {
    this._http
      .put<any>(`${environment.apiUrl}/favorites/add/${id}`,null)
      .toPromise()
      .then((response) => {
        this.getFavorites(filter,sortOrder,pageNumber,pageSize);
        this.historyService.getHistory(filter,sortOrder,pageNumber,pageSize)
      });
  }

  changeNumberPerPage(number: number): void {
    this._numberPerPage = number;
    this.getFavorites();
  }

  changePageNumber(number: number): void {
    this._pageNumber = number;
    this.getFavorites();
  }
}
