/* CategoryList.cs
 * 
 * Purpose: CategoryList class for ReadList app
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.08: Created
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace ReadList
{
    public class CategoryList
    {
        public CategoryList()
        {
            Categories = new List<Category>();
        }

        public List<Category> Categories { get; set; }

        public void SaveToFile(string savePath)
        {
            var serializer = new JavaScriptSerializer();

            var json = serializer.Serialize(Categories);

            File.WriteAllText(savePath, json);
        }

        private int AddCategory(string categoryName)
        {
            var newCategory = new Category
            {
                Name = categoryName
            };

            Categories.Add(newCategory);

            return Categories.IndexOf(newCategory);
        }

        public string AddItem(string categoryName, string title, string url, string note)
        {
            var newItem = new ReadItem
            {
                Title = title,
                Url = url,
                Note = note,
                Date = DateTime.Now.ToShortDateString()
            };

            Category category =
                Categories.FirstOrDefault(cat => 
                    String.Equals(cat.Name, categoryName, StringComparison.CurrentCultureIgnoreCase));

            if (category == null)
            {
                int newCategoryIndex = AddCategory(categoryName);

                category = Categories[newCategoryIndex];
            }

            category.ReadItems.Add(newItem);

            return "Added Item";
        }
    }
}