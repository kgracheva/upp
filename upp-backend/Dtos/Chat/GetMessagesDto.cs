using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upp.Dtos.Chat;
public class GetMessagesDto
{

    public int ChatId { get; set; } = 1;
    public int Take { get; set; } = 50;
    public long? LastMessageId { get; set; }
}
