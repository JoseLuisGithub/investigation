using Microsoft.AspNet.SignalR;
using SignalRDemo.Hubs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SignalRDemo
{
    public class PushNotification
    {
        public void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
           
            //from here we will send notification message to client
            var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            notificationHub.Clients.All.notify("added");
            
        }
    }
}