using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBot.Domain.Entities;

namespace TravelBot.Application.Services
{
    public interface IChatMemoryService
    {
        ChatSession CrearChat(string nombre);
        ChatSession? ObtenerChat(Guid id);
        List<ChatSession> ObtenerTodos();
        void AgregarMensaje(Guid chatId, ChatMessage mensaje);
    }

    public class ChatMemoryService : IChatMemoryService
    {
        private readonly List<ChatSession> _chats = new();

        public ChatSession CrearChat(string nombre)
        {
            var chat = new ChatSession { Nombre = nombre };
            _chats.Add(chat);
            return chat;
        }

        public ChatSession? ObtenerChat(Guid id) =>
            _chats.FirstOrDefault(c => c.Id == id);

        public List<ChatSession> ObtenerTodos() => _chats;

        public void AgregarMensaje(Guid chatId, ChatMessage mensaje)
        {
            var chat = ObtenerChat(chatId);
            chat?.Mensajes.Add(mensaje);
        }
    }
}
