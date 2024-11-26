using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace upp.Dtos.Chat;
public class AddChatMemberDto
{
    public int ChatId { get; set; }
    public int UserId { get; set; }
}
