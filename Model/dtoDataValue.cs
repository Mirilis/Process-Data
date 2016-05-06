using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Text;

namespace Model
{
    [Serializable]
    public class dtoDataValue
    {
        public int id { get; set; }
        public string Name { get;  set; }
        public string Value { get;  set; }
        public ActivityStatus Status { get; set; }

        private dtoDataValue()
        { 
        }


        public dtoDataValue(DataValue DV)
        {
            this.id = DV.id;
            this.Name = DV.Name;
            this.Value = DV.Value;
            this.Status = DV.Status;
        }
    }
}
