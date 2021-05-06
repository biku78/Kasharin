
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Collections;
using WebSocketSharp;

namespace Kasharin
{
    public class Webclient
    {
        private string Addr;
        private int Port;
        private Module Mainmod;
        private Thread SendThread;
        private Thread RecieveThread;
        private WebSocket CurSocket;
        public Webclient(string addr, int port, Module mainmod)
        {
            this.Addr = addr;
            this.Port = port;
            this.Mainmod = mainmod;
            this.CurSocket = null;
        }

        private void ExitThread(Thread t) {
            if (t != null) {
                try {
                    t.Abort();
                } catch (ThreadStateException) {

                }
            }
        }

        public void ServerConnect()
        {
            ExitThread(this.SendThread);
            if (this.CurSocket != null) {
                this.CurSocket.CloseAsync();
            }
            this.CurSocket = new WebSocket($"ws://{this.Addr}:{this.Port}");
            //this.SendThread = new Thread(new ThreadStart(StartClientSend));
            //this.SendThread.Start();
        }
        
        private void StartClientRecieve() {
            while (true) {

            }
        }

        private void StartClientSend() {
            this.CurSocket.Connect();
            this.CurSocket.Send("Hey i am connected now! This is from ETG!");
            while (true) {
                try {   
                    Vector3 pos = Module.MainPlrPos;
                    string posStr = $"x : {pos.x}, y: {pos.y}";
                    this.CurSocket.Send(posStr);
                } catch (ThreadAbortException e) {
                    Module.Log(e.ToString());
                    break;
                }
            }
        }

    }
}
