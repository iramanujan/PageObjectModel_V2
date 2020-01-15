using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Automation.Common.Config
{
    public class ToolConfigHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(section.OuterXml)))
            {
                return (ToolConfigMember)new DataContractSerializer(typeof(ToolConfigMember)).ReadObject(ms);
            }
        }
    }
}
