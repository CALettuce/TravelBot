using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBot.Domain.Entities;

namespace TravelBot.Application.Interfaces;

public interface IOpenAIService
{
    Task<string> GetChatCompletionAsync(IEnumerable<ChatMessage> mensajes);
}
