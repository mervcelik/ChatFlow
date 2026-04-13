using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos;

public class GetListResponse<T> 
{
    private IList<T> _items;
    public IList<T> Items
    {
        get => _items ??= new List<T>();
        set => _items = value;
    }
    public int Size { get; set; }
    public int Index { get; set; }
    public int Count { get; set; }
    public int Pages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
  
}