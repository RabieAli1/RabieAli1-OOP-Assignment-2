namespace SrpLab.SrpLab.KitchenTicket;

public sealed class AllergenScanner
{
    public IReadOnlyList<string> DetectAllergens(IEnumerable<List<string>> itemsIngredients)
    {
        var hits = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var ingredients in itemsIngredients)
        {
            foreach (var ing in ingredients)
            {
                if (ing.Contains("milk") || ing.Contains("cheese") || ing.Contains("butter")) hits.Add("dairy");
                if (ing.Contains("wheat") || ing.Contains("flour") || ing.Contains("bread")) hits.Add("gluten");
                if (ing.Contains("peanut") || ing.Contains("almond") || ing.Contains("cashew")) hits.Add("nuts");
                if (ing.Contains("shrimp") || ing.Contains("prawn") || ing.Contains("crab")) hits.Add("shellfish");
            }
        }
        return hits.OrderBy(x => x).ToList();
    }
}