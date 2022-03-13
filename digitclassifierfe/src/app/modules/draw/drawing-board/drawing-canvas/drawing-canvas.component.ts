import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../../../environments/environment";

@Component({
  selector: 'app-drawing-canvas',
  templateUrl: './drawing-canvas.component.html',
  styleUrls: ['./drawing-canvas.component.scss']
})
export class DrawingCanvasComponent implements OnInit {
  // Canvas data
  @Input() height: number = 0;
  @Input() width: number = 0;
  @Input() id: string = "";
  @Input() brushSize: number = 21;

  @Output() result = new EventEmitter<any>();

  protected digit: number = -1;

  private DELAY_TIME: number = 400;
  private SMOOTHING: boolean = true;

  private canvas: HTMLCanvasElement | undefined;
  private context: CanvasRenderingContext2D | undefined;
  private paint: boolean = false;
  private clickTime: number = 0;
  private clickX: number[] = [];
  private clickY: number[] = [];
  private clickDrag: boolean[] = [];
  private isMouseDown: boolean = false;
  public response:any;
  constructor(private httpClient: HttpClient) {
  }


  ngOnInit(): void {
  }

  sendImage(data: any): Observable<any> {
    return this.httpClient.post<any>(environment.apiUrl+"/images/predict", data);
  }

  selectCanvas(): void {
    let canvas = document.getElementById(this.id) as HTMLCanvasElement;
    let context = canvas.getContext("2d");

    if (context) {
      context.lineCap = 'round';
      context.lineJoin = 'round';
      context.strokeStyle = 'black';
      context.lineWidth = this.brushSize;
      context.imageSmoothingEnabled = this.SMOOTHING;

      this.canvas = canvas;
      this.context = context;
      if (this.paint) {
        this.clearCanvas();
      }

      this.createUserEvents();
    }
  }

  private createUserEvents() {
    let canvas = this.canvas;

    if (canvas) {
      canvas.addEventListener("mousedown", this.pressEventHandler);
      canvas.addEventListener("mousemove", this.dragEventHandler);
      canvas.addEventListener("mouseup", this.releaseEventHandler);
      canvas.addEventListener("mouseout", this.cancelEventHandler);

      canvas.addEventListener("touchstart", this.pressEventHandler);
      canvas.addEventListener("touchmove", this.dragEventHandler);
      canvas.addEventListener("touchend", this.releaseEventHandler);
      canvas.addEventListener("touchcancel", this.cancelEventHandler);
    }
  }

  private redraw() {
    let clickX = this.clickX;
    let context = this.context;
    let clickDrag = this.clickDrag;
    let clickY = this.clickY;

    if (context) {
      for (let i = 0; i < clickX.length; ++i) {
        context.beginPath();
        if (clickDrag[i]) {
          context.moveTo(clickX[i - 1], clickY[i - 1]);
        } else {
          context.moveTo(clickX[i] - 1, clickY[i]);
        }

        context.lineTo(clickX[i], clickY[i]);
        context.stroke();
      }
      context.closePath();
    }
  }

  private addClick(x: number, y: number, dragging: boolean) {
    this.clickX.push(x);
    this.clickY.push(y);
    this.clickDrag.push(dragging);
  }

  private clearCanvas() {
    if (this.context && this.canvas) {
      this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
      this.clickX = [];
      this.clickY = [];
      this.clickDrag = [];
      this.clickTime = 0;
      this.sendCanvasData().then(image => {
        let base64 = image.replace("data:image/png;base64,", "");
        this.sendImage({"base64image": base64})
          .subscribe(value => {
            this.result.emit(value);
            }
          );
      });
    }
  }

  private releaseEventHandler = () => {
    this.paint = false;
    this.isMouseDown = false;
    this.clickTime = Date.now();
  }

  private cancelEventHandler = () => {
    this.paint = false;
    this.isMouseDown = false;
    this.clickTime = Date.now();
  }

  private pressEventHandler = (e: MouseEvent | TouchEvent) => {
    this.isMouseDown = true;
    if (this.canvas) {
      if (Date.now() > this.clickTime + this.DELAY_TIME) {
        this.clearCanvas();
      }
      let mouseX = (e as TouchEvent).changedTouches ?
        (e as TouchEvent).changedTouches[0].pageX :
        (e as MouseEvent).pageX;
      let mouseY = (e as TouchEvent).changedTouches ?
        (e as TouchEvent).changedTouches[0].pageY :
        (e as MouseEvent).pageY;
      mouseX -= this.canvas.offsetLeft;
      mouseY -= this.canvas.offsetTop;

      this.paint = true;
      this.addClick(mouseX, mouseY, false);
      this.redraw();
    }
  }

  private dragEventHandler = (e: MouseEvent | TouchEvent) => {
    if (this.canvas) {
      let mouseX = (e as TouchEvent).changedTouches ?
        (e as TouchEvent).changedTouches[0].pageX :
        (e as MouseEvent).pageX;
      let mouseY = (e as TouchEvent).changedTouches ?
        (e as TouchEvent).changedTouches[0].pageY :
        (e as MouseEvent).pageY;
      mouseX -= this.canvas.offsetLeft;
      mouseY -= this.canvas.offsetTop;

      if (this.paint) {
        this.addClick(mouseX, mouseY, true);
        this.redraw();
      }

      e.preventDefault();
    }
  }

  private sendCanvasData = async (): Promise<any> => {
    while (true) {
      await this.sleep(100);
      if (this.canvas && !this.isMouseDown && this.clickTime != 0
        && this.clickTime + this.DELAY_TIME < Date.now()) {

        return this.canvas.toDataURL();
      }
    }
  }

  private sleep(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
