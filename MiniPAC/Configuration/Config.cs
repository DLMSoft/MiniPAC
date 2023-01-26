using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Xml;

namespace DLMSoft.MiniPAC.Configuration {
    public static class Config {
        const string CONFIG_KEY_HTTP_PORT = "httpPort";
        const string CONFIG_KEY_AUTO_START = "autoStart";
        const string CONFIG_KEY_SET_PROXY = "setProxy";
        const string CONFIG_KEY_PROXY_TYPE = "proxyType";
        const string CONFIG_KEY_PROXY_HOST = "proxyHost";
        const string CONFIG_KEY_PROXY_PORT = "proxyPort";

        public static ushort HttpPort { get; set; }
        public static bool AutoStart { get; set; }
        public static bool SetProxy { get; set; }
        public static string ProxyType { get; set; }
        public static string ProxyHost { get; set; }
        public static ushort ProxyPort { get; set; }

        public static List<string> UserRules { get; set; }

        public static void Load()
        {
            ConfigurationManager.RefreshSection("appSettings");

            if (!ushort.TryParse(ConfigurationManager.AppSettings[CONFIG_KEY_HTTP_PORT], out var port)) {
                HttpPort = 8001;
            }
            else {
                HttpPort = port;
            }
            AutoStart = ConfigurationManager.AppSettings[CONFIG_KEY_AUTO_START] == "true";
            SetProxy = ConfigurationManager.AppSettings[CONFIG_KEY_SET_PROXY] == "true";
            ProxyType = ConfigurationManager.AppSettings[CONFIG_KEY_PROXY_TYPE];
            ProxyHost = ConfigurationManager.AppSettings[CONFIG_KEY_PROXY_HOST];
            if (ushort.TryParse(ConfigurationManager.AppSettings[CONFIG_KEY_PROXY_PORT], out var proxyPort)) {
                ProxyPort = proxyPort;
            }

            UserRules = ConfigurationManager.GetSection("userRules") as List<string>;
        }

        public static void Save()
        {
            var xmlFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            var doc = new XmlDocument();
            doc.Load(xmlFileName);
            var appSettingsNodes = doc.GetElementsByTagName("appSettings");
            var userRulesNodes = doc.GetElementsByTagName("userRules");

            var appSettingsNode = appSettingsNodes.Item(0);
            foreach (XmlElement el in appSettingsNode.ChildNodes) {
                var key = el.Attributes["key"]?.Value;
                if (string.IsNullOrEmpty(key)) {
                    continue;
                }
                
                switch (key) {
                    case CONFIG_KEY_HTTP_PORT:
                        el.SetAttribute("value", HttpPort.ToString());
                        break;
                    case CONFIG_KEY_AUTO_START:
                        el.SetAttribute("value", AutoStart ? "true" : "false");
                        break;
                    case CONFIG_KEY_SET_PROXY:
                        el.SetAttribute("value", SetProxy ? "true" : "false");
                        break;
                    case CONFIG_KEY_PROXY_TYPE:
                        el.SetAttribute("value", ProxyType ?? "");
                        break;
                    case CONFIG_KEY_PROXY_HOST:
                        el.SetAttribute("value", ProxyHost ?? "");
                        break;
                    case CONFIG_KEY_PROXY_PORT:
                        el.SetAttribute("value", ProxyPort.ToString());
                        break;
                }
            }

            var userRulesNode = userRulesNodes.Count > 0 ? userRulesNodes.Item(0) : doc.CreateElement("userRules");
            if (userRulesNodes.Count == 0) {
                doc.AppendChild(userRulesNode);
            }
            userRulesNode.RemoveAll();
            foreach (var rule in UserRules) {
                var node = doc.CreateElement("userRule");
                node.InnerText = rule;
                userRulesNode.AppendChild(node);
            }

            doc.Save(xmlFileName);
        }
    }
}
