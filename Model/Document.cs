using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Document : IValidatableObject
    {
        public Document()
        {
            DataValues = new List<DataValue>();
        }

        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Documents Must have a Unique Title..")]
        public string Title { get; set; }
        [Required(ErrorMessage= "Documents Must Be Based on a Template.")]
        public virtual Template Template { get; set; }
        public virtual ICollection<DataValue> DataValues { get; set; }
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
