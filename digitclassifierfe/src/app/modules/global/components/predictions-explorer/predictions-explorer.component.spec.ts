import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PredictionsExplorerComponent } from './predictions-explorer.component';

describe('PredictionsExplorerComponent', () => {
  let component: PredictionsExplorerComponent;
  let fixture: ComponentFixture<PredictionsExplorerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PredictionsExplorerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PredictionsExplorerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
