namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Enum that contains all of the product types
    /// </summary>
    public enum ProductTypeEnum
    {
        Undefined = 0,
        Vegetarian = 1,
        Vegan = 2,
        DairyFree = 3,
        GlutenFree = 4,
        LowCarb = 5,
        Keto = 6,
        LowFat = 7
    }

    /// <summary>
    /// Contains logic for displaying names for product types
    /// </summary>
    public static class ProductTypeEnumExtensions
    {
        /// <summary>
        /// Method to display product by name
        /// </summary>
        /// <param name="data">The product type enum</param>
        /// <returns>The display name</returns>
        public static string DisplayName(this ProductTypeEnum data)
        {
            return data switch
            {
                ProductTypeEnum.Vegetarian => "Vegetarian",
                ProductTypeEnum.Vegan => "Vegan",
                ProductTypeEnum.DairyFree => "Dairy-Free",
                ProductTypeEnum.GlutenFree => "Gluten-Free",
                ProductTypeEnum.LowCarb => "Low-Carb",
                ProductTypeEnum.Keto => "Keto",
                ProductTypeEnum.LowFat => "Low-Fat",
                // Default, Unknown
                _ => "",
            };
        }
    }
}