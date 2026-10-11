namespace SrpLab.SrpLab.KitchenTicket;

public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();

    private readonly AllergenScanner _allergenScanner = new();
    private readonly KitchenOperationsCalculator _opsCalculator = new();
    private readonly ThermalTicketFormatter _ticketFormatter = new();

    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add((item, ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(), prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        return _allergenScanner.DetectAllergens(_items.Select(i => i.Ingredients));
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        var prepTimes = _items.Select(i => i.PrepMinutes).ToList();
        var hasAllergens = DetectAllergens().Count > 0;
        return _opsCalculator.CalculateEstimatedReadyMinutes(prepTimes, hasAllergens, openStations);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        var formattedItems = _items.Select(i => (i.Item, i.PrepMinutes));
        var allergens = DetectAllergens();
        var eta = EstimatedReadyMinutes(2);
        return _ticketFormatter.RenderTicket(orderNumber, eta, formattedItems, allergens);
    }

    public string ExpoLaneHint()
    {
        var hasAllergens = DetectAllergens().Count > 0;
        var eta = EstimatedReadyMinutes(2);
        return _opsCalculator.DetermineExpoLaneHint(hasAllergens, eta);
    }
}