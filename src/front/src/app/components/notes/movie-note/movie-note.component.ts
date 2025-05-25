import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkdownModule } from 'ngx-markdown';
import { MovieNote } from '../../../models/movie-note.interface';
import { ApiService } from '../../../services/api.service';
import { Lightbox, LightboxModule, IAlbum, LightboxConfig } from 'ngx-lightbox';

@Component({
  selector: 'app-movie-note',
  standalone: true,
  imports: [CommonModule, MarkdownModule, LightboxModule],
  templateUrl: './movie-note.component.html',
  styleUrl: './movie-note.component.css'
})
export class MovieNoteComponent implements OnInit {
  @Input() note!: MovieNote;

  // Используем IAlbum для строгой типизации альбома
  public galleryAlbum: Array<IAlbum> = [];

  constructor(
    private apiService: ApiService,
    private lightbox: Lightbox,
    private lightboxConfig: LightboxConfig
    ) {
      this.lightboxConfig.fadeDuration = 0.2;
      this.lightboxConfig.resizeDuration = 0.2;

    }
    
  ngOnInit(): void {
    this.initializeGalleryAlbum();
  }

  private initializeGalleryAlbum(): void {
    this.galleryAlbum = []; // Очищаем альбом перед заполнением
    if (this.note) {
      for (let i = 0; i < this.note.attachments.length; i++) {
        const filename = this.note.attachments[i];
        const src = this.getAttachmentUrl(filename);
        const caption = `Изображение ${i + 1}`;
        const thumb = src; // Используем полное изображение как миниатюру для простоты

        const albumEntry: IAlbum = {
          src: src,
          caption: caption,
          thumb: thumb
        };
        this.galleryAlbum.push(albumEntry);
      }
    }
  }

  /**
   * Получает URL для отображения вложения.
   * @param filename Имя файла вложения.
   * @returns Строка URL для изображения.
   */
  getAttachmentUrl(filename: string): string {
    return this.apiService.getFileUrl(filename);
  }

  openLightbox(index: number): void {
    this.lightbox.open(this.galleryAlbum, index);
  }

  closeLightbox(): void {
    this.lightbox.close();
  }

  /**
   * Формирует markdown контент из synopsis и opinion.
   */
  getMarkdownContent(): string {
    return `# ${this.note.title}\n\n## Синопсис\n${this.note.synopsis}\n\n## Мнение\n${this.note.opinion}`;
  }
}
