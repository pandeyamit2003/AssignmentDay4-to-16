using System;
using System.Collections.Generic;
class SocialMediaSystem
{
    private List<string> posts = new List<string>();
    private Dictionary<string, int> likes = new Dictionary<string, int>();
    private HashSet<int> users = new HashSet<int>();
    private Stack<string> actions = new Stack<string>();
    private Queue<string> notifications = new Queue<string>();
    // Add User (ensures uniqueness)
    public void AddUser(int userId)
    {
        if (users.Add(userId))
        {
            Console.WriteLine($"User {userId} added.");
        }
        else
        {
            Console.WriteLine($"User {userId} already exists.");
        }
    }
    // Add Post
    public void AddPost(string post)
    {
        posts.Add(post);
        likes[post] = 0;
        actions.Push($"Added Post: {post}");
        notifications.Enqueue($"New post added: {post}");
        Console.WriteLine("Post added.");
    }
    // Like Post
    public void LikePost(string post)
    {
        if (likes.ContainsKey(post))
        {
            likes[post]++;
            actions.Push($"Liked Post: {post}");
            notifications.Enqueue($"Post liked: {post}");
            Console.WriteLine("Post liked.");
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
    }
    // Undo Last Action (LIFO)
    public void UndoAction()
    {
        if (actions.Count > 0)
        {
            string lastAction = actions.Pop();
            Console.WriteLine($"Undoing: {lastAction}");
        }
        else
        {
            Console.WriteLine("No actions to undo.");
        }
    }
    // Process Notifications (FIFO)
    public void ProcessNotifications()
    {
        Console.WriteLine("\nProcessing Notifications:");
        while (notifications.Count > 0)
        {
            Console.WriteLine(notifications.Dequeue());
        }
    }
    // Display Posts and Likes
    public void ShowPosts()
    {
        Console.WriteLine("\nPosts and Likes:");
        foreach (var post in posts)
        {
            Console.WriteLine($"{post}-> {likes[post]} likes");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        SocialMediaSystem system = new SocialMediaSystem();
        // Add Users
        system.AddUser(1);
        system.AddUser(2);
        system.AddUser(1); // duplicate check
                           // Add Posts
        system.AddPost("Hello World!");
        system.AddPost("Learning C# Collections");
        // Like Posts
        system.LikePost("Hello World!");
        system.LikePost("Hello World!");
        system.LikePost("Learning C# Collections");
        // Show Posts
        system.ShowPosts();
        // Undo Actions
        system.UndoAction();
        system.UndoAction();
        // Process Notifications
        system.ProcessNotifications();
        Console.WriteLine("\nExecution Completed.");
    }
}
