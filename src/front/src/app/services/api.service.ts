import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

/**
 * Интерфейс, описывающий структуру заметки.
 */
export interface Note {
  id: string;
  title: string;
  content: string;
}

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
   * Получает список всех заметок.
   * @returns Наблюдаемый объект с массивом заметок.
   */
  getNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(`${this.baseUrl}/notes`);
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
}
