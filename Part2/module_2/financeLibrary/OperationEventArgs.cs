using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceLibrary
{
    public enum OperationAction
    {
        Add,
        Remove
    };

    public class OperationEventArgs
    {
        public Operation Item { get; private set; }  
        public OperationAction Action { get; private set; }
        
        public OperationEventArgs()
        {

        }

        public OperationEventArgs(Operation item, OperationAction action)
        {
            Item = item;
            Action = action;
        }
    }
}
