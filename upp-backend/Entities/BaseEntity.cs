using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? LastModified { get; set; }
}
