namespace SrpLab.SrpLab.WarehousePickList;

public sealed class InventoryAllocationPolicy
{
    public IReadOnlyList<(string Sku, int Allocated)> AllocateStock(IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        var result = new List<(string, int)>();
        foreach (var line in lines)
        {
            var alloc = Math.Min(line.QtyNeeded, line.QtyOnHand);
            result.Add((line.Sku, alloc));
        }
        return result;
    }
}