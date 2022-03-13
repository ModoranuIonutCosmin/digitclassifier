import {ComponentFixture, TestBed} from '@angular/core/testing';

import {DrawingBoardPageComponent} from './drawing-board-page.component';

describe('DrawingBoardComponent', () => {
  let component: DrawingBoardPageComponent;
  let fixture: ComponentFixture<DrawingBoardPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DrawingBoardPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DrawingBoardPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
