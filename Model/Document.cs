using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Model
{
    [Serializable]
    public class Document : IValidatableObject
    {
        private Template _template;
        
        private List<ValidationResult> _validationResults;

        public Document()
        {
            _DataValues = new List<DataValue>();
        }

        [Key]
        [XmlElement("id")]
        public int id { get; set; }
        
        [Required(ErrorMessage = "Documents Must have a Unique Title..")]
        [XmlElement("Title")]
        public string Title { get; set; }
          
        [Required(ErrorMessage= "Documents Must Be Based on a Template.")]
        [XmlElement]
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

        
        protected virtual ICollection<DataValue> _DataValues { get; set; }
        
        [XmlElement]
        public IEnumerable<DataValue> DataValues { get { return _DataValues; } }
        
        public static Expression<Func<Document, ICollection<DataValue>>> DataValuesAccessor = f => f._DataValues;
        
        [NotMapped]
        [XmlIgnore]
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
            ((List<DataValue>)DataValues).Add(dv);
        }

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
