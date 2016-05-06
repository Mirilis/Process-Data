using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace Model
{
    public class DataValue : IValidatableObject
    {

        public DataValue()
        {
            _RevisionItems = new List<DataValueRevisionItem>();
        }
 
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Data Values must have a Variable Name.")]
        public string Name { get; set; }

        [NotMapped]
        public string Value
        {
            get
            {
                if (_RevisionItems.Count >0)
                {
                    return _RevisionItems.OrderByDescending(x => x.Date).First().Value;    
                }
                return "No Value Exists.";
            }
            set
            {
                var nValue = new DataValueRevisionItem();
                nValue.Value = value;
                _RevisionItems.Add(nValue);
            }
        }

        public ActivityStatus Status { get; set; }

        public void ChangeStatus(ActivityStatus NewStatus)
        {
            this.Status = NewStatus;
        }

        public static Expression<Func<DataValue, ICollection<DataValueRevisionItem>>> DataValuesRevisionItemsAccessor = f => f._RevisionItems;
        protected virtual ICollection<DataValueRevisionItem> _RevisionItems { get; set; }
        
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
