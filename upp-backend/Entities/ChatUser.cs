using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace upp.Entities;
public class ChatUser
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public int ChatId { get; set; }
    public Chat? Chat { get; set; }
}
