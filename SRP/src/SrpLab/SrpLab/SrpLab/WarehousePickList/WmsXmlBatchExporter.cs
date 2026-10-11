namespace SrpLab.SrpLab.WarehousePickList;

public sealed class WmsXmlBatchExporter
{
    public string ExportBatchXml(string batchId, IEnumerable<(string Sku, int Allocated)> allocations)
    {
        var parts = allocations.Select(a => $"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />");
        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}