using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class dtoDocument
    {
        
        public IEnumerable<dtoDataValue> DataValues { get; private set; }
        public int id { get; private set; }
        public string Title { get; private set; }
        public dtoDocument(Document Doc)
        {
            DataValues = new List<dtoDataValue>();
            this.id = Doc.id;
            this.Title = Doc.Title;
            foreach (var dv in Doc.DataValues)
	        {
                ((List<dtoDataValue>)DataValues).Add(new dtoDataValue(dv));
	        }
        }
    }
}
