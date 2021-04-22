using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WechatMessageMerageDemo.Helpers;

namespace WechatMessageMerageDemo.EqualityComparers
{
    public class MessageDateEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return KMPHeler.Equals(x, y);
        }

        public int GetHashCode([DisallowNull] string obj) => obj.GetHashCode();
    }
}
