using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Catalogue.DAL.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Overriden method of base class (object)
        public override int GetHashCode()
        {
            return Id;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == GetType())
            {
                return Id == ((Publisher)obj).Id;
            }
            else
            {
                return false;
            }
        }
    }
}
