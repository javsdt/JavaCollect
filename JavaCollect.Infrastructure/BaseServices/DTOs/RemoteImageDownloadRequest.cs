namespace JavaCollect.Infrastructure.BaseServices.DTOs
{
    internal class RemoteImageDownloadRequest
    {
        public string PageUrl { get; set; } = default!;

        public string ImageXPath { get; set; } = default!;

        public string? ObstacleXPath { get; set; }

        public int DownCount { get; set; }

        public string ImageName { get; set; } = $"{Guid.NewGuid()}.jpg";
    }
}
