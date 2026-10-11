namespace SrpLab.SrpLab.WarehousePickList;

public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();

    private readonly InventoryAllocationPolicy _allocationPolicy = new();
    private readonly WarehousePathOptimizer _pathOptimizer = new();
    private readonly PickerScriptFormatter _scriptFormatter = new();
    private readonly WmsXmlBatchExporter _xmlExporter = new();

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        return _allocationPolicy.AllocateStock(_lines);
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        return _pathOptimizer.CalculateWalkingOrder(_lines);
    }

    public string PickerScript()
    {
        return _scriptFormatter.FormatScript(WalkingOrder(), Allocate(), _lines);
    }

    public string WmsXmlBatch(string batchId)
    {
        return _xmlExporter.ExportBatchXml(batchId, Allocate());
    }
}