import {AfterViewInit, Component, Input, OnInit, ViewChild} from '@angular/core';
import {MatSort} from "@angular/material/sort";
import {catchError, debounceTime, distinctUntilChanged, map, startWith, switchMap} from "rxjs/operators";
import {BehaviorSubject, merge, Observable, of, pipe} from "rxjs";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {DomSanitizer} from "@angular/platform-browser";
import {HistoryModel} from "../../../models/history-model";
import {HistoryService} from "../../../services/history.service";
import {FavoritesService} from "../../../services/favorites.service";
import {LoadingService} from "../../../services/loading.service";


@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements AfterViewInit, OnInit {
  displayedColumns: string[] = ['image', 'dateTime', 'predictedDigit', 'predictionProbability', 'delete', 'isFavorite'];
  data: HistoryModel[] = [];
  @ViewChild(MatSort) sort!: MatSort;
  resultsLength = 0;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  loader$: Observable<boolean> = this.loaderService.loading$;

  constructor(public loaderService: LoadingService,
              private historyService: HistoryService,
              public _sanitizer: DomSanitizer,
              private favoriteService: FavoritesService) {
    this.loaderService.hide();
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap((searchTerm) => {
          this.historyService!.getHistory(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.paginator.pageSize);
          return this.historyService!.historySubject.asObservable()
            .pipe(catchError(() => of(null)));
        }),
        map(data => {
          if (data === null) {
            return null;
          }
          return data;
        })
      ).subscribe(data => {
      if (data !== null) {
        this.data = data.historyResponseList;
        this.resultsLength = data.numberOfPages * this.paginator.pageSize
        this.paginator.length = data.numberOfPages * this.paginator.pageSize
      }
    });

  }

  toggleDelete(id: string) {

    this.historyService.deleteHistoryEntry(id, this.sort.active, this.sort.direction, this.paginator.pageIndex, this.paginator.pageSize);
    this.ngAfterViewInit();
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
  }

  addToFav(id: string) {
    this.favoriteService.addFavoriteEntry(id, this.sort.active, this.sort.direction, this.paginator.pageIndex, this.paginator.pageSize);
    this.ngAfterViewInit();
  }

  removeFromFav(id: string) {
    this.favoriteService.deleteFavoriteEntry(id, this.sort.active, this.sort.direction, this.paginator.pageIndex, this.paginator.pageSize);
    this.ngAfterViewInit();
  }

  ngOnInit(): void {
  }
}
