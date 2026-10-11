namespace SrpLab.SrpLab.WarehousePickList;

public sealed class WarehousePathOptimizer
{
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> CalculateWalkingOrder(IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        return lines
            .OrderBy(l => l.Aisle)
            .ThenBy(l => l.Bin)
            .Select(l => (l.Aisle, l.Bin, l.Sku, Math.Min(l.QtyNeeded, l.QtyOnHand)))
            .Where(x => x.Item4 > 0)
            .ToList();
    }
}