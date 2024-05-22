using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardAi.WebSocket.Hubs.Interfaces
{
    public interface ICompletionHub
    {
        Task ReceiveTextGenerationAssistantMessage(string chunkMessage);
    }
}
