using System;
using System.IO;

using Windows = System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FragSharpHelper;
using FragSharpFramework;

using Awesomium.Core;
using Awesomium.Core.Data;
using Awesomium.Core.Dynamic;
using AwesomiumXNA;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;

using Newtonsoft.Json;
using SteamWrapper;

namespace Game
{
    public partial class GameClass : Microsoft.Xna.Framework.Game
    {
        void Test_OnCreateLobby(bool result)
        {
            Console.WriteLine(result);

            string player_name = SteamCore.PlayerName();
            string lobby_name = string.Format("{0}'s lobby", player_name);
            SteamMatches.SetLobbyData("name", lobby_name);

            SteamMatches.FindLobbies(Test_OnFindLobbies);
        }

        void Test_OnFindLobbies(bool result)
        {
            Console.WriteLine(result);

            if (result)
            {
                Console.WriteLine("Failure during lobby search.");
                return;
            }

            int n = SteamMatches.NumLobbies();
            Console.WriteLine("Found {0} lobbies", n);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(SteamMatches.GetLobbyData(i, "name"));
            }

            SteamMatches.JoinLobby(Test_OnJoinLobby, Test_OnLobbyChatUpdate, Test_OnLobbyChatMsg, Test_OnLobbyDataUpdate);
        }

        void Test_OnJoinLobby(bool result)
        {
            Console.WriteLine(result);

            Console.WriteLine(SteamMatches.GetLobbyData("name"));
        }

        void Test_OnLobbyDataUpdate()
        {
            Console.WriteLine("data updated");
        }

        void Test_OnLobbyChatUpdate()
        {
            Console.WriteLine("chat updated");
        }

        void Test_OnLobbyChatMsg(string msg)
        {
            Console.WriteLine("chat msg = {0}", msg);
        }

        void Test_CreateLobby()
        {
            SteamMatches.CreateLobby(Test_OnCreateLobby, SteamMatches.LobbyType_Public);
        }
    }
}