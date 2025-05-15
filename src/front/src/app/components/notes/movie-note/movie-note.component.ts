import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkdownModule } from 'ngx-markdown';
import { MovieNote } from '../../../models/movie-note.interface';

@Component({
  selector: 'app-movie-note',
  standalone: true,
  imports: [CommonModule, MarkdownModule],
  templateUrl: './movie-note.component.html',
  styleUrl: './movie-note.component.css'
})
export class MovieNoteComponent {
  @Input() note!: MovieNote;

  /**
   * Формирует markdown контент из synopsis и opinion.
   */
  getMarkdownContent(): string {
    return `# ${this.note.title}\n\n## Синопсис\n${this.note.synopsis}\n\n## Мнение\n${this.note.opinion}`;
  }
}
