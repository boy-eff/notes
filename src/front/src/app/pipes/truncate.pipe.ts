import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate'
})
export class TruncatePipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return '';
    const firstNewline = value.indexOf('\n');
    return firstNewline === -1 ? value : value.substring(0, firstNewline);
  }
} 