import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { UpdateVideoGameRequest } from '../../models/video-game.model';
import { VideoGameService } from '../../services/video-game.service';

@Component({
  selector: 'app-edit-game',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './edit-game.component.html'
})
export class EditGameComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly service = inject(VideoGameService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  private gameId!: number;

  loading = true;
  saving = false;
  loadFailed = false;
  errorMessage: string | null = null;

  readonly form = this.fb.nonNullable.group({
    title: ['', [Validators.required, Validators.maxLength(200)]],
    genre: ['', Validators.required],
    platform: ['', Validators.required],
    publisher: ['', Validators.required],
    releaseDate: ['', Validators.required],
    rating: [0, [Validators.required, Validators.min(0), Validators.max(10)]],
    description: ['']
  });

  ngOnInit(): void {
    this.gameId = Number(this.route.snapshot.paramMap.get('id'));

    this.service.getById(this.gameId).subscribe({
      next: game => {
        this.form.patchValue({ ...game, description: game.description ?? '' });
        this.loading = false;
      },
      error: () => {
        this.loadFailed = true;
        this.loading = false;
      }
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving = true;
    this.errorMessage = null;

    const value = this.form.getRawValue();
    const request: UpdateVideoGameRequest = {
      ...value,
      description: value.description.trim() ? value.description.trim() : null
    };

    this.service.update(this.gameId, request).subscribe({
      next: () => this.router.navigate(['/']),
      error: (err: HttpErrorResponse) => {
        this.saving = false;
        this.errorMessage = err.error?.detail ?? 'Could not save changes. Please try again.';
      }
    });
  }
}
