using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace ViewReference
{
	public static class AttributeSetExtensions
	{
        public static void AssignAttributeValue(this AttributeSet attribSet, string attributeName, string attributeValue)
        {
            if (attribSet.NameIsUsed[attributeName])
            {
                //Attribute Exists, Assign Value
                attribSet[attributeName].Value = attributeValue;
            }
            else
            {
                //Attribute Doesn't Exist, Create then Assign Value
                Inventor.Attribute newAttribute = attribSet.Add(attributeName, ValueTypeEnum.kStringType, attributeValue);
                newAttribute.Value = attributeValue;
            }
        }
    }
}
