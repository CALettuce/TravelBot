using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBot.Application.Interfaces;

public interface IOpenAIService
{
    Task<string> GetChatCompletionAsync(string prompt);
}
