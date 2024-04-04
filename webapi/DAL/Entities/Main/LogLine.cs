using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DAL.Entities.Main
{
    [Table("loglines")]
    public class LogLine : Entity
    {
        [Column("logmessage")]
        [Required]
        public string LogMessage { get; set; }

        [Column("timestamp")]
        [Required]
        public DateTimeOffset TimeStamp { get; set; }

        [Column("loglevel")]
        [Required]
        public string LogLevel { get; set; }
    }
}
