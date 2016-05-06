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
    public class Document : IValidatableObject
    {
        private Template _template;
        
        private List<ValidationResult> _validationResults;

        public Document()
        {
            Templates = new List<Template>();
            _DataValues = new List<DataValue>();
        }

        [Key]
        public int id { get; set; }
        
        [Required(ErrorMessage = "Documents Must have a Unique Title..")]
        public string Title { get; set; }
          
        [NotMapped]
        public virtual Template Template 
        {
            get
            {

                return _template;
            }
        }

        public virtual ICollection<Template> Templates { get; set; }

        public void AddTemplate(Template TV)
        {
            Templates.Add(TV);
            _template = Template.CombineTemplates(Templates);
        }
        
        public void RemoveTemplate(Template TV)
        {
            Templates.Remove(TV);
            _template = Template.CombineTemplates(Templates);
        }

        protected virtual ICollection<DataValue> _DataValues { get; set; }

        public IEnumerable<DataValue> DataValues { get { return _DataValues; } }
        
        public static Expression<Func<Document, ICollection<DataValue>>> DataValuesAccessor = f => f._DataValues;
        
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

        public void ApplyAllTemplates()
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
