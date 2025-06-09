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
export class MovieNoteComponent implements OnInit, OnChanges {
  @Input() note!: MovieNote;

  // Используем IAlbum для строгой типизации альбома
  public galleryAlbum: Array<IAlbum> = [];
  public parsedInfo: { [key: string]: any } = {};

  constructor(
    private apiService: ApiService,
    private lightbox: Lightbox,
    private lightboxConfig: LightboxConfig
    ) {
      this.lightboxConfig.fadeDuration = 0.2;
      this.lightboxConfig.resizeDuration = 0.2;

    }
    
  ngOnInit(): void {
    this.processNote();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['note']) {
      this.processNote();
    }
  }

  private processNote(): void {
    this.initializeGalleryAlbum();
    this.parseInfo();
  }

  private parseInfo(): void {
    this.parsedInfo = {};
    if (!this.note || !this.note.info) {
      return;
    }

    const infoString = this.note.info;
    const regex = /\[([\w_]+)::\s*([\s\S]*?)\](?=\s*(?:\[|$))/g;
    const parsed: { [key: string]: any } = {};
    let match;

    while ((match = regex.exec(infoString)) !== null) {
      const key = match[1].trim();
      let value: any = match[2].trim();

      if (key === 'actors') {
        parsed[key] = value.split(';').map((actor: string) => actor.replace(/\[\[|\]\]/g, '').trim());
      } else if (key === 'tags') {
        parsed[key] = value.split(';').map((tag: string) => tag.trim());
      } else if (key === 'director') {
        parsed[key] = value.replace(/\[\[|\]\]/g, '').trim();
      } else {
        parsed[key] = value;
      }
    }

    this.parsedInfo = parsed;
  }

  private initializeGalleryAlbum(): void {
    this.galleryAlbum = []; // Очищаем альбом перед заполнением
    if (this.note && this.note.attachments) {
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
    if (!this.note) {
      return '';
    }
    return `# ${this.note.title}\n\n## Синопсис\n${this.note.synopsis}\n\n## Мнение\n${this.note.opinion}`;
  }

  public getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  public getRatingClass(rating: string): string {
    switch (rating) {
      case '#база':
        return 'rating-baza';
      case '#хорошо':
        return 'rating-horosho';
      case '#норм':
        return 'rating-norm';
      case '#мех':
        return 'rating-meh';
      case '#кринж':
        return 'rating-cringe';
      default:
        return '';
    }
  }
}
