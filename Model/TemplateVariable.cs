using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class TemplateVariable
    {

        public TemplateVariable()
        {
            RevisionItems = new List<TemplateVariableRevisionItem>();
        }

        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "VariableName must be provided.")]

        public string Value
        {
            get
            {
                if (RevisionItems.Count > 0)
                {
                    return RevisionItems.OrderByDescending(x => x.Date).First().Value;
                }
                return "No Value Exists.";
            }
            set
            {
                var nValue = new TemplateVariableRevisionItem();
                nValue.Value = value;
                RevisionItems.Add(nValue);
            }
        }

        public virtual ICollection<TemplateVariableRevisionItem> RevisionItems { get; set; }

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
