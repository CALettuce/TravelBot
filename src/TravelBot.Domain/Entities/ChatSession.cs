using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBot.Domain.Entities
{
    public class ChatSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = "Nuevo Chat";
        public List<ChatMessage> Mensajes { get; set; } = new();
    }
}
