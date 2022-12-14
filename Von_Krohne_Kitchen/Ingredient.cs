using System;
using System.Collections.Generic;
using System.Text;

namespace Von_Krohne_Kitchen
{
    class Ingredient
    {
        public Recipe Rec;

        public string Title;

        public decimal Amount;

        public int Servings;

        public Unit UnitOfMeasure;

        public Ingredient(Recipe rec, string title, decimal amount, int servings)
        {
            this.Rec = rec;
            this.Title = title;
            this.Amount = amount;
            this.Servings = servings;
        }
    }
}
