using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Data.Interfaces
{
    public interface IProcessDataManager
    {
        void Commit();
        ITemplateRepository Templates { get; }
        IDocumentRepository Documents { get; }
        IRepository<DataValue> DataValues { get; }
        IRepository<TemplateVariable> TemplateData { get; }
        

    }
}
