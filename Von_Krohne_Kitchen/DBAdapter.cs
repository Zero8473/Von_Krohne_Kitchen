using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Von_Krohne_Kitchen
{
    class DBAdapter
    {
        public const string DB_NAME = "";
        private MySqlConnector mySqlConnector = new MySqlConnector(DB_NAME);

        public List<Category> GetCategories()
        {
            List<Category> cats = new List<Category>();

            string mySqlQueryCat = "SELECT id, title, color FROM tblcategories; ";
            MySqlDataReader CatReader = mySqlConnector.ExecuteQuery(mySqlQueryCat);

            while (CatReader.Read())
            {
                Category cat = new Category();

                cat.ID = CatReader.GetInt32("id");
                cat.Title = CatReader.GetString("title");
                cat.Color = CatReader.GetString("color");

                cats.Add(cat);
            }

            return cats;
        }

        public List<Recipe> GetRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();

            string mySqlQueryRecipe = "SELECT id, title, pic, servings, time, category FROM tblrecipes; ";
            MySqlDataReader RecReader = mySqlConnector.ExecuteQuery(mySqlQueryRecipe);

            while(RecReader.Read())
            {
                Recipe rec = new Recipe();

                rec.Title = RecReader.GetString("title");
                rec.Pic = RecReader.GetString("pic");
                rec.Servings = RecReader.GetInt32("servings");
                rec.TimeInMinutes = RecReader.GetInt32("time");

                string mySqlQueryIngredients = String.Format("SELECT title, amount, unit FROM tblingredients WHERE recid='{0}'; ", RecReader.GetString("id"));
                MySqlDataReader IngReader = mySqlConnector.ExecuteQuery(mySqlQueryIngredients);

                while(IngReader.Read())
                {
                    Ingredient ing = new Ingredient();

                    ing.Rec = rec;
                    ing.Title = IngReader.GetString("title");
                    ing.Amount = IngReader.GetDecimal("amount");
                    Enum.TryParse<Unit>(IngReader.GetString("unit"), out ing.UnitOfMeasure);

                    rec.Ingredients.Add(ing);
                }

                string mySqlQuerySteps = String.Format("SELECT no, description FROM tblsteps WHERE recid='{0}'; ", RecReader.GetString("id"));
                MySqlDataReader StepReader = mySqlConnector.ExecuteQuery(mySqlQuerySteps);

                while(StepReader.Read())
                {
                    Step step = new Step();

                    step.Rec = rec;
                    step.No = StepReader.GetInt32("no");
                    step.Description = StepReader.GetString("description");

                    rec.Steps.Add(step);
                }

                List<Category> cats = this.GetCategories();

                foreach(Category cat in cats)
                {
                    if(cat.ID == RecReader.GetInt32("category"))
                    {
                        rec.category = cat;
                    }
                }

                recipes.Add(rec);
            }

            return recipes;
        }
    }
}
