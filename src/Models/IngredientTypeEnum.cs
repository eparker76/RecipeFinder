using System;
using System.Diagnostics.Metrics;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Enum that contains all of the ingredient types
    /// </summary>
    public enum IngredientTypeEnum
    {
        Undefined = 0,
        Beef = 1,
        Butter = 2,
        Chicken = 3,
        Egg = 4,
        Flour = 5,
        Garlic = 6,
        OliveOil = 7,
        Onion = 8,
        Pepper = 9,
        Pork = 10,
        Potato = 11,
        Rice = 12,
        Sugar = 13,
        Salt = 14,
        Water = 15,
        Fish = 16,
        Turkey = 17,
        Broccoli = 18,
        Carrot = 19,
        Tomato = 20,
        Legumes = 21,
        Lamb = 22
    }

    /// <summary>
    /// Contains logic for displaying names for ingredient types
    /// </summary>
    public static class IngredientTypeEnumExtensions
    {
        /// <summary>
        /// Method to display ingredient by name
        /// </summary>
        /// <param name="data">The ingredient type enum</param>
        /// <returns>The display name</returns>
        public static string DisplayName(this IngredientTypeEnum data)
        {
            return data switch
            {
                IngredientTypeEnum.Beef => "Beef",
                IngredientTypeEnum.Butter => "Butter",
                IngredientTypeEnum.Chicken => "Chicken",
                IngredientTypeEnum.Egg => "Egg",
                IngredientTypeEnum.Flour => "Flour",
                IngredientTypeEnum.Garlic => "Garlic",
                IngredientTypeEnum.OliveOil => "Olive Oil",
                IngredientTypeEnum.Onion => "Onion",
                IngredientTypeEnum.Pepper => "Pepper",
                IngredientTypeEnum.Pork => "Pork",
                IngredientTypeEnum.Potato => "Potato",
                IngredientTypeEnum.Rice => "Rice",
                IngredientTypeEnum.Sugar => "Sugar",
                IngredientTypeEnum.Salt => "Salt",
                IngredientTypeEnum.Water => "Water",
                IngredientTypeEnum.Fish => "Fish",
                IngredientTypeEnum.Turkey => "Turkey",
                IngredientTypeEnum.Broccoli => "Broccoli",
                IngredientTypeEnum.Carrot => "Carrot",
                IngredientTypeEnum.Tomato => "Tomato",
                IngredientTypeEnum.Legumes => "Legumes",
                IngredientTypeEnum.Lamb => "Lamb",
                // Default, Unknown
                _ => "",
            };
        }
    }
}