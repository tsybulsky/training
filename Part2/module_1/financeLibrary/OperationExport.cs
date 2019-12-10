using System;
using System.IO;


namespace FinanceLibrary
{
    public abstract class OperationExport
    {
        public string ErrorMessage { get; protected set; }
        abstract public bool ExportToFile(string filename, Operations operations);
        abstract public bool ExportToStream(Stream stream, Operations operations);
    }
}
