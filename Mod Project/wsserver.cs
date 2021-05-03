
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Collections;
namespace Kasharin
{
    public class Webserver
    {
        private string Addr;
        private int Port;
        private Module Mainmod;
        private TcpListener TcpClient;
        public Webserver(string addr, int port, Module mainmod)
        {
            this.Addr = addr;
            this.Port = port;
            this.Mainmod = mainmod;
        }
        public void Connect()
        {
           Thread t = new Thread(new ThreadStart(MyCouroutine));
           t.Start();
        }

        private void MyCouroutine() {
            while (true) {
                Module.Log($"Server running.. {this.Addr}");
                Thread.Sleep(3000);
            }
        }

    }
}
