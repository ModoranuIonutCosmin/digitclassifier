import {ComponentFixture, TestBed} from '@angular/core/testing';

import {HeaderMenuContentComponent} from './header-menu-content.component';

describe('HeaderMenuContentComponent', () => {
  let component: HeaderMenuContentComponent;
  let fixture: ComponentFixture<HeaderMenuContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HeaderMenuContentComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderMenuContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
