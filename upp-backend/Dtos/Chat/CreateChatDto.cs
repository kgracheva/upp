using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace upp.Dtos.Chat;
public class CreateChatDto
{
    public int UserId { get; set; }
}

public class CreateSimpleChatDto
{
    public int[] UserIds { get; set; }
}
