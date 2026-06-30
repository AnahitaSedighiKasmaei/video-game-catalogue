import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Observable, catchError, debounceTime, distinctUntilChanged, of, startWith, switchMap, tap } from 'rxjs';

import { VideoGame } from '../../models/video-game.model';
import { VideoGameService } from '../../services/video-game.service';

@Component({
  selector: 'app-browse-games',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './browse-games.component.html'
})
export class BrowseGamesComponent {
  private readonly service = inject(VideoGameService);

  readonly searchControl = new FormControl('', { nonNullable: true });
  loading = false;
  error = false;

  readonly games$: Observable<VideoGame[]> = this.searchControl.valueChanges.pipe(
    startWith(this.searchControl.value),
    debounceTime(300),
    distinctUntilChanged(),
    tap(() => {
      this.loading = true;
      this.error = false;
    }),
    switchMap(term =>
      this.service.getAll(term).pipe(
        catchError(() => {
          this.error = true;
          return of<VideoGame[]>([]);
        })
      )
    ),
    tap(() => (this.loading = false))
  );
}
