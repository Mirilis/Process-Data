using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class TemplateVariableRevisionItem
    {
        public TemplateVariableRevisionItem()
        {
            Author = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Date = DateTime.Now;
        }

        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Value must be provided.")]
        public string Value { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Date of Revision is required.")]
        public DateTime Date { get; set; }
    }
}
