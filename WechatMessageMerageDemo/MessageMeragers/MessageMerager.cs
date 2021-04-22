using System;
using System.Collections.Generic;
using System.Linq;
using WechatMessageMerageDemo.EqualityComparers;
using WechatMessageMerageDemo.Models;

namespace WechatMessageMerageDemo.MessageMeragers
{
    public class MessageMerager
    {
        public IEnumerable<WechatMessage> Merage(IEnumerable<WechatMessage> mobileMessages, IEnumerable<WechatMessage> pcMessages)
        {
            var messagesByTalker = mobileMessages.Union(pcMessages)
                .GroupBy(m => new TalkerKey() { Talker = m.Talker, IsSend = m.IsSend }, new TalkerEqualityComparer())
                .OrderBy(g => g.Key.Talker)
                .ToList();
            foreach (var talkerMessages in messagesByTalker)
            {
                Console.WriteLine($" ===> {talkerMessages.Key.Talker} {(talkerMessages.Key.IsSend ? "Send" : "Receive")} {talkerMessages.Count()} messsages totally.");
                var uniqueMessages = talkerMessages
                    .GroupBy(m => m.CreateTime, new MessageDateEqualityComparer())
                    .ToList();
                Console.WriteLine($"\t===> Removed {talkerMessages.Count() - uniqueMessages.Count()} duplicated messages.");
            }

            return Enumerable.Empty<WechatMessage>();
        }
    }
}
