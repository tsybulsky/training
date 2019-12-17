using System;
using System.IO;
using System.Collections.Generic;

namespace FinanceLibrary
{
    public abstract class OperationImport
    {
        static private List<OperationImport> importers;
        public string ErrorMessage { get; protected set; }
        public abstract bool ImportFromFile(string filename, Operations operations);
        public abstract bool ImportFromStream(Stream stream, Operations operations);
        
        static OperationImport()
        {
            importers = new List<OperationImport>();
        }

        public static bool RegisterImporter(OperationImport importer)
        {
            importers.Add(importer);
            return true;
        }
    }
}
