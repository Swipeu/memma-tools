using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTools.Save
{
    [Serializable]
    public class ComponentData
    {
        public List<PropertyData> propertyData = new List<PropertyData>();
    }
}
