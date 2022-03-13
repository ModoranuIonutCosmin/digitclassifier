import {Component, HostListener, OnInit} from '@angular/core';
import {GlobalPredictionsService} from "../../../services/global-predictions.service";
import {LoadingService} from "../../../services/loading.service";

@Component({
  selector: 'app-drawing-board',
  templateUrl: './drawing-board-page.component.html',
  styleUrls: ['./drawing-board-page.component.scss']
})
export class DrawingBoardPageComponent implements OnInit {

  canvasWidth: number = 350
  brushSize: number = 21
  BRUSH_TO_WIDTH_RATIO: number = 0.06


  infoVisible: boolean = false;
  boardInfo = {
    predictionLikelihood: 0,
    digitPredicted: 0,
    historyEntryId: ''
  }

  starList: boolean[] = [true,true,true,true,true];
  rating:number = 0;


  constructor(private globalRatingService: GlobalPredictionsService,
              private loaderService: LoadingService) {
    loaderService.hide();
  }

  ngOnInit(): void {
    this.resizeCanvas();
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.resizeCanvas();
  }

  resizeCanvas() {

    var canvases = document.getElementsByClassName("canvas")[0]

    var canvasContainerWidth = canvases.clientWidth;
    var canvasContainerHeight = canvases.clientHeight;

    this.canvasWidth = Math.min(canvasContainerWidth, canvasContainerHeight);
    this.brushSize = this.canvasWidth * this.BRUSH_TO_WIDTH_RATIO
  }
  setStar(data:any){
    this.rating=data;
    for(var i=0;i<=4;i++){
      if(i < data){
        this.starList[i]=false;
      }
      else{
        this.starList[i]=true;
      }
    }
  }

  submitRating(rating: number) {
    //indexii sunt de la 0 - 4.

    this.globalRatingService
      .submitRatingForPrediction(this.boardInfo.historyEntryId, rating + 1)
      .subscribe(result => {
        this.rating = result.starsAmount;
        this.setStar(this.rating);
      })
  }



  updateBoard(result: any) {
    this.infoVisible = true
    this.setStar(0)
    this.boardInfo = result;
  }
}
