﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFramework.SysExceptions
{
    public class MessageDuplicatelyHandled : Exception
    {
        public MessageDuplicatelyHandled() : base("MessageDuplicatelyHandled") { }
    }
}
