/* Category.cs
 * 
 * Purpose: Category class for ReadList app
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.08: Created
*/

using System.Collections.Generic;

namespace ReadList
{
    public class Category
    {
        public Category()
        {
            ReadItems = new List<ReadItem>();
        }

        public string Name { get; set; }
        public List<ReadItem> ReadItems { get; set; }
    }
}