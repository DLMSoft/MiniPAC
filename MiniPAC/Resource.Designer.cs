//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DLMSoft.MiniPAC {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DLMSoft.MiniPAC.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似于 (图标) 的 System.Drawing.Icon 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Icon ICO_NOTIFY {
            get {
                object obj = ResourceManager.GetObject("ICO_NOTIFY", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 The URL of PAC has been copied to clipboard. 的本地化字符串。
        /// </summary>
        internal static string MESSAGE_ALERT_URL_COPIED {
            get {
                return ResourceManager.GetString("MESSAGE_ALERT_URL_COPIED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Are you sure want to exit MiniPAC ? 的本地化字符串。
        /// </summary>
        internal static string MESSAGE_CONFIRM_EXIT {
            get {
                return ResourceManager.GetString("MESSAGE_CONFIRM_EXIT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Are you sure want to remove rule &quot;{0}&quot;? 的本地化字符串。
        /// </summary>
        internal static string MESSAGE_CONFIRM_USER_RULE_REMOVE {
            get {
                return ResourceManager.GetString("MESSAGE_CONFIRM_USER_RULE_REMOVE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 There&apos;s can only one instance of MiniPAC. 的本地化字符串。
        /// </summary>
        internal static string MESSAGE_WARN_INSTANCE_EXISTS {
            get {
                return ResourceManager.GetString("MESSAGE_WARN_INSTANCE_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Error occured when {0}: {1} 的本地化字符串。
        /// </summary>
        internal static string MSG_WARN_ERROR_WHEN {
            get {
                return ResourceManager.GetString("MSG_WARN_ERROR_WHEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 MiniPAC 的本地化字符串。
        /// </summary>
        internal static string PRODUCT_NAME {
            get {
                return ResourceManager.GetString("PRODUCT_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Stopped 的本地化字符串。
        /// </summary>
        internal static string SERVICE_STATUS_INSTALLED {
            get {
                return ResourceManager.GetString("SERVICE_STATUS_INSTALLED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Not Installed 的本地化字符串。
        /// </summary>
        internal static string SERVICE_STATUS_NOT_INSTALLED {
            get {
                return ResourceManager.GetString("SERVICE_STATUS_NOT_INSTALLED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Running 的本地化字符串。
        /// </summary>
        internal static string SERVICE_STATUS_RUNNING {
            get {
                return ResourceManager.GetString("SERVICE_STATUS_RUNNING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 setting auto start 的本地化字符串。
        /// </summary>
        internal static string TIMING_SET_AUTO_START {
            get {
                return ResourceManager.GetString("TIMING_SET_AUTO_START", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 setting system proxy 的本地化字符串。
        /// </summary>
        internal static string TIMING_SET_PROXY {
            get {
                return ResourceManager.GetString("TIMING_SET_PROXY", resourceCulture);
            }
        }
    }
}
