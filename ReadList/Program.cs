/* Program.cs
 * 
 * Purpose: Starting method for app
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.08: Created
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace ReadList
{
    internal static class Program
    {
        private const string PATH = @"D:\My Documents\Dropbox\Public\ReadList.json";
        private static readonly string pasteCode = Convert.ToChar(22).ToString();

        [STAThread]
        private static void Main(string[] args)
        {
            string jsonPath = PATH;

            if (args.Length == 1)
            {
                jsonPath = args[0];

                try
                {
                    jsonPath = Path.GetFullPath(jsonPath);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("*** The path provided as the first argument was invalid ***");
                    Console.WriteLine("-- Press any key to exit --");
                    Console.ReadKey();
                }
            }

            StartUp();

            CategoryList list = ReadJsonFile(jsonPath);

            bool stop = false;

            while (stop == false)
            {
                var categoryName = GetInput("Category");

                if (categoryName.ToLower() == "x" || categoryName.ToLower() == "exit")
                {
                    stop = ConfirmClosure();

                    continue;
                }

                var title = GetInput("Title", optional: true);
                var url = GetInput("URL");
                var note = GetInput("Note", optional: true);

                Console.WriteLine("\tCategory: {0} \n\tTitle: {1} \n\tUrl: {2} \n\tNote: {3} ", categoryName, title, url, note);
                Console.Write("\tPress n or x to scrap item > ");

                var scrap = Console.ReadLine();

                if (scrap != null && (scrap.ToLower() == "n" || scrap.ToLower() == "x"))
                {
                    continue;
                }

                var result = list.AddItem(categoryName, title, url, note);

                list.SaveToFile(jsonPath);

                Console.WriteLine(result);
                Console.WriteLine();
            }

            list.SaveToFile(jsonPath);
        }

        private static void StartUp()
        {
            Console.Title = "Read List";

            Console.WriteLine("Type exit or x as the Category to stop and exit.");
            Console.WriteLine("Ctrl+v (paste) works.");
            Console.WriteLine();
        }

        private static CategoryList ReadJsonFile(string pathToJson)
        {
            CategoryList list = new CategoryList();

            try
            {
                var serializer = new JavaScriptSerializer();

                var json = File.ReadAllText(pathToJson);

                List<Category> loadedCategories = serializer.Deserialize<List<Category>>(json);

                if (loadedCategories != null)
                {
                    list.Categories = loadedCategories;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Json file was not found. A new list was created.");
            }

            return list;
        }

        private static string GetInput(string identifier, bool optional = false)
        {
            string input = "";

            bool validInput = false;

            while (!validInput)
            {
                Console.Write(identifier + ": ");
                input = Console.ReadLine();

                if (String.IsNullOrEmpty(input) && !optional)
                {
                    Console.WriteLine("Please enter a " + identifier + ".");

                    continue;
                }
                else if (String.IsNullOrEmpty(input) && optional)
                {
                    break;
                }

                if (input == pasteCode)
                {
                    input = input.Replace(pasteCode, Clipboard.GetText());
                }

                validInput = true;
            }

            return input;
        }

        private static bool ConfirmClosure()
        {
            bool close = false;

            Console.WriteLine("Save and close the program? (y/n)");
            string response = Console.ReadLine();

            if (response != null && (response.ToLower() == "y" || response.ToLower() == "yes"))
            {
                close = true;
            }

            return close;
        }
    }
}