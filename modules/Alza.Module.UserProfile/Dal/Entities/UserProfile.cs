using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.UserProfile.Dal.Entities
{
    public class UserProfile : Entity
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Avatar { get; set; }
    }
}
