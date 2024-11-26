using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace upp.Entities;
public class Message : BaseEntity
{
    public string? Text { get; set; }
    public int? UserId { get; set; } // если нету пользователя, то системное уведомление
    public User? User { get; set; }
    public bool IsRead { get; set; } = false;
    public int ChatId { get; set; }
    public Chat? Chat { get; set; }
}
