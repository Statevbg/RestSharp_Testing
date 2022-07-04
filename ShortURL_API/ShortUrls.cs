namespace Exam_SeleniumWebDriverUI_Tests
{
    public class ShortUrls
    {
        public string url { get; set; }
        public string shortCode { get; set; }
        public string shortUrl { get; set; }
        public string dateCreated { get; set; }
        public int visits { get; set; }

        public string errMsg { get; set; }
        public Msg msg { get; set; }
    }
    public class Msg
    {
        public string msg { get; set; }

    }
}