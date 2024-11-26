using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upp.Dtos.Chat;
public class MessageDto
{
    public long Id { get; set; }
    public string? Text { get; set; }
    public int? UserId { get; set; } // если нету пользователя, то системное уведомление
    public bool IsRead { get; set; } = false;
    public int ChatId { get; set; }
    public DateTime Created { get; set; }
}
