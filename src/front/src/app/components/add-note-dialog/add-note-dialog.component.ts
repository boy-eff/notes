import { Component, OnInit } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { NoteType, getNoteTypeLabel } from '../../enums/note-type.enum';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-add-note-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatInputModule
  ],
  templateUrl: './add-note-dialog.component.html',
  styleUrl: './add-note-dialog.component.css'
})
export class AddNoteDialogComponent implements OnInit {
  protected readonly noteTypes = Object.values(NoteType);
  protected readonly getNoteTypeLabel = getNoteTypeLabel;
  protected addNoteForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddNoteDialogComponent>
  ) {}

  ngOnInit(): void {
    this.addNoteForm = this.fb.group({
      file: [null, Validators.required],
      noteType: [null, Validators.required],
      title: ['', Validators.required]
    });
  }

  onFileSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      this.addNoteForm.patchValue({ file });
    }
  }

  submit(): void {
    if (this.addNoteForm.valid) {
      this.dialogRef.close(this.addNoteForm.value);
    }
  }
}
