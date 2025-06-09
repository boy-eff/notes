import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, Router } from '@angular/router';
import { NoteType, getNoteTypeLabel } from '../../enums/note-type.enum';
import { CommonModule } from '@angular/common';

/**
 * Компонент хедера приложения.
 */
@Component({
    selector: 'app-header',
    standalone: true,
    imports: [CommonModule, MatToolbarModule, MatButtonModule, RouterModule],
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent {
    /**
     * Массив разделов.
     */
    protected readonly noteTypes = Object.values(NoteType);

    /**
     * Получает русское название для типа заметки.
     */
    protected readonly getNoteTypeLabel = getNoteTypeLabel;

    constructor(private router: Router) {}

    /**
     * Переключает тип заметок.
     * @param noteType - Тип заметок для переключения.
     */
    protected switchNoteType(noteType: NoteType): void {
        this.router.navigate(['/notes'], { queryParams: { noteType } });
    }
} 