import { Component, OnInit, OnDestroy, HostListener } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TruncatePipe } from '../../pipes/truncate.pipe';
import { Subscription } from 'rxjs';
import { Note } from '../../models/note.interface';
import Keycloak from 'keycloak-js';
import { MatDialog } from '@angular/material/dialog';
import { AddNoteDialogComponent } from '../add-note-dialog/add-note-dialog.component';

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
  isMoreLoading = false;

  /**
   * Тип заметок для фильтрации.
   */
  noteType: string | null = null;
  
  private readonly pageSize = 50;
  private lastId: string | undefined = undefined;
  private allNotesLoaded = false;

  private routeSubscription?: Subscription;

  constructor(
    private readonly api: ApiService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly keycloak: Keycloak,
    private readonly dialog: MatDialog
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
    this.lastId = undefined;
    this.allNotesLoaded = false;
    
    this.api.getNotes(this.noteType, this.pageSize).subscribe({
      next: (data) => {
        this.notes = data;
        this.updatePagingState(data);
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading notes:', err);
        this.isLoading = false;
      }
    });
  }

  loadMoreNotes() {
    if (this.isMoreLoading || this.allNotesLoaded) {
      return;
    }

    this.isMoreLoading = true;
    this.api.getNotes(this.noteType, this.pageSize, this.lastId).subscribe({
      next: (data) => {
        this.notes.push(...data);
        this.updatePagingState(data);
        this.isMoreLoading = false;
      },
      error: (err) => {
        console.error('Error loading more notes:', err);
        this.isMoreLoading = false;
      }
    });
  }
  
  private updatePagingState(loadedNotes: Note[]) {
    if (loadedNotes.length < this.pageSize) {
      this.allNotesLoaded = true;
    }
    if (loadedNotes.length > 0) {
      this.lastId = loadedNotes[loadedNotes.length - 1].id;
    }
  }

  @HostListener('window:scroll', ['$event'])
  onScroll() {
    if (this.isLoading || this.isMoreLoading || this.allNotesLoaded) {
      return;
    }

    const scrollPosition = window.innerHeight + window.scrollY;
    const scrollHeight = document.documentElement.scrollHeight;

    if (scrollPosition >= scrollHeight - 20) {
      this.loadMoreNotes();
    }
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

  addNote() {
    const dialogRef = this.dialog.open(AddNoteDialogComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.isLoading = true;
        this.api.importNoteFromFile(result).subscribe({
          next: () => {
            this.loadNotes();
          },
          error: (err) => {
            console.error('Error importing note:', err);
            this.isLoading = false;
          }
        });
      }
    });
  }

  isAuthenticated() {
    return this.keycloak.authenticated;
  } 
}
