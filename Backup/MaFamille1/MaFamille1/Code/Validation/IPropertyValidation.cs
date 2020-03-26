using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaFamille1.Validation
{
    public interface IPropertyValidation
    {
        string PropertyName { get; }
        string ErrorMessage { get; }
        bool IsInvalid();
    }
}
