using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Persistence.Entities;

public interface IEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
