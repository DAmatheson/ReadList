/* ReadItem.cs
 * 
 * Purpose: ReadItem class for ReadList app
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.08: Created
 *      Drew Matheson, 2014.08.14: Added Title property
*/

using System;

namespace ReadList
{
    public class ReadItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Note { get; set; }
        public string Date { get; set; }

        public override string ToString()
        {
            return String.Format("Title: {0} Url: {1} Note: {2}  Date: {3}", Title, Url, Note, Date);
        }
    }
}