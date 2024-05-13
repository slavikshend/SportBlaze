using System;

namespace webapi.BLL.Models
{
    public class LogLineModel
    {
        public string LogMessage { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
