import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { UpdateVideoGameRequest, VideoGame } from '../models/video-game.model';

@Injectable({ providedIn: 'root' })
export class VideoGameService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiBaseUrl}/videogames`;

  getAll(search?: string): Observable<VideoGame[]> {
    let params = new HttpParams();
    if (search?.trim()) {
      params = params.set('search', search.trim());
    }
    return this.http.get<VideoGame[]>(this.baseUrl, { params });
  }

  getById(id: number): Observable<VideoGame> {
    return this.http.get<VideoGame>(`${this.baseUrl}/${id}`);
  }

  update(id: number, request: UpdateVideoGameRequest): Observable<VideoGame> {
    return this.http.put<VideoGame>(`${this.baseUrl}/${id}`, request);
  }
}
