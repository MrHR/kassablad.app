namespace kassablad.app.Client.Data;

public class StateContainer
{
    private string? savedString;

    public string Property
    {
        get => savedString ?? string.Empty;
        set
        {
            savedString = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}

public class KassaContainer {
    public List<Kassa> Kassas { get; set; }
}

public class Kassa { 
    public string Type { get; set; }
    public List<KassaNom> KassaNoms { get; set; }
}

public class KassaNom { 
    public int NomId { get; set; }
    public int amount { get; set; }
    public decimal total { get; set; }
    public decimal multiplier { get; set; }
}