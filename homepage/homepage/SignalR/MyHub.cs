using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace homepage.SignalR
{
    public class MyHub : Hub
    {
        public void Hello(string name,string content)
        {
            Clients.All.hello(name,content);
        }
        public void notes(int role, string message)
        {
            Clients.All.notes(role,message);
        }

    }
}