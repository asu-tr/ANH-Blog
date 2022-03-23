namespace Blog.Entities
{
    public class Comment : EntitiesBase
    {
        public int Rating { get; set; }
        public string Text { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
