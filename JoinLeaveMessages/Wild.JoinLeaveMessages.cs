using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using OpenMod.Unturned.Plugins;
using OpenMod.API.Plugins;
using OpenMod.API.Eventing;
using System.Threading.Tasks;
using OpenMod.API.Users;
using OpenMod.Unturned.Players.Connections.Events;

[assembly: PluginMetadata("Wild.JoinLeaveMessages", DisplayName = "Wild.JoinLeaveMessages", Author = "WilDev Studios")]
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
            m_Logger.LogInformation("| WilDev Studios Website: https://wildev-studios.github.io |");
            m_Logger.LogInformation("+==========================================================+");
            return UniTask.CompletedTask;
        }

        protected override UniTask OnUnloadAsync()
        {
            m_Logger.LogInformation("+==========================================================+");
            m_Logger.LogInformation("| WILD.JOINLEAVEMESSAGES plugin has been unloaded!         |");
            m_Logger.LogInformation("| Made with <3 by WildKadeGaming @ WilDev Studios          |");
            m_Logger.LogInformation("| WilDev Discord: https://discord.com/invite/4Ggybyy87d    |");
            m_Logger.LogInformation("| WilDev Studios Website: https://wildev-studios.github.io |");
            m_Logger.LogInformation("+==========================================================+");
            return UniTask.CompletedTask;
        }
    }

    public class UserConnectionHandler : IEventListener<UnturnedPlayerConnectedEvent>
    {
        private readonly IConfiguration m_Configuration;
        private readonly IUserManager m_UserManager;
        private readonly IStringLocalizer m_StringLocalizer;

        public UserConnectionHandler(
            IConfiguration configuration,
            IUserManager userManager,
            IStringLocalizer stringLocalizer)
        {
            m_Configuration = configuration;
            m_UserManager = userManager;
            m_StringLocalizer = stringLocalizer;
        }

        public async Task HandleEventAsync(object sender, UnturnedPlayerConnectedEvent @event)
        {
            var user = @event.Player;
            var join_enabled = m_Configuration.GetSection("Values:Join_Enabled").Get<bool>();

            if (join_enabled == true)
            {
                await m_UserManager.BroadcastAsync(m_StringLocalizer["Messages:Join_Message", new { Player = user }]);
            }
        }
    }

    public class UserDisconnectionHandler : IEventListener<UnturnedPlayerDisconnectedEvent>
    {
        private readonly IConfiguration m_Configuration;
        private readonly IUserManager m_UserManager;
        private readonly IStringLocalizer m_StringLocalizer;

        public UserDisconnectionHandler(
            IConfiguration configuration,
            IUserManager userManager,
            IStringLocalizer stringLocalizer)
        {
            m_Configuration = configuration;
            m_UserManager = userManager;
            m_StringLocalizer = stringLocalizer;
        }

        public async Task HandleEventAsync(object sender, UnturnedPlayerDisconnectedEvent @event)
        {
            var user = @event.Player;
            var leave_enabled = m_Configuration.GetSection("Values:Leave_Enabled").Get<bool>();

            if (leave_enabled == true)
            {
                await m_UserManager.BroadcastAsync(m_StringLocalizer["Messages:Leave_Message", new { Player = user }]);
            }
        }
    }
}
