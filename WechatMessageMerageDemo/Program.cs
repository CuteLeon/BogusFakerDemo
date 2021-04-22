using System;
using System.Diagnostics;
using System.Linq;
using WechatMessageMerageDemo.DataProviders;
using WechatMessageMerageDemo.MessageMeragers;

namespace WechatMessageMerageDemo
{
    class Program
    {
        static WechatMessageProvider mobileWechatMessageProvider = new MobileWechatMessageProvider();
        static WechatMessageProvider pcWechatMessageProvider = new PCWechatMessageProvider();
        static MessageMerager messageMerager = new MessageMerager();
        static Stopwatch stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            stopwatch.Start();
            var mobileMessages = mobileWechatMessageProvider.GetWechatMessages();
            var pcMessages = pcWechatMessageProvider.GetWechatMessages();
            Console.WriteLine($"Got Mobile Wechat Messages: {mobileMessages.Count()}");
            Console.WriteLine($"Got PC Wechat Messages: {pcMessages.Count()}");

            Console.WriteLine("<—————————— Start merage... ——————————>");
            var meragedMessages = messageMerager.Merage(mobileMessages, pcMessages).ToList();
            Console.WriteLine("<—————————— Merage Finish. ——————————>");
            stopwatch.Stop();
            Console.WriteLine($"Total messages: {mobileMessages.Count() + pcMessages.Count()}, Unique messages: {meragedMessages.Count()}, cost time: {stopwatch.Elapsed.TotalSeconds} s.");

            Console.ReadLine();
        }
    }
}
