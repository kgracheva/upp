using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace upp.Entities;
public class Chat : BaseEntity
{
    public virtual ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
