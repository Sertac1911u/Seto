namespace SetoClass.DTOs.Video
{
    public class VideoReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Desc { get; set; }

        public string Author { get; set; }

        public DateTime UploadDate { get; set; }

        public int Views { get; set; }

        public int Saves { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public string CoverImageUrl { get; set; }

        public string VideoUrl { get; set; }
    }

}
