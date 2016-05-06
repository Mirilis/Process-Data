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
    
    public class Template : IValidatableObject
    {
        public Template()
        {
            _TemplateVariables = new List<TemplateVariable>();
        }

        [Key]
        public int id { get; set; }
        
        [Required(ErrorMessage="Must have a valid name.")]
        public string Name { get; set; }
        
        protected virtual ICollection<TemplateVariable> _TemplateVariables { get; set; }
        
        public IEnumerable<TemplateVariable> TemplateVariables { get { return _TemplateVariables; } }
        
        public static Expression<Func<Template, ICollection<TemplateVariable>>> TemplateVariablesAccessor = f => f._TemplateVariables;

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
            ((List<TemplateVariable>)TemplateVariables).Add(tv);
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

        public static Template CombineTemplates(IEnumerable<Template> Templates)
        {
            var tmp = new Template();
            tmp.Name = "Combined Template: ";
            foreach (var tmpl in Templates)
            {
                tmp.Name = tmpl.Name + tmpl.Name + ", ";
                foreach (var variable in tmpl.TemplateVariables)
	            {
                    if (!tmp.TemplateVariables.Contains(variable))
                	{	    
			            tmp.AddTemplateVariable(variable);
                    }
            	}
            }
            return tmp;
        }

    }
}
