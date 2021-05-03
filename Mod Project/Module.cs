using System;

using MonoMod.RuntimeDetour;
using System.Reflection;

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
        public Webserver CurServer;
        public override void Start()
        {
            Log($"{ModName} v{Version} started successfully.", TextColor);
            Hook hook = new Hook(
                typeof(PlayerController).GetMethod("Update", BindingFlags.Instance | BindingFlags.Public),
                typeof(Module).GetMethod("OnPlrControllerUpdate")
            );
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
            this.CurServer = new Webserver(addr, port, this);
            this.CurServer.Connect();
        }

        public static void Log(string text, string color="#FFFFFF") {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }
        public static void OnPlrControllerUpdate(Action<PlayerController> orig, PlayerController self)
        {
            orig(self);
        }

        public override void Exit() { }
        public override void Init() { }
    }
}
