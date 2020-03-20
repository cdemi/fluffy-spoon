using System;

namespace DemoFluffySpoon.Contracts.Models
{
    [Serializable]
    public class EmailSentEvent
    {
        public TimeSpan TimeTakeToSend { get; set; }
    }
}