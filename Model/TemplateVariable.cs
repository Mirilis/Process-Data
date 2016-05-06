using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Model
{
    public class TemplateVariable : IValidatableObject
    {

        public TemplateVariable()
        {
            _RevisionItems = new List<TemplateVariableRevisionItem>();
        }

        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "VariableName must be provided.")]
        [NotMapped]
        public string Value
        {
            get
            {
                if (_RevisionItems.Count > 0)
                {
                    return _RevisionItems.OrderByDescending(x => x.Date).First().Value;
                }
                return "No Value Exists.";
            }
            set
            {
                var nValue = new TemplateVariableRevisionItem();
                nValue.Value = value;
                _RevisionItems.Add(nValue);
            }
        }

        public static Expression<Func<TemplateVariable, ICollection<TemplateVariableRevisionItem>>> TemplateValuesRevisionItemsAccessor = f => f._RevisionItems;
        protected virtual ICollection<TemplateVariableRevisionItem> _RevisionItems { get; set; }

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

        public static bool operator ==(TemplateVariable obj1, TemplateVariable obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(TemplateVariable obj1, TemplateVariable obj2)
        {
            return !obj1.Equals(obj2);
        }

        public override bool Equals(object obj)
        {
            TemplateVariable z = obj as TemplateVariable;
            return z.Value == this.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }


    }
}
