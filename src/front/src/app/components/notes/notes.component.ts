import { Component, OnInit } from '@angular/core';
import { ApiService, Note } from '../../services/api.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MarkdownComponent } from 'ngx-markdown';

/**
 * Компонент для отображения списка заметок.
 * Поддерживает два режима отображения: сетка и список.
 */
@Component({
  selector: 'app-notes',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    RouterModule,
    MarkdownComponent
  ],
  templateUrl: './notes.component.html',
  styleUrl: './notes.component.css'
})
export class NotesComponent implements OnInit {
  /**
   * Флаг, указывающий на режим отображения заметок.
   * <see langword="true"/> - режим сетки, <see langword="false"/> - режим списка.
   */
  isGridView = true;

  /**
   * Массив заметок для отображения.
   */
  notes: Note[] = [];

  /**
   * Флаг, указывающий на состояние загрузки данных.
   */
  isLoading = true;

  constructor(private api: ApiService) {}

  ngOnInit() {
    this.loadNotes();
  }

  /**
   * Загружает заметки с сервера.
   */
  loadNotes() {
    this.api.getNotes().subscribe({
      next: (data) => {
        this.notes = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading notes:', err);
        this.isLoading = false;
      }
    });
  }

  /**
   * Удаляет заметку по ID.
   * @param noteId - ID заметки для удаления.
   */
  deleteNote(noteId: string) {
    this.api.deleteNote(noteId).subscribe(() => {
      this.notes = this.notes.filter(n => n.id !== noteId);
    });
  }
}
