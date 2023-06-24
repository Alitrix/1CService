using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Persistence.Enums
{
    public class UserTypeAccess : Enumeration
    {
        public static UserTypeAccess User => new(1, "User", "Authentication user");
        public static UserTypeAccess Manager => new(2, "Manager", "Manager company");
        public static UserTypeAccess Administrator => new(3, "Administrator", "Administrator company");

        public UserTypeAccess(int id, string name, string description)
            : base(id, name, description)
        {
        }
    }
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name, string description) => (Id, Name, _) = (id, name, description);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
    }
}
