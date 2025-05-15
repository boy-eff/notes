import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Note } from '../models/note.interface';

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
   * Получает список заметок.
   * @param noteType - Тип заметок для фильтрации.
   * @returns Наблюдаемый объект с массивом заметок.
   */
  getNotes(noteType?: string | null): Observable<Note[]> {
    let params = new HttpParams();
    if (noteType) {
      params = params.set('noteType', noteType.toLowerCase());
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
}
