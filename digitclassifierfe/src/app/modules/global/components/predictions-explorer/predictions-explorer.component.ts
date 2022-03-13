import {Component, OnInit} from '@angular/core';
import {GlobalPredictionsService} from "../../../../services/global-predictions.service";
import {RatingStatsModel} from "../../../../models/rating-stats-model";
import {PaginatePipeArgs} from "ngx-pagination/dist/paginate.pipe";
import {DomSanitizer} from "@angular/platform-browser";
import {Observable} from "rxjs";
import {LoadingService} from "../../../../services/loading.service";

@Component({
  selector: 'app-predictions-explorer',
  templateUrl: './predictions-explorer.component.html',
  styleUrls: ['./predictions-explorer.component.scss']
})
export class PredictionsExplorerComponent implements OnInit {
  data: RatingStatsModel[] = [];

  ratingStars: number[][] = [];

  entriesPerPage: number = 10;
  paginationConfig: PaginatePipeArgs =
    {
      itemsPerPage: this.entriesPerPage, currentPage: 1,
      totalItems: this.entriesPerPage * 10
    };

  loader$: Observable<boolean> = this.loaderService.loading$;

  constructor(public loaderService: LoadingService,
              private globalPredictionsService: GlobalPredictionsService,
              public _sanitizer: DomSanitizer) {
    this.loaderService.show();
  }

  ngOnInit(): void {
    this.pageChanged(this.paginationConfig.currentPage);
  }

  pageChanged(eventPageNumber: any) {
    this.paginationConfig.currentPage = eventPageNumber;

    this.globalPredictionsService
      .gatherPredictionsRatingStatsPaginated(eventPageNumber, this.entriesPerPage)
      .subscribe(data => {
        this.loaderService.hide();
        this.data = data.predictionsStats;
        this.paginationConfig.totalItems = data.totalPredictionsCount;
        this.updateRating();
      });

  }

  updateRating() {
    this.ratingStars = [];

    for (let index = 0; index < this.data.length; index++) {
      let ratingCount: number = this.data[index].averageRating;
      let litStars: number[] = [0, 0, 0, 0, 0];

      for (let star = 0; star < ratingCount; star++) {
        litStars[star] = 1;
      }
      this.ratingStars.push(litStars);
    }
  }
}
