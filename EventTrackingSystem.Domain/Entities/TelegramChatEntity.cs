using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTrackingSystem.Domain.Entities
{
    [Table("tblTelegramChates")]
    public class TelegramChatEntity
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public bool IsDeleted { get; set; }

        [Required, StringLength(255)]
        public string? FirstName { get; set; }

        [StringLength(255)]
        public string? LastName { get; set; }

        [StringLength(255)]
        public string? Username { get; set; }
    }
}
