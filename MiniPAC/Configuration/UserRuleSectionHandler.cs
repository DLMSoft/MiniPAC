using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace DLMSoft.MiniPAC.Configuration {
    public class UserRuleSectionHandler : IConfigurationSectionHandler {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var values = new List<string>();
            foreach (XmlNode child in section.ChildNodes) {
                if (child.Name == "userRule") {
                    values.Add(child.InnerText);
                }
            }
            return values;
        }
    }
}
