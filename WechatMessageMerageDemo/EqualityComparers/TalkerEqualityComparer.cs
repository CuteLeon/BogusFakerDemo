using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WechatMessageMerageDemo.Helpers;
using WechatMessageMerageDemo.Models;

namespace WechatMessageMerageDemo.EqualityComparers
{
    public class TalkerEqualityComparer : IEqualityComparer<TalkerKey>
    {
        public bool Equals(TalkerKey x, TalkerKey y)
        {
            return KMPHeler.Equals(x.Talker, y.Talker) && x.IsSend == y.IsSend;
        }

        public int GetHashCode([DisallowNull] TalkerKey obj) => obj.GetHashCode();
    }
}
