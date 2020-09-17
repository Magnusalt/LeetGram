using System;

namespace LeetGram.State
{
    public class Comment
    {
        public Comment()
        {
            Id = Guid.NewGuid();
        }

        public DateTime CommentDate { get; set; }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Value { get; set; }
    }
}