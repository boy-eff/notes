/**
 * Базовый интерфейс для заметок.
 */
export interface Note {
  id: string;
  title: string;
  content: string;
  /**
   * Тип заметки.
   */
  type: string;
} 