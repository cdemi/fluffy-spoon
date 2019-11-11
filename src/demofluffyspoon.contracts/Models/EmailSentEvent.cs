using System;

namespace demofluffyspoon.contracts.Models
{
    [Serializable]
    public class EmailSentEvent
    {
        public TimeSpan TimeTakeToSend { get; set; }
    }
}