namespace SetoClass.DTOs.Blog
{
    public class BlogReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public int Saves { get; set; }

        public int Views { get; set; }

        public string ImageUrl { get; set; }
    }

}
