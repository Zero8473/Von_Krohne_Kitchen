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

        public bool InsertCategorie(Category cat)
        {
            string mySqlInsert = String.Format("INSERT INTO category (title, color) VALUES ('{1}', '{2}'); ", cat.Title, cat.Color);
            int catid = unchecked((int)mySqlConnector.executeInsert(mySqlInsert));

            if(catid == -1)
            {
                return false
            }

            cat.ID = catid;

            return true;
        }

        public bool InsertRecipe(Recipe rec)
        {
            string mySqlInsert = String.Format("INSERT INTO recipe (title, pic, servings, time, category) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}'); ", rec.Title, rec.Pic, rec.Servings, rec.TimeInMinutes, rec.category.ID);
            int recid = unchecked((int)mySqlConnector.executeInsert(mySqlInsert));

            if(recid == -1)
            {
                return false;
            }

            rec.ID = recid;

            foreach(Step step in rec.Steps)
            {
                if(this.InsertStep(recid, step) == -1)
                {
                    return false;
                }
            }

            foreach(Ingredient ing in rec.Ingredients)
            {
                if(this.InsertIngredient(recid, ing) == -1)
                {
                    return false;
                }
            }

            return true;
        }

        private int InsertStep(int recid, Step step)
        {
            string mySqlInsertStep = String.Format("INSERT INTO step (recid, no, description) VALUES ('{0}', '{1}', '{2}'); ", recid, step.No, step.Description);
            return mySqlConnector.executeNonQuery(mySqlInsertStep);
        }

        private int InsertIngredient(int recid, Ingredient ing)
        {
            string mySqlInsertIng = String.Format("INSERT INTO ingredient (recid, title, amount, unit) VALUES ('{0}','{1}','{2}','{3}'); ", recid, ing.Title, ing.Amount, ing.UnitOfMeasure.ToString());
            return mySqlConnector.executeNonQuery(mySqlInsertIng);
        }

        public List<Category> GetCategories()
        {
            List<Category> cats = new List<Category>();

            string mySqlQueryCat = "SELECT id, title, color FROM category; ";
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

            string mySqlQueryRecipe = "SELECT id, title, pic, servings, time, category FROM recipe; ";
            MySqlDataReader RecReader = mySqlConnector.ExecuteQuery(mySqlQueryRecipe);

            while(RecReader.Read())
            {
                Recipe rec = new Recipe();

                rec.ID = RecReader.GetInt32("id");
                rec.Title = RecReader.GetString("title");
                rec.Pic = RecReader.GetString("pic");
                rec.Servings = RecReader.GetInt32("servings");
                rec.TimeInMinutes = RecReader.GetInt32("time");

                this.GetIngredientsForRecipe(rec, RecReader);

                this.GetStepsForRecipe(rec, RecReader);

                this.GetRecipeCategory(rec, RecReader);

                recipes.Add(rec);
            }

            return recipes;
        }

        private void GetStepsForRecipe(Recipe rec, MySqlDataReader RecReader)
        {
            string mySqlQuerySteps = String.Format("SELECT no, description FROM step WHERE recid='{0}'; ", rec.ID);
            MySqlDataReader StepReader = mySqlConnector.ExecuteQuery(mySqlQuerySteps);

            while (StepReader.Read())
            {
                Step step = new Step();

                step.Rec = rec;
                step.No = StepReader.GetInt32("no");
                step.Description = StepReader.GetString("description");

                rec.Steps.Add(step);
            }
        }

        private void GetIngredientsForRecipe(Recipe rec, MySqlDataReader RecReader)
        {
            string mySqlQueryIngredients = String.Format("SELECT title, amount, unit FROM ingredient WHERE recid='{0}'; ", rec.ID);
            MySqlDataReader IngReader = mySqlConnector.ExecuteQuery(mySqlQueryIngredients);

            while (IngReader.Read())
            {
                Ingredient ing = new Ingredient();

                ing.Rec = rec;
                ing.Title = IngReader.GetString("title");
                ing.Amount = IngReader.GetDecimal("amount");
                Enum.TryParse<Unit>(IngReader.GetString("unit"), out ing.UnitOfMeasure);

                rec.Ingredients.Add(ing);
            }
        }

        private void GetRecipeCategory(Recipe rec, MySqlDataReader RecReader)
        {
            List<Category> cats = this.GetCategories();

            foreach (Category cat in cats)
            {
                if (cat.ID == RecReader.GetInt32("category"))
                {
                    rec.category = cat;
                }
            }
        }

        public int DeleteRecipe(Recipe rec)
        {
            DeleteIngredientsForRecipe(rec);

            DeleteStepsForRecipe(rec);

            string mySqlDelete = String.Format("DELETE FROM recipe WHERE id='{0}'; ", rec.ID);
            return mySqlConnector.executeNonQuery(mySqlDelete);
        }

        private int DeleteIngredientsForRecipe(Recipe rec)
        {
            string mySqlDelete = String.Format("DELETE FROM tblingredients WHERE recid='{0}'; ", rec.ID);
            return mySqlConnector.executeNonQuery(mySqlDelete);
        }

        private int DeleteStepsForRecipe(Recipe rec)
        {
            string mySqlDelete = String.Format("DELETE FROM tblsteps WHERE recid='{0}'; ", rec.ID);
            return mySqlConnector.executeNonQuery(mySqlDelete);
        }

        public int UpdateRecipe(Recipe rec)
        {
            string mySqlUpdate = String.Format("UPDATE recipe SET title='{0}', pic='{1}', servings='{2}', time='{3}', category='{4}' WHERE id='{5}'; ", rec.Title, rec.Pic, rec.Servings, rec.TimeInMinutes, rec.category, rec.ID);
            return mySqlConnector.executeNonQuery(mySqlUpdate);
        }

        public int UpdateStep(Step step)
        {
            string mySqlUpdate = String.Format("UPDATE step SET description='{0}' WHERE recid='{1}' AND no='{2}'; ", step.Description, step.Rec.ID, step.No);
            return mySqlConnector.executeNonQuery(mySqlUpdate);
        }

        public int UpdateIngredient(Ingredient ing)
        {
            string mySqlUpdate = String.Format("UPDATE ingredient SET amount='{0}', unit='{1}' WHERE recid='{2}' and title='{3}'; ", ing.Amount, ing.UnitOfMeasure.ToString(), ing.Rec.ID, ing.Title);
            return mySqlConnector.executeNonQuery(mySqlUpdate);
        }
    }
}
