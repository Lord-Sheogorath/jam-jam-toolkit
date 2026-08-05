using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace LordSheo.JJTK
{
    public class DefaultResourceCountUI : DefaultInventoryCountUI
    {
        public ResourceType type;

        public override string ItemId => type.ToString();
        public override BaseDefinition Definition => GeneralDefinitions.Instance.resources.Get(type);
    }
}
