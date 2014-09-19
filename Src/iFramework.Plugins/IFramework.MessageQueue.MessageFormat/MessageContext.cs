﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IFramework.Infrastructure;
using System.Collections;
using Newtonsoft.Json;
using IFramework.Message;

namespace IFramework.MessageQueue.MessageFormat
{
    public class MessageContext : IMessageContext
    {
        public string MessageID { get; protected set; }
        public string ReplyToEndPoint { get; protected set; }
        [JsonIgnore]
        public object Reply { get; set; }
        public string FromEndPoint { get; set; }
        public string Key { get; protected set; }
        Dictionary<string, string> _Headers;
        public Dictionary<string, string> Headers
        {
            get { return _Headers; }
            set { _Headers = value; }
        }

        public MessageContext()
        {
            Headers = new Dictionary<string, string>();
            SentTime = DateTime.Now;
        }

        public MessageContext(object message)
            : this()
        {
            MessageID = ObjectId.GenerateNewId().ToString();
            Message = message;
        }

        public MessageContext(object message, string key)
            : this(message)
        {
            Key = key;
        }

        public MessageContext(object message, string replyToEndPoint, string key)
            : this(message, key)
        {
            ReplyToEndPoint = replyToEndPoint;
        }

        public MessageContext(object message, string replyToEndPoint, string fromEndPoint, string key)
            : this(message, replyToEndPoint, key)
        {
            FromEndPoint = fromEndPoint;
        }

        object _Message;
        [JsonIgnore]
        public object Message
        {
            get
            {
                return _Message ?? (_Message = Headers["Message"]
                                                .ToJsonObject(Type.GetType(Headers["MessageType"])));
            }
            set
            {
                _Message = value;
                Headers["Message"] = _Message.ToJson();
                Headers["MessageType"] = _Message.GetType().AssemblyQualifiedName;
            }
        }


        public DateTime SentTime
        {
            get;
            set;
        }
    }
}