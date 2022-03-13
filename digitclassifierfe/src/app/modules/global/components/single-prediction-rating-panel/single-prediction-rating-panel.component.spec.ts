import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SinglePredictionRatingPanelComponent } from './single-prediction-rating-panel.component';

describe('SinglePredictionRatingPanelComponent', () => {
  let component: SinglePredictionRatingPanelComponent;
  let fixture: ComponentFixture<SinglePredictionRatingPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SinglePredictionRatingPanelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SinglePredictionRatingPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
