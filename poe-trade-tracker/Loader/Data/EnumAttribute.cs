using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader.Data
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumAttribute : Attribute
    {
        public EnumAttribute(string description)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}
