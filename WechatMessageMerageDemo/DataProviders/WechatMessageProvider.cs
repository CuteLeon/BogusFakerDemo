using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using WechatMessageMerageDemo.Models;

namespace WechatMessageMerageDemo.DataProviders
{
    public abstract class WechatMessageProvider
    {
        protected virtual string[] Talkers { get; set; }
        protected Faker<WechatMessage> MessageFaker { get; set; }

        public WechatMessageProvider()
        {
            this.MessageFaker = new Faker<WechatMessage>()
                .StrictMode(true)
                .RuleFor(m => m.Talker, faker => faker.PickRandom(this.Talkers))
                .RuleFor(m => m.IsSend, faker => faker.Random.Bool())
                .RuleFor(m => m.Content, faker => faker.Lorem.Text())
                .RuleFor(m => m.CreateTime, faker => faker.Date
                    .Between(
                        new DateTime(2021, 5, 1, 12, 0, 0), 
                        new DateTime(2021, 5, 1, 13, 0, 0))
                    .ToString("yyyy-MM-dd HH:mm:000"));
        }

        public virtual IEnumerable<WechatMessage> GetWechatMessages()
        {
            return this.MessageFaker
                .GenerateForever()
                .Take(new Random().Next(1000, 2000))
                .ToList();
        }
    }
}
