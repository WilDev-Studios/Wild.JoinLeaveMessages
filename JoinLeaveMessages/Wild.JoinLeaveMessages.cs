using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using OpenMod.Unturned.Plugins;
using OpenMod.API.Plugins;
using OpenMod.API.Eventing;
using System.Threading.Tasks;
using OpenMod.Unturned.Players.Connections.Events;
using OpenMod.Core.Users.Events;
using OpenMod.Core.Eventing;
using System.Collections.Generic;
using SDG.Unturned;

[assembly: PluginMetadata("Wild.JoinLeaveMessages", DisplayName = "Wild.JoinLeaveMessages", Author = "WilDev Studios", Website = "https://discord.gg/4Ggybyy87d", Description = "Unturned plugin to add join and leave messages through the OpenMod API")]
namespace JoinLeaveMessages
{
    public class JoinLeaveMessages : OpenModUnturnedPlugin
    {
        public readonly IStringLocalizer m_StringLocalizer;
        public readonly ILogger<JoinLeaveMessages> m_Logger;

        public JoinLeaveMessages(
            IStringLocalizer stringLocalizer,
            ILogger<JoinLeaveMessages> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_StringLocalizer = stringLocalizer;
            m_Logger = logger;
        }

        protected override UniTask OnLoadAsync()
        {
            m_Logger.LogInformation("+==========================================================+");
            m_Logger.LogInformation("| WILD.JOINLEAVEMESSAGES plugin has been loaded!           |");
            m_Logger.LogInformation("| Made with <3 by WildKadeGaming @ WilDev Studios          |");
            m_Logger.LogInformation("| WilDev Discord: https://discord.com/invite/4Ggybyy87d    |");
            m_Logger.LogInformation("+==========================================================+");
            return UniTask.CompletedTask;
        }

        protected override UniTask OnUnloadAsync()
        {
            m_Logger.LogInformation("+==========================================================+");
            m_Logger.LogInformation("| WILD.JOINLEAVEMESSAGES plugin has been unloaded!         |");
            m_Logger.LogInformation("| Made with <3 by WildKadeGaming @ WilDev Studios          |");
            m_Logger.LogInformation("| WilDev Discord: https://discord.com/invite/4Ggybyy87d    |");
            m_Logger.LogInformation("+==========================================================+");
            return UniTask.CompletedTask;
        }
    }

    public class UserFirstConnectionHandler : IEventListener<IUserFirstConnectingEvent>
    {
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;

        public static List<string> ConnectingMembers = new();

        public UserFirstConnectionHandler(
            IConfiguration configuration,
            IStringLocalizer stringLocalizer)
        {
            m_Configuration = configuration;
            m_StringLocalizer = stringLocalizer;
        }

        [EventListener(Priority = EventListenerPriority.Lowest)]
        public Task HandleEventAsync(object sender, IUserFirstConnectingEvent @event)
        {
            var user = @event.User.DisplayName;
            var first_join_enabled = m_Configuration.GetSection("Values:First-Join-Enabled").Get<bool>();

            if (first_join_enabled == true)
            {
                ConnectingMembers.Add(user);
                ChatManager.serverSendMessage(m_StringLocalizer["Messages:First-Join-Message", new { Player = user }], UnityEngine.Color.green, null, null, EChatMode.GLOBAL, m_Configuration.GetSection("First-Join-Image-URL").Get<string>(), true);
            }

            return Task.CompletedTask;
        }
    }

    public class UserConnectionHandler : IEventListener<UnturnedPlayerConnectedEvent>
    {
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;

        public UserConnectionHandler(
            IConfiguration configuration,
            IStringLocalizer stringLocalizer)
        {
            m_Configuration = configuration;
            m_StringLocalizer = stringLocalizer;
        }

        [EventListener(Priority = EventListenerPriority.Low)]
        public Task HandleEventAsync(object sender, UnturnedPlayerConnectedEvent @event)
        {
            var user = @event.Player;
            var stringUser = user.ToString();
            var join_enabled = m_Configuration.GetSection("Values:Join-Enabled").Get<bool>();
            var first_join_enabled = m_Configuration.GetSection("Values:First-Join-Enabled").Get<bool>();

            if (first_join_enabled == true)
            {
                for (int i = 0; i < UserFirstConnectionHandler.ConnectingMembers.Count; i++)
                {
                    if (UserFirstConnectionHandler.ConnectingMembers[i] == stringUser)
                    {
                        UserFirstConnectionHandler.ConnectingMembers.Remove(stringUser);
                        return Task.CompletedTask;
                    }
                }
            }

            if (join_enabled == true)
            {
                ChatManager.serverSendMessage(m_StringLocalizer["Messages:Join-Message", new { Player = user.SteamPlayer.playerID.playerName }], UnityEngine.Color.green, null, null, EChatMode.GLOBAL, m_Configuration.GetSection("Join-Image-URL").Get<string>(), true);
            }

            return Task.CompletedTask;
        }
    }

    public class UserDisconnectionHandler : IEventListener<UnturnedPlayerDisconnectedEvent>
    {
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;

        public UserDisconnectionHandler(
            IConfiguration configuration,
            IStringLocalizer stringLocalizer)
        {
            m_Configuration = configuration;
            m_StringLocalizer = stringLocalizer;
        }

        [EventListener(Priority = EventListenerPriority.Low)]
        public Task HandleEventAsync(object sender, UnturnedPlayerDisconnectedEvent @event)
        {
            var user = @event.Player;
            var leave_enabled = m_Configuration.GetSection("Values:Leave-Enabled").Get<bool>();

            if (leave_enabled == true)
            {
                ChatManager.serverSendMessage(m_StringLocalizer["Messages:Leave-Message", new { Player = user.SteamPlayer.playerID.playerName }], UnityEngine.Color.green, null, null, EChatMode.GLOBAL, m_Configuration.GetSection("Leave-Image-URL").Get<string>(), true);
            }

            return Task.CompletedTask;
        }
    }
}
