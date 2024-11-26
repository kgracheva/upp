using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace upp.Dtos.Chat;
public class ChatDto
{
    public int Id { get; set; }
    public int UnreadCount { get; set; }
    public MessageDto? LastMessage { get; set; }
    public ICollection<ChatUserDto> Users { get; set; }

}
