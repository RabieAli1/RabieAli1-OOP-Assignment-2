namespace SrpLab.SrpLab.KitchenTicket;

public sealed class ThermalTicketFormatter
{
    public string RenderTicket(int orderNumber, int etaMinutes, IEnumerable<(string Item, int PrepMinutes)> items, IReadOnlyList<string> allergens)
    {
        var width = 32;
        var line = new string('=', width);
        var body = string.Join('\n', items.Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergyLine = allergens.Count == 0 ? "ALLERGENS: none" : "ALLERGENS: " + string.Join(",", allergens);
        return $"{line}\nORDER #{orderNumber}\nETA {etaMinutes} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}