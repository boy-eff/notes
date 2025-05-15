import { Routes } from '@angular/router';
import { NoteListComponent } from './components/note-list/note-list.component';
import { NoteComponent } from './components/note/note.component';

export const routes: Routes = [
    { path: '', redirectTo: '/notes', pathMatch: 'full' },
    { path: 'notes', component: NoteListComponent },
    { path: 'notes/:id', component: NoteComponent }
];
