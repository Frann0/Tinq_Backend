
using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.WebApi.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public List<Tag> Tags {get;set;}

        public PostDto(Post post)
        {
            Id = post.Id;
            Body = post.Body;
            Title = post.Title;
            UserId = post.UserId;
            Tags=post.Tags;
        }
    }
}