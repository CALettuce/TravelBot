using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBot.Domain.Entities;

namespace TravelBot.Infrastructure.Services
{
    public interface IChatStorageService
    {
        Task<ChatSession> CrearChatAsync(string nombre);
        Task<List<ChatSession>> ObtenerTodosAsync();
        Task<ChatSession?> ObtenerChatAsync(Guid id);
        Task<ChatMessage> AgregarMensajeAsync(Guid chatId, ChatMessage mensaje);
    }

    public class ChatStorageService : IChatStorageService
    {
        private readonly TravelBotDbContext _context;

        public ChatStorageService(TravelBotDbContext context)
        {
            _context = context;
        }

        public async Task<ChatSession> CrearChatAsync(string nombre)
        {
            var chat = new ChatSession { Id = Guid.NewGuid(), Nombre = nombre };
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<List<ChatSession>> ObtenerTodosAsync() =>
            await _context.Chats.Include(c => c.Mensajes).ToListAsync();

        public async Task<ChatSession?> ObtenerChatAsync(Guid id) =>
            await _context.Chats.Include(c => c.Mensajes)
                .OrderByDescending(c => c.FechaCreacion)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<ChatMessage> AgregarMensajeAsync(Guid chatId, ChatMessage mensaje)
        {
            mensaje.ChatSessionId = chatId;
            _context.Mensajes.Add(mensaje);
            await _context.SaveChangesAsync();
            return mensaje;
        }
    }
}
