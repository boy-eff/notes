import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Note } from '../models/note.interface';
import { NoteType } from '../enums/note-type.enum';

/**
 * Сервис для работы с API заметок.
 */
@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:8000/api';

  constructor(private http: HttpClient) { }

  /**
   * Возвращает базовый URL API.
   * @returns Базовый URL в виде строки.
   */
  getBaseUrl(): string {
    return this.baseUrl;
  }

  /**
   * Формирует URL для загрузки файла.
   * @param filename - Имя файла.
   * @returns Полный URL для загрузки файла.
   */
  getFileUrl(filename: string): string {
    return `${this.baseUrl}/files/download/${filename}`;
  }

  /**
   * Получает список заметок.
   * @param noteType - Тип заметок для фильтрации.
   * @param pageSize - Размер страницы.
   * @param lastId - ID последнего элемента для курсорной пагинации.
   * @returns Наблюдаемый объект с массивом заметок.
   */
  getNotes(noteType?: string | null, pageSize?: number, lastId?: string): Observable<Note[]> {
    let params = new HttpParams();
    if (noteType) {
      params = params.set('noteType', noteType.toLowerCase());
    }
    if (pageSize) {
      params = params.set('pageSize', pageSize.toString());
    }
    if (lastId) {
      params = params.set('lastId', lastId);
    }
    return this.http.get<Note[]>(`${this.baseUrl}/notes`, { params });
  }

  /**
   * Создает новую заметку.
   * @param note - Данные новой заметки.
   * @returns Наблюдаемый объект с созданной заметкой.
   */
  createNote(note: Omit<Note, 'id'>): Observable<Note> {
    return this.http.post<Note>(`${this.baseUrl}/notes`, note);
  }

  /**
   * Обновляет существующую заметку.
   * @param id - ID заметки для обновления.
   * @param note - Новые данные заметки.
   * @returns Наблюдаемый объект с обновленной заметкой.
   */
  updateNote(id: string, note: Partial<Note>): Observable<Note> {
    return this.http.put<Note>(`${this.baseUrl}/notes/${id}`, note);
  }

  /**
   * Удаляет заметку.
   * @param id - ID заметки для удаления.
   * @returns Наблюдаемый объект.
   */
  deleteNote(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/notes/${id}`);
  }

  /**
   * Получает заметку по ID.
   * @param id - ID заметки.
   * @returns Наблюдаемый объект с заметкой.
   */
  getNote(id: string): Observable<Note> {
    return this.http.get<Note>(`${this.baseUrl}/notes/${id}`);
  }

  /**
   * Импортирует заметку из файла.
   * @param data - Данные для импорта, включая файл, тип заметки и название.
   * @returns Наблюдаемый объект.
   */
  importNoteFromFile(data: { file: File, noteType: NoteType, title: string }): Observable<void> {
    const formData = new FormData();
    formData.append('file', data.file, data.file.name);
    formData.append('title', data.title);

    return this.http.post<void>(`${this.baseUrl}/notes/import/${data.noteType}`, formData);
  }
}
