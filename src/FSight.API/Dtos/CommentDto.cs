namespace FSight.API.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? DeveloperName { get; set; }
        public string? ProjectManagerName { get; set; }
        public string Body { get; set; }
    }
}