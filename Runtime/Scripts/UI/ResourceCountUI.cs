using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace LordSheo.JJTK
{
    public class ResourceCountUI : InventoryCountUI
    {
        public ResourceType type;

        protected override string ItemId => type.ToString();
        protected override BaseDefinition Definition => GeneralDefinitions.Instance.resources.Get(type);
    }
}
