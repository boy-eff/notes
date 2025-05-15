import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, Router } from '@angular/router';
import { NoteType } from '../../enums/note-type.enum';
import { CommonModule } from '@angular/common';

/**
 * Маппинг типов заметок на русские названия.
 */
const NOTE_TYPE_LABELS: Record<NoteType, string> = {
    [NoteType.Movie]: 'Фильмы',
    [NoteType.General]: 'Общее'
};

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
     * @param noteType - Тип заметки.
     * @returns Русское название типа заметки.
     */
    protected getNoteTypeLabel(noteType: NoteType): string {
        return NOTE_TYPE_LABELS[noteType];
    }

    constructor(private router: Router) {}

    /**
     * Переключает тип заметок.
     * @param noteType - Тип заметок для переключения.
     */
    protected switchNoteType(noteType: NoteType): void {
        this.router.navigate(['/notes'], { queryParams: { noteType } });
    }
} 