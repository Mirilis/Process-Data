using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Revision : IValidatableObject
    {
        public Revision()
        {
            Author = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Date = DateTime.Now;
        }

        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Date of Revision is required.")]
        public DateTime Date { get; set; }
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
