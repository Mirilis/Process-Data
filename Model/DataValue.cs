using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class DataValue : IValidatableObject
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Data Values must have a Variable Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Data Values must have a Value.")]
        public string Value { get; set; }
        [Required(ErrorMessage = "Data Values must have a Revision.")]
        public virtual Revision Revision { get; set; }
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
    }
}
