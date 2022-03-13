import {AfterViewInit, Component,  OnInit, ViewChild} from '@angular/core';
import {MatSort} from "@angular/material/sort";
import {catchError, map, startWith, switchMap} from "rxjs/operators";
import {merge, Observable, of} from "rxjs";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {DomSanitizer} from "@angular/platform-browser";

import {HistoryService} from "../../../services/history.service";
import {FavoritesService} from "../../../services/favorites.service";
import {FavoritesModel} from "../../../models/favorites-model";
import {LoadingService} from "../../../services/loading.service";


@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements AfterViewInit,OnInit {
  displayedColumns: string[] = ['image', 'dateTime', 'predictedDigit', 'predictionProbability', 'delete'];
  data: FavoritesModel[] = [];
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
          this.favoriteService!.getFavorites(this.sort.active, this.sort.direction, this.paginator.pageIndex, this.paginator.pageSize);
          return this.favoriteService!.favoritesSubject.asObservable()
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
        this.data = data.favoritesResponseList;
        this.resultsLength = data.numberOfPages * this.paginator.pageSize
        this.paginator.length = data.numberOfPages * this.paginator.pageSize
      }
    });
  }

  removeFromFav(id: string) {

    this.favoriteService.deleteFavoriteEntry(id,this.sort.active, this.sort.direction, this.paginator.pageIndex, this.paginator.pageSize);

    this.ngAfterViewInit();
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
  }

  ngOnInit(): void {

  }
}
