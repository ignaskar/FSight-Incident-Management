using System;

namespace FSight.API.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Body { get; set; }
    }
}