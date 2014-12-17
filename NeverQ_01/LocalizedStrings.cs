using NeverQ_01.Resources;

namespace NeverQ_01
{
    /// <summary>
    /// 提供字串資源的存取權。
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}