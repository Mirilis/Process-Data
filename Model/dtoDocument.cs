using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class dtoDocument
    {
        public List<dtoDataValue> DataValues { get;  set; }
        public int id { get;  set; }
        public string Title { get;  set; }

        private dtoDocument()
        { 
        }
        
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
