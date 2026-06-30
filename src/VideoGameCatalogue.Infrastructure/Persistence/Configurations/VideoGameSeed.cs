namespace VideoGameCatalogue.Infrastructure.Persistence.Configurations;

/// <summary>
/// Initial catalogue data shipped with the migration. Anonymous objects are used so the
/// domain entity can keep its setters private and stay free of seeding concerns.
/// </summary>
internal static class VideoGameSeed
{
    public static readonly object[] Games =
    [
        new { Id = 1, Title = "The Legend of Zelda: Breath of the Wild", Genre = "Action-Adventure", Platform = "Nintendo Switch", Publisher = "Nintendo", ReleaseDate = new DateOnly(2017, 3, 3), Rating = 9.7m, Description = "Open-world exploration across the kingdom of Hyrule." },
        new { Id = 2, Title = "The Witcher 3: Wild Hunt", Genre = "RPG", Platform = "PC", Publisher = "CD Projekt", ReleaseDate = new DateOnly(2015, 5, 19), Rating = 9.8m, Description = "A story-driven open-world RPG set in a dark fantasy universe." },
        new { Id = 3, Title = "Elden Ring", Genre = "Action RPG", Platform = "PlayStation 5", Publisher = "Bandai Namco", ReleaseDate = new DateOnly(2022, 2, 25), Rating = 9.6m, Description = "A vast open world crafted by FromSoftware and George R. R. Martin." },
        new { Id = 4, Title = "Hades", Genre = "Roguelike", Platform = "PC", Publisher = "Supergiant Games", ReleaseDate = new DateOnly(2020, 9, 17), Rating = 9.3m, Description = "Defy the god of the dead in a fast-paced roguelike dungeon crawler." },
        new { Id = 5, Title = "Red Dead Redemption 2", Genre = "Action-Adventure", Platform = "Xbox One", Publisher = "Rockstar Games", ReleaseDate = new DateOnly(2018, 10, 26), Rating = 9.7m, Description = "An epic tale of life in America's unforgiving heartland." },
        new { Id = 6, Title = "Stardew Valley", Genre = "Simulation", Platform = "PC", Publisher = "ConcernedApe", ReleaseDate = new DateOnly(2016, 2, 26), Rating = 8.9m, Description = "Build the farm of your dreams in this charming country-life RPG." },
        new { Id = 7, Title = "God of War Ragnarök", Genre = "Action-Adventure", Platform = "PlayStation 5", Publisher = "Sony Interactive Entertainment", ReleaseDate = new DateOnly(2022, 11, 9), Rating = 9.4m, Description = "Kratos and Atreus journey through the Nine Realms." },
        new { Id = 8, Title = "Hollow Knight", Genre = "Metroidvania", Platform = "Nintendo Switch", Publisher = "Team Cherry", ReleaseDate = new DateOnly(2017, 2, 24), Rating = 9.0m, Description = "Explore a vast interconnected world of insects and heroes." },
    ];
}
