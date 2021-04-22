namespace WechatMessageMerageDemo.DataProviders
{
    public class MobileWechatMessageProvider : WechatMessageProvider
    {
        protected override string[] Talkers { get; set; } = new string[] { "Leon", "Jaci", "Lilac", "Cindy", "Jeremy", "Frank" };
    }
}
