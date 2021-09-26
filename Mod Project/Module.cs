using System;

using MonoMod.RuntimeDetour;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using Gungeon;
using MonoMod;
using ItemAPI;

namespace Kasharin
{
    public class Module : ETGModule
    {
        public static readonly string ModName = "Kasharin";
        public static readonly string Version = "0.0.1";
        public static readonly string TextColor = "#00FFFF";
        public static readonly string CmdTextColor = "#8a2db8";
        public static readonly string DefaultAddr = "localhost";
        public static readonly int DefaultPort = 25564;
        public Webclient CurServer;
        public override void Start()
        {
            Log($"{ModName} v{Version} started successfully.", TextColor);

            ETGModConsole.Commands.AddGroup("host", (args) => {
                Log($"Kasharin Version {Version}", CmdTextColor);
            });
            ETGModConsole.Commands.GetGroup("host").AddUnit("help", (args) => {
                Log($"type in:host start <host/ip>:<port> or just host start for default options");
                Log($"type in:host stop to disconnect from server, note: if you are the leader it might bug out massively right now");
            });
            ETGModConsole.Commands.GetGroup("host").AddUnit("start", this.StartServer);
        }
        private void StartServer(string[] args) {
            Log("Kasharin host server starting...", CmdTextColor);
            string addr = DefaultAddr;
            int port = DefaultPort;
            if (args.Length > 0) {
                addr = args[0];
            }
            if (args.Length > 1) {
                port = int.Parse(args[1]);
            }
            //TODO: disconnect if previous webserver exist..
            //this.CurServer = new Webclient(addr, port, this);
            //this.CurServer.ServerConnect();
            try {
                GameObject plrfrefab = ResourceCache.Acquire("PlayerRobot") as GameObject;
                GameObject character = UnityEngine.Object.Instantiate<GameObject> (plrfrefab, Vector3.zero, Quaternion.identity);
                UnityEngine.Object.DontDestroyOnLoad(character);
                foreach (Component comp in character.GetComponents(typeof(Component))) {
                    Log(comp.ToString());
                }

            } catch (Exception e) {
                Log(e.ToString());
            }

        }
        public static void Log(string text, string color="#FFFFFF") {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        } 

        public override void Exit() { }
        public override void Init() { }
    }
}
