namespace SetoClass.DTOs.Game
{
    public class GameReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string GameAuthor { get; set; }

        public string GameGenre { get; set; }

        public int LikeCount { get; set; }

        public int SaveCount { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string CoverImageUrl { get; set; }

        public string GameUrl { get; set; }
    }

}
