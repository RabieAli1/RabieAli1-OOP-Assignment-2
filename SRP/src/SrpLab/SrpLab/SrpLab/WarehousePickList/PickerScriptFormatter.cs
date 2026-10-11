namespace SrpLab.SrpLab.WarehousePickList;

public sealed class PickerScriptFormatter
{
    public string FormatScript(
        IEnumerable<(string Aisle, int Bin, string Sku, int Qty)> walkingOrder,
        IEnumerable<(string Sku, int Allocated)> allocations,
        IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> rawLines)
    {
        var steps = walkingOrder
            .Select((s, i) => $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");

        var shortfalls = allocations.Where(a =>
        {
            var need = rawLines.First(l => l.Sku == a.Sku).QtyNeeded;
            return a.Allocated < need;
        });

        var warn = shortfalls.Any()
            ? "SHORTAGES: " + string.Join(", ", shortfalls.Select(s => s.Sku))
            : "SHORTAGES: none";

        return string.Join('\n', steps) + "\n" + warn;
    }
}