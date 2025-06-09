/**
 * Перечисление разделов приложения.
 */
export enum NoteType {
    /**
     * Раздел с фильмами.
     */
    Movie = 'movie',
    
    /**
     * Общий раздел.
     */
    General = 'general'
}

/**
 * Маппинг типов заметок на русские названия.
 */
export const NOTE_TYPE_LABELS: Record<NoteType, string> = {
    [NoteType.Movie]: 'Фильмы',
    [NoteType.General]: 'Общее'
};

/**
 * Получает русское название для типа заметки.
 * @param noteType - Тип заметки.
 * @returns Русское название типа заметки.
 */
export function getNoteTypeLabel(noteType: NoteType): string {
    return NOTE_TYPE_LABELS[noteType];
} 