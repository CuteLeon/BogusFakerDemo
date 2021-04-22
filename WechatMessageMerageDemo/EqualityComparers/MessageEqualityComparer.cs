using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WechatMessageMerageDemo.Helpers;
using WechatMessageMerageDemo.Models;

namespace WechatMessageMerageDemo.EqualityComparers
{
    public class MessageEqualityComparer : IEqualityComparer<WechatMessage>
    {
        public bool Equals(WechatMessage x, WechatMessage y)
        {
            return KMPHeler.Equals(x.CreateTime, y.CreateTime);
        }

        public int GetHashCode([DisallowNull] WechatMessage obj) => obj.CreateTime.GetHashCode();
    }
}
