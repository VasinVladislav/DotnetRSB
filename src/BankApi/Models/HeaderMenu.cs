namespace BankApi.Models;

// Уровень 3: Ссылки 
public class LevelThreeMenu
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; 
    public string Text { get; set; } = string.Empty;
    public string Href { get; set; } = "#";   
    public int SortOrder { get; set; }        
    public int LevelTwoMenuId { get; set; }
}

// Уровень 2: Категории 
public class LevelTwoMenu
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int LevelOneMenuId { get; set; }
    public List<LevelThreeMenu> Items { get; set; } = new();
}

// Уровень 1: Главные табы 
public class LevelOneMenu
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public List<LevelTwoMenu> Items { get; set; } = new();
}
