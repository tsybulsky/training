using System;
using System.IO;

namespace FinanceLibrary
{
    public class OperationTextExport: OperationExport
    {

        public override bool ExportToFile(string filename, Operations operations)
        {
            using (FileStream fileStream = new FileStream(filename,FileMode.OpenOrCreate))
            {
                return ExportToStream(fileStream, operations);
            }
        }

        public override bool ExportToStream(Stream stream, Operations operations)
        {
            if ((stream == null) || (operations == null))
                throw new ArgumentException();
            stream.Position = 0;
            stream.SetLength(0);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach(Operation item in operations)
                {
                    writer.WriteLine(item.StoreToString());
                }
            }            
            return true;
        }
    }
}
