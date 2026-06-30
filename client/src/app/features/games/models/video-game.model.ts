export interface VideoGame {
  id: number;
  title: string;
  genre: string;
  platform: string;
  publisher: string;
  releaseDate: string; // ISO date (yyyy-MM-dd)
  rating: number;
  description: string | null;
}

export type UpdateVideoGameRequest = Omit<VideoGame, 'id'>;
