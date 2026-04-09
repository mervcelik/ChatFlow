using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Services.Hash;

public interface IHashService
{
    string Hash(string value);
    bool Verify(string value, string hash);
}
