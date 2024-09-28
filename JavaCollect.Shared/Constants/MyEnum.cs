namespace JavaCollect.Shared.Constants
{
    public enum WebsiteType
    {
        未知 = 0,

        Sht = 1,

        Trust = 99,
    }

    public enum ShtType
    {
        未知 = 0,
        亚洲有码原创 = 1,
        高清中文字幕 = 2,
        国产原创 = 3,
        亚洲无码原创 = 4,
        求片问答悬赏区 = 5,
        综合讨论区 = 6,
        转帖交流区 = 7,
        资源出售区 = 8,
        自提字幕区 = 9,
        投诉建议区 = 10,
        网友原创区 = 11,
        卡通动漫 = 12,
        亚洲性爱 = 13,
    }

    public static class ShtTypeExtensions
    {
        public static bool NeedCollect(this ShtType type)
        {
            return type == ShtType.高清中文字幕 || type == ShtType.亚洲有码原创 || type == ShtType.国产原创;
        }
    }

}
