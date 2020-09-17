using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetGram.State
{
    public class ApplicationState
    {
        private readonly Random _random;

        public ApplicationState()
        {
            Users = new Dictionary<string, User>();
            _random = new Random();
        }

        public Dictionary<string, User> Users { get; }

        public void SignInUser(string userName, ISignedIn currentUser)
        {
            if (Users.TryGetValue(userName, out var user))
            {
                user.IsSignedIn = true;
                currentUser.Apply(user);
            }
            else
            {
                var pic = Avatars.Avatar[_random.Next(0, Avatars.Avatar.Length)];
                var newUser = new User {Name = userName, IsSignedIn = true, ProfilePicture = pic};
                Users.Add(userName, newUser);
                NewUserSignedIn?.Invoke();
                currentUser.Apply(newUser);
            }
        }

        public void AddPostToUser(User user, Post post)
        {
            var userInState = Users[user.Name];
            userInState.Posts.Add(post);
            PostsUpdated?.Invoke(userInState.Name);
            user.Apply(userInState);
        }

        public void AddComment(string postOwner, Guid postId, Comment comment)
        {
            var userInState = Users[postOwner];
            var post = userInState.Posts.First(p => p.Id == postId);
            post.Comments.Add(comment);

            PostsUpdated?.Invoke(postOwner);
        }

        public event Action NewUserSignedIn;
        public event Action<string> PostsUpdated;
    }
}