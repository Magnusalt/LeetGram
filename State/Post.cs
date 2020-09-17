using System;
using System.Collections.Generic;

namespace LeetGram.State
{
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid();
            Comments = new List<Comment>();
        }

        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Caption { get; set; }
        public int Likes { get; set; }
        public DateTime PostDate { get; set; }
        public List<Comment> Comments { get; set; }
    }
}