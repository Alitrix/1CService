using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Utilities
{
    public static class ExtAuth
    {
        public static List<KeyValuePair<string, string>> GetClaims(this IEnumerable<ClaimsIdentity> identities)
        {
            return identities.SelectMany(x => x.Claims.Select(x => KeyValuePair.Create(x.Type, x.Value))).ToList();
        }
    }
}
