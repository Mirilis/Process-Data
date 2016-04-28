using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Data.Repository
{
    public class TemplateRepository : ProcessDataRepository<Template>, Interfaces.ITemplateRepository
    {
        public TemplateRepository(ProcessDataDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
