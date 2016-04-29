using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace Model
{
    public class Document : IValidatableObject
    {
        public Document()
        {
            DataValues = new HashSet<DataValue>();
        }

        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Documents Must have a Unique Title..")]
        public string Title { get; set; }
        private Template _template;
        [Required(ErrorMessage= "Documents Must Be Based on a Template.")]
        public virtual Template Template 
        {
            get
            {
                return _template;
            }
            set
            {
                _template = value;
                ApplyTemplate();

            }
        }
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

        public void ApplyTemplate()
        {
            foreach (var TemplateData in Template.TemplateVariables)
            {
                if (!DataValues.Where(x=>x.Name == TemplateData.Value).Any())
                {
                    var nDataValue = new DataValue();
                    nDataValue.Name = TemplateData.Value;
                    this.AddDataValue(nDataValue);
                }
            }
        }

        public void AddDataValue(DataValue dv)
        {
            if (DataValues.Any(x=>x == dv))
            {
                throw new ArgumentException("Duplicate Value.");
            }
            DataValues.Add(dv);
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
