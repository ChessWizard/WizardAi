using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.WebSocket.Hubs.Interfaces;

namespace WizardAi.WebSocket.Hubs
{
    public class CompletionHub : Hub<ICompletionHub>
    {
    }
}
