namespace BankApi.ModelsDto;

// Уровень 3: Ссылки 
public record LevelThreeMenu(
    int Id, 
    string Name,
    string Text, 
    string Href,
    string LevelTwoItemId
);

// Уровень 2: Категории 
public record LevelTwoMenu(
    int Id, 
    string Text, 
    string Name,
    string Icon, 
    string LevelOneItemId,
    List<LevelThreeMenu> Items
);

// Уровень 1: Главные табы 
public record LevelOneMenu(
    int Id, 
    string Text, 
    string Name,
    List<LevelTwoMenu> Items
);
