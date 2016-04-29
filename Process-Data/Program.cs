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
            var nTemplate = new Template();
            nTemplate.Name = "GeneratedCatfishName";
            nTemplate.AddTemplateVariable(new TemplateVariable() { Value = "ProductNumber" });

            var nDocument = new Document();
            nDocument.Title = "CatfishingUpTheRealMcCoy";
            nDocument.Template = nTemplate;
            var nDataValue = new DataValue();
            nDataValue.Name = "ProductNumber";
            
            var dv = nDocument.DataValues.Where(x => x == nDataValue);
            if (dv.Any()) nDataValue = dv.First();
            else nDocument.AddDataValue(nDataValue);

            WriteDocumentInfo(nDocument);
            nDataValue.Value = "RFBC3W4025AA";
            WriteDocumentInfo(nDocument);
            nDataValue.Value = "RFHC3W4025AA";
            WriteDocumentInfo(nDocument);
            


            
            IProcessDataManager Context = new ProcessDataManager();
            Context.Documents.Add(nDocument);
            Context.Commit();

            var docs = Context.Documents.GetAll();
            var template = Context.Templates.GetAll();
            Console.ReadKey();
        }

        public static void WriteDocumentInfo(Document doc)
        {
            Console.WriteLine("Document Information:");
            Console.WriteLine(doc.Title);
            Console.WriteLine("Document Contains the Following Data Values:");
            foreach (var datavalue in doc.DataValues)
            {
                Console.WriteLine(datavalue.Name + ": " + datavalue.Value);
            }

            Console.WriteLine("=== Press a Key To Continue ===");
            Console.ReadKey();
        }
    }
}
