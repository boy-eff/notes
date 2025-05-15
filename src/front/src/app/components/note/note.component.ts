import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';
import { MarkdownModule } from 'ngx-markdown';
import { Note } from '../../models/note.interface';
import { MovieNote } from '../../models/movie-note.interface';
import { MovieNoteComponent } from '../notes/movie-note/movie-note.component';

@Component({
  selector: 'app-note',
  standalone: true,
  imports: [CommonModule, MarkdownModule, MovieNoteComponent],
  templateUrl: './note.component.html',
  styleUrl: './note.component.css'
})
export class NoteComponent implements OnInit {
  /**
   * Текущая заметка.
   */
  note: Note | null = null;

  /**
   * Флаг загрузки.
   */
  isLoading = true;

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.apiService.getNote(id).subscribe({
        next: (note) => {
          this.note = note;
          this.isLoading = false;
        },
        error: () => {
          this.isLoading = false;
        }
      });
    }
  }

  /**
   * Приводит заметку к типу MovieNote.
   * @param note - Заметка для приведения типов.
   * @returns Заметка типа MovieNote.
   */
  asMovieNote(note: Note): MovieNote {
    return note as MovieNote;
  }
}
