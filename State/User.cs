using System;
using System.Collections.Generic;

namespace LeetGram.State
{
    public interface ISignedIn
    {
        string Name { get; }
        bool IsSignedIn { get; }
        event Action ModelUpdated;
        void Apply(User user);
    }

    public class User : ISignedIn
    {
        public User()
        {
            Posts = new List<Post>();
        }

        public string ProfilePicture { get; set; }
        public List<Post> Posts { get; private set; }
        public string Name { get; set; }
        public bool IsSignedIn { get; set; }

        public void Apply(User user)
        {
            ProfilePicture = user.ProfilePicture;
            Name = user.Name;
            IsSignedIn = user.IsSignedIn;
            Posts = user.Posts;
            ModelUpdated?.Invoke();
        }

        public event Action ModelUpdated;
    }
}