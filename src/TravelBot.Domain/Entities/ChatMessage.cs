using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBot.Domain.Entities
{
    public class ChatMessage
    {
        public string Rol { get; set; } // "user" o "assistant"
        public string Texto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
