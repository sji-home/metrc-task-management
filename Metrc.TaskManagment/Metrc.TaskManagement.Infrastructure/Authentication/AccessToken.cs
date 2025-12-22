using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrc.TaskManagement.Infrastructure.Authentication;

public class AccessToken : JsonWebToken
{
    public AccessToken(string tokenString, long expiration) :
        base(tokenString, expiration)
    {
    }
}
