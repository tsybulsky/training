using System;
using System.IO;

namespace FinanceLibrary
{
    public abstract class OperationImport
    {
        public string ErrorMessage { get; protected set; }
        abstract public bool ImportFromFile(string filename, Operations operations);
        abstract public bool ImportFromStream(Stream stream, Operations operations);
       
    }
}
