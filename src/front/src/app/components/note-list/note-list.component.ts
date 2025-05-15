import { Component, OnInit, OnDestroy } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TruncatePipe } from '../../pipes/truncate.pipe';
import { Subscription } from 'rxjs';
import { Note } from '../../models/note.interface';

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
    TruncatePipe
  ],
  templateUrl: './note-list.component.html',
  styleUrl: './note-list.component.css'
})
export class NoteListComponent implements OnInit, OnDestroy {
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

  /**
   * Тип заметок для фильтрации.
   */
  noteType: string | null = null;

  private routeSubscription?: Subscription;

  constructor(
    private api: ApiService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.routeSubscription = this.route.queryParamMap.subscribe(params => {
      this.noteType = params.get('noteType');
      this.loadNotes();
    });
  }

  ngOnDestroy() {
    this.routeSubscription?.unsubscribe();
  }

  /**
   * Загружает заметки с сервера.
   */
  loadNotes() {
    this.isLoading = true;
    this.notes = [];
    this.api.getNotes(this.noteType).subscribe({
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

  /**
   * Переходит на страницу заметки.
   * @param noteId - ID заметки.
   */
  navigateToNote(noteId: string) {
    this.router.navigate(['/notes', noteId]);
  }
}
