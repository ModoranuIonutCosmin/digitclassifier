import {HttpClient, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HistoryModel } from '../models/history-model';
import { HistoryResponseModel } from '../models/history-response';
import { HistoryRequestModel } from '../models/history-request';
import { HistoryWithPages } from '../models/history-with-pages';
import { HistoryDeleteRequestModel } from '../models/history-delete-request';
import {filter} from "rxjs/operators";

@Injectable({
  providedIn: 'root',
})
export class HistoryService {
  private _numberPerPage = 5;
  private _pageNumber = 0;
  public historySubject: Subject<HistoryWithPages> =
    new Subject<HistoryWithPages>();

  constructor(private _http: HttpClient) {}

  getHistory( filter = '', sortOrder = 'asc',
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
      .get<HistoryWithPages>(`${environment.apiUrl}/history`, req)
      .toPromise()
      .then((response) => {
        this.historySubject.next(response);
      });
  }

  deleteHistoryEntry(id: string,filter = '', sortOrder = 'asc',
                     pageNumber = 0, pageSize = 5): void {
    const req: HistoryDeleteRequestModel = {
      params: {
        requestId: id,
      },
    };
    this._http
      .delete<HistoryModel>(`${environment.apiUrl}/history`, req)
      .toPromise()
      .then((response) => {
        this.getHistory(filter, sortOrder, pageNumber, pageSize);
      });
  }

  changeNumberPerPage(number: number): void {
    this._numberPerPage = number;
    this.getHistory()
  }

  changePageNumber(number: number): void {
    this._pageNumber = number;
    this.getHistory();
  }
}
