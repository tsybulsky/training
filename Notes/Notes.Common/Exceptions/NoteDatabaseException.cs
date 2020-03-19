﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Common.Exceptions
{
    public class NoteDatabaseException: Exception
    {
        public NoteDatabaseException(string message) : base(message) { }
    }
}
