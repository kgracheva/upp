using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using upp.Dtos.Chat;
using upp.Entities;

namespace upp.Services;
public class ChatService 
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ChatService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ChatDto> CreateSimpleChat(CreateSimpleChatDto dto, CancellationToken token)
    {
        var chat = new Chat();
        chat.ChatUsers = new List<ChatUser>();
        foreach (var user in dto.UserIds)
        {
            chat.ChatUsers.Add(new ChatUser() { UserId = user });
        }
        _context.Chats.Add(chat);
        await _context.SaveChangesAsync(token);
        return _mapper.Map<ChatDto>(chat);
    }

    public async Task<ChatDto> AddChatMember(AddChatMemberDto dto)
    {
        var chat = await _context.Chats
            .Include(x => x.ChatUsers)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == dto.ChatId);

        chat.ChatUsers.Add(new ChatUser()
        {
            ChatId = dto.ChatId,
            UserId = dto.UserId,
        });

        await _context.SaveChangesAsync();

        return _mapper.Map<ChatDto>(chat);
    }

    public async Task<ICollection<MessageDto>> GetMessages(GetMessagesDto dto)
    {
        var LastMessage = dto.LastMessageId ?? long.MaxValue;
        var messages = _context.Messages.Where(x => x.ChatId == dto.ChatId && x.Id < LastMessage);

        messages = messages.OrderBy(x => x.Created);

        return _mapper.Map<ICollection<MessageDto>>(await messages.ToListAsync());
    }

    public async Task<ICollection<int>?> ReadMessages(int chatId, int userId)
    {
        var messages = await _context.Messages.Where(x => x.ChatId == chatId && x.UserId != userId && x.IsRead == false).ToListAsync();


        messages.ForEach(x => x.IsRead = true);

        await _context.SaveChangesAsync();

        return (messages.Where(x => x.UserId != userId && x.UserId != null).Select(x => x.UserId.Value).Distinct().ToList());
    }

    public async Task<ICollection<int>?> SaveMessage(int? userId, int? chatId, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return null;
        }

        var chat = await _context.Chats
            .Include(x => x.ChatUsers)
            .FirstOrDefaultAsync(x => x.Id == chatId);

        if (chat == null || chat.ChatUsers == null || chat.ChatUsers.All(x => x.UserId != userId))
        {
            return null;
        }

        _context.Messages.Add(new Message()
        {
            ChatId = chat.Id,
            UserId = userId,
            IsRead = false,
            Text = message,
            Created = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();
        return chat.ChatUsers.Where(x => x.UserId != userId).Select(x => x.UserId).ToList();
    }

    public async Task<ICollection<ChatDto>> GetChats(int userId)
    {
        return await _context.Chats
            .Include(x => x.ChatUsers).ThenInclude(x => x.User).ThenInclude(x => x.Info)
            .Include(x => x.Messages)
            .Where(x => x.ChatUsers.Any(u => u.UserId == userId))
            .Select(x => new ChatDto
            {
                Users = _mapper.Map<ICollection<ChatUserDto>>(x.ChatUsers),
                Id = x.Id,
                LastMessage = _mapper.Map<MessageDto>(x.Messages.OrderBy(m => m.Created).LastOrDefault()),
                UnreadCount = x.Messages.Count(x => !x.IsRead && userId != x.UserId),

            }).ToListAsync();
    }
}
