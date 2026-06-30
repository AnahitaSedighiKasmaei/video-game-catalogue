import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./features/games/pages/browse-games/browse-games.component').then(m => m.BrowseGamesComponent)
  },
  {
    path: 'games/:id/edit',
    loadComponent: () =>
      import('./features/games/pages/edit-game/edit-game.component').then(m => m.EditGameComponent)
  },
  { path: '**', redirectTo: '' }
];
