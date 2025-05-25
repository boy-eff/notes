import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkdownModule } from 'ngx-markdown';
import { MovieNote } from '../../../models/movie-note.interface';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-movie-note',
  standalone: true,
  imports: [CommonModule, MarkdownModule],
  templateUrl: './movie-note.component.html',
  styleUrl: './movie-note.component.css'
})
export class MovieNoteComponent {
  @Input() note!: MovieNote;

  constructor(private apiService: ApiService) {}

  /**
   * Получает URL для отображения вложения.
   * @param filename Имя файла вложения.
   * @returns Строка URL для изображения.
   */
  getAttachmentUrl(filename: string): string {
    return this.apiService.getFileUrl(filename);
  }

  /**
   * Формирует markdown контент из synopsis и opinion.
   */
  getMarkdownContent(): string {
    return `# ${this.note.title}\n\n## Синопсис\n${this.note.synopsis}\n\n## Мнение\n${this.note.opinion}`;
  }
}
