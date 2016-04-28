using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Data;
using Data.Interfaces;


namespace Process_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            var newTemplate = new Template();
            newTemplate.Name = "GeneratedCatfishName";
            newTemplate.TemplateData.Add(new TemplateData() { VariableName = "LewBobMcGhee" });

            var newDocument = new Document();
            newDocument.Title = "CatfishingUpTheRealMcCoy";
            newDocument.Template = newTemplate;
            var result = newDocument.DataValues
                .ValidateAndAdd(new DataValue()
                {
                    Value = "TotalBiscuit".ToXMLString(),
                    Revision = new Revision() { Author = "Me", Date = DateTime.Now }
                });


            
            IProcessDataManager Context = new ProcessDataManager();
            Context.Documents.Add(newDocument);
            Context.Commit();

            var docs = Context.Documents.GetAll();
            var template = Context.Templates.GetAll();
            Console.ReadKey();
        }
    }
}
