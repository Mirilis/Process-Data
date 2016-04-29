using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Template : IValidatableObject
    {
        public Template()
        {
            TemplateVariables = new List<TemplateVariable>();
        }

        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TemplateVariable> TemplateVariables { get; set; }
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

        public void AddTemplateVariable(TemplateVariable tv)
        {
            if (TemplateVariables.Any(x => x == tv))
            {
                throw new ArgumentException("Duplicate Value.");
            }
            TemplateVariables.Add(tv);
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
    }
}
