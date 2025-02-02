using Npgsql;
using Plugify;
using s2sdk;
using static s2sdk.s2sdk;

namespace TE
{
    //public static class nintExtended
    //{
    //    public unsafe static bool GetEventBool(this nint @event, string name)
    //    {
    //        return s2sdk.s2sdk.GetEventBool(@event, name);
    //    }

    //    public unsafe static int GetEventInt(this nint @event, string name)
    //    {
    //        return s2sdk.s2sdk.GetEventInt(@event, name);
    //    }

    //    public unsafe static string GetEventString(this nint @event, string name)
    //    {
    //        return s2sdk.s2sdk.GetEventString(@event, name);
    //    }
    //}

    public unsafe class Class1 : Plugin
    {
        public void OnPluginStart()
        {
            var connectionString = "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";
            using var dataSource = NpgsqlDataSource.Create(connectionString);

            PrintToServer(dataSource.ConnectionString + "\n");
            
            HookEvent("player_spawn", PlayerSpawnPost, false);

            PrintToServer($"{Name}: OnStart\n");
        }

        private static ResultType PlayerSpawnPost(string name, nint @event, Bool8 dontBroadcast)
        {
            PrintToServer("-------------PlayerSpawnPost\n");
            return ResultType.Continue;
        }

        public void OnPluginEnd()
        {
            PrintToServer($"{Name}: OnEnd\n");
        }

        /*private unsafe static ResultType WeaponFireCallback(string name, nint @event, Bool8 dontBroadcast)
        {
            //int userid = @event.GetEventInt("userid");

            //userid &= 0xFF;

            //int userid = @event.GetEventInt("userid");
            //string userName = GetClientName(userid);

            //int userid = @event.GetEventInt("userid") + 1;
            //int entHandle = EntIndexToEntHandle(userid);
            //string userName = GetEntSchemaString(entHandle, "CBasePlayerController", "m_iszPlayerName", 0);

            //string weapon = @event.GetEventString("weapon");

            //PrintToChatColored(userid, string.Format("\x03TEst\x05TEst {0}", weapon));
            //PrintToChatColoredAll(string.Format("{0}", weapon));

            return ResultType.Continue;
        }*/
    }
}