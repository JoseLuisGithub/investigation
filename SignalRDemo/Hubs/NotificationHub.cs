using Microsoft.AspNet.SignalR;
using System.Timers;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using SignalRDemo.Models;
using Dapper;

namespace SignalRDemo.Hubs
{
    public class NotificationHub : Hub
    {
        public int recordsToBeProcessed = 100000;

        public void DoLongOperation()
        {
            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            string SqlString = "SELECT TOP 1000 [id],[username],[message]FROM [chat].[dbo].[chat]";
            

        var ourCustomers = (List<Messaage>)db.Query<Messaage>(SqlString);

            Console.WriteLine(ourCustomers);

            foreach (var Customer in ourCustomers)
            {
                if (ShouldNotifyClient(Customer.id))
                {
                    var recordString = Customer.username + "-" + Customer.message;
                    Clients.Caller.sendMessage(string.Format
                    ("Processing item {0} of {1}", recordString, recordsToBeProcessed));
                    Thread.Sleep(200);
                }
            }



            //for (int record = 0; record <= recordsToBeProcessed; record++)
            //{
            //    if (ShouldNotifyClient(record))
            //    {
            //        var recordString = "itachi" + record;
            //        Clients.Caller.sendMessage(string.Format
            //        ("Processing item {0} of {1}", recordString, recordsToBeProcessed));
            //        Thread.Sleep(200);
            //    }
            //}


        }

        private static bool ShouldNotifyClient(int record)
        {
            //return record % 10 == 0;
            return true;
        }

    }
}