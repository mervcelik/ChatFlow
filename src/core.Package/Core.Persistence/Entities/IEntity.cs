using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Persistence.Entities;

public interface IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DeleteDate { get; set; }
}
