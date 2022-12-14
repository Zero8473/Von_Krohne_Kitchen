using System;
using System.Collections.Generic;
using System.Text;

namespace Von_Krohne_Kitchen
{
    class Recipe
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _pic;
        public string Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        public List<Category> Categories = new List<Category>();

        public List<Ingredient> Ingredients = new List<Ingredient>();

        public int Servings;

        public int TimeInMinutes;

        public Recipe(string title, string pic)
        {
            this._title = title;
            this._pic = pic;
        }

        public void AddCategory(Category category)
        {
            Categories.Add(category);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }
    }
}
