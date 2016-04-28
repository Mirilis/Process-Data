using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public interface IValidatableObject
    {
        IList<ValidationResult> ValidationResults {get;}
        bool IsValid();
        bool HasErrors();

    }
}
