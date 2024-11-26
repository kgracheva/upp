using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using upp.Services;

namespace Application.Hubs;

public class ChatHub: Hub
{
    private readonly IMemoryCache _cache;
    private readonly ChatService _chatService;

    public ChatHub(IMemoryCache cache, ChatService chatService)
    {
        _cache = cache;
        _chatService = chatService;
    }

    public async Task SendMessage(int chatId, string message)
    {
        var userId = GetUserId(Context.ConnectionId);
        var sendTo = await _chatService.SaveMessage(userId, chatId, message);

        if (sendTo != null)
        {
            foreach(var client in sendTo)
            {
                await Clients.Group(client.ToString()).SendAsync("messageReceived", userId, chatId, message);
            }
            
        }
    }

    public async Task SetMessageRead(int chatId)
    {
        var userId = GetUserId(Context.ConnectionId);
        if (userId == null) return;
        var sendTo = await _chatService.ReadMessages(chatId, userId.Value);
        if (sendTo != null)
        {
            foreach (var client in sendTo)
            {
                await Clients.Group(client.ToString()).SendAsync("read", chatId);
            }

        }
    }

    public override Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        if (httpContext != null)
        {
            var jwtToken = httpContext.Request.Query["access_token"];
            var handler = new JwtSecurityTokenHandler();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                var token = handler.ReadJwtToken(jwtToken);
                Console.WriteLine(token);
                var tokenS = token as JwtSecurityToken;
                foreach(var c in tokenS.Claims) {
                    Console.WriteLine(c.ToString());
                }

                var jti = tokenS.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;
                if (jti != null && jti != "")
                {
                    
                    Groups.AddToGroupAsync(Context.ConnectionId, jti);
                    _cache.Set(Context.ConnectionId, jti);
                    Console.WriteLine("Everything is fine");
                }
            }
        }
        return base.OnConnectedAsync();
    }

    private int? GetUserId(string connectionId)
    {
        if (int.TryParse(_cache.Get(connectionId).ToString(), out var userId))
        {
            return userId;
        }
        return null;
    }
}
