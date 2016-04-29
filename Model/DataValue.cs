﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class DataValue : IValidatableObject
    {

        public DataValue()
        {
            RevisionItems = new List<DataValueRevisionItem>();
        }

        [Key]
        public int id { get; set; }
        
        [Required(ErrorMessage = "Data Values must have a Variable Name.")]
        public string Name { get; set; }


        public string Value
        {
            get
            {
                if (RevisionItems.Count >0)
                {
                    return RevisionItems.OrderByDescending(x => x.Date).First().Value;    
                }
                return "No Value Exists.";
            }
            set
            {
                var nValue = new DataValueRevisionItem();
                nValue.Value = value;
                RevisionItems.Add(nValue);
            }
        }

        public virtual ICollection<DataValueRevisionItem> RevisionItems { get; set; }
        

        
        [NotMapped]
        public IList<ValidationResult> ValidationResults
        {
            get
            {
                if (_validationResults == null)
                {
                    _validationResults = new List<ValidationResult>();
                }
                return _validationResults;
            }
        }

        private List<ValidationResult> _validationResults;

        public bool IsValid()
        {
           return Validator.TryValidateObject(this, new ValidationContext(this, null, null), ValidationResults, true);
        }

        public bool HasErrors()
        {
            return ValidationResults.Count() > 0;
	    }

        public static bool operator ==(DataValue obj1, DataValue obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(DataValue obj1, DataValue obj2)
        {
            return !obj1.Equals(obj2);
        }

        public override bool Equals(object obj)
        {
            DataValue z = obj as DataValue;
            return z.Name == this.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

    }
}
