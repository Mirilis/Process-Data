using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Model;

namespace Data.Repository
{
    public class DocumentRepository : ProcessDataRepository<Document>, Interfaces.IDocumentRepository
    {
        public DocumentRepository(ProcessDataDBContext dbContext)
            : base(dbContext)
        {
        }

        public IQueryable<Document> GetDocumentsWithDataValue(string DataValue)
        {
            return GetAll().Where(y => y.DataValues.Any(x=>x.Value == DataValue));
        }
    }
}
