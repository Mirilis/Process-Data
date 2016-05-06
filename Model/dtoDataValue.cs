using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class dtoDataValue
    {
        public int id { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }
        public ActivityStatus Status { get; private set; }

        public dtoDataValue(DataValue DV)
        {
            this.id = DV.id;
            this.Name = DV.Name;
            this.Value = DV.Value;
            this.Status = DV.Status;
        }
    }
}
