import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieNoteComponent } from './movie-note.component';

describe('MovieNoteComponent', () => {
  let component: MovieNoteComponent;
  let fixture: ComponentFixture<MovieNoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MovieNoteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MovieNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
