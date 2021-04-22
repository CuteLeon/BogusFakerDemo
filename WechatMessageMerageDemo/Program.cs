using System;
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

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var mobileMessages = mobileWechatMessageProvider.GetWechatMessages();
            var pcMessages = pcWechatMessageProvider.GetWechatMessages();
            Console.WriteLine($"Got Mobile Wechat Messages: {mobileMessages.Count()}");
            Console.WriteLine($"Got PC Wechat Messages: {pcMessages.Count()}");

            Console.WriteLine("<—————————— Start merage... ——————————>");
            var meragedMessages = messageMerager.Merage(mobileMessages, pcMessages);
            Console.WriteLine("<—————————— Merage Finish. ——————————>");
            Console.WriteLine($"Meraged Wechat Messages: {meragedMessages.Count()}");

            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }
    }
}
