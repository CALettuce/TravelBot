using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBot.Domain.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Rol { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public Guid ChatSessionId { get; set; }  // FK
    }
}
