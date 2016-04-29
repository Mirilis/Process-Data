using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Data.Interfaces;
using Data.Repository;

namespace Data
{
    public class ProcessDataManager : IProcessDataManager, IDisposable
	{
        private ProcessDataDBContext dbContext { get; set; }

        private IRepository<DataValue> _dataValues;
        private IRepository<TemplateVariable> _templateData;
        private ITemplateRepository _templates;
        private IDocumentRepository _documents;

        public ProcessDataManager()
        {
            CreateDbContext();
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        protected void CreateDbContext()
        {
            dbContext = new ProcessDataDBContext();
            dbContext.Configuration.ProxyCreationEnabled = false;
            dbContext.Configuration.LazyLoadingEnabled = false;
            dbContext.Configuration.ValidateOnSaveEnabled = false;
            //dbContext.Configuration.AutoDetectChangesEnabled = false;
        }
        public ITemplateRepository Templates
        {
            get 
            {
                if (_templates == null)
                {
                    _templates = new TemplateRepository(dbContext);
                }
                return _templates;
            }
        }

        public IDocumentRepository Documents
        {
            get 
            {
                if (_documents == null)
                {
                    _documents = new DocumentRepository(dbContext);
                }
                return _documents;
            }
        }

        public IRepository<DataValue> DataValues
        {
            get
            {
                if (_dataValues == null)
                {
                    _dataValues = new ProcessDataRepository<DataValue>(dbContext);
                }
                return _dataValues;
            }
        }

        public IRepository<TemplateVariable> TemplateData
        {
            get
            {
                if (_templateData == null)
                {
                    _templateData = new ProcessDataRepository<TemplateVariable>(dbContext);
                }
                return _templateData;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                }
            }
        }
    }
}
