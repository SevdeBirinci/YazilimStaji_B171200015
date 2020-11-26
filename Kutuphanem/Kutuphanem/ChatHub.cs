using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Kutuphanem
{
    public class ChatHub : Hub
    {
         public void Send(string KullaniciAdi, string message)
        {
            Clients.All.sendMessage(KullaniciAdi, message);
        }
    }
}