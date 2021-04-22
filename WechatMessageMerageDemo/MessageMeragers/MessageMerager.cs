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
            var mobileMessagesByTalker = mobileMessages
                .GroupBy(m => new TalkerKey() { Talker = m.Talker, IsSend = m.IsSend }, new TalkerEqualityComparer())
                .OrderBy(g => g.Key.Talker)
                .ToDictionary(g => g.Key, new TalkerEqualityComparer());
            var pcMessagesByTalker = pcMessages
                .GroupBy(m => new TalkerKey() { Talker = m.Talker, IsSend = m.IsSend }, new TalkerEqualityComparer())
                .OrderBy(g => g.Key.Talker)
                .ToDictionary(g => g.Key, new TalkerEqualityComparer());
            var meragedMessages = new List<WechatMessage>(mobileMessages.Count() + pcMessages.Count());
            meragedMessages.AddRange(mobileMessages);
            foreach (var pcTalkerMessages in pcMessagesByTalker)
            {
                if (mobileMessagesByTalker.TryGetValue(new TalkerKey { Talker = pcTalkerMessages.Key.Talker, IsSend = pcTalkerMessages.Key.IsSend }, out var mobileTalkerMessages))
                {
                    var mobileMessagesHashSet = new HashSet<WechatMessage>(mobileTalkerMessages, new MessageEqualityComparer());
                    foreach (var pcMessage in pcTalkerMessages.Value)
                    {
                        if (!mobileMessagesHashSet.Contains(pcMessage))
                        {
                            meragedMessages.Add(pcMessage);
                        }
                    }
                }
                else
                {
                    meragedMessages.AddRange(pcTalkerMessages.Value.AsEnumerable());
                }
            }
            foreach (var talkerMessages in meragedMessages.GroupBy(m => new TalkerKey() { Talker = m.Talker, IsSend = m.IsSend }, new TalkerEqualityComparer()).OrderBy(g => g.Key.Talker))
            {
                int mobileMessageCount = mobileMessagesByTalker.TryGetValue(new TalkerKey() { Talker = talkerMessages.Key.Talker, IsSend = talkerMessages.Key.IsSend }, out var messages) ? messages.Count() : 0;
                int pcMessageCount = pcMessagesByTalker.TryGetValue(new TalkerKey() { Talker = talkerMessages.Key.Talker, IsSend = talkerMessages.Key.IsSend }, out messages) ? messages.Count() : 0;
                Console.WriteLine($" ===> {talkerMessages.Key.Talker} {(talkerMessages.Key.IsSend ? "Send" : "Receive")} {mobileMessageCount + pcMessageCount} messsages totally.");
                Console.WriteLine($"\t===> Removed {talkerMessages.Count()} duplicated messages.");
            }

            return meragedMessages;
        }
    }
}
