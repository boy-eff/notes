import { Note } from './note.interface';

/**
 * Интерфейс для заметок о фильмах.
 */
export interface MovieNote extends Note {
  /**
   * Краткая информация о фильме.
   */
  synopsis: string;

  /**
   * Мнение о фильме.
   */
  opinion: string;

  /**
   * Метаинформация о фильме.
   */
  info: string;

  /**
   * Список вложений (имен файлов) к заметке.
   */
  attachments: string[];
} 