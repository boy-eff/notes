import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

/**
 * Корневой компонент приложения.
 */
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: '<router-outlet></router-outlet>',
  styles: []
})
export class AppComponent {
  title = 'Notes';
}
