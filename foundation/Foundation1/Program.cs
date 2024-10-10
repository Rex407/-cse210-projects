using System;
using System.Collections.Generic;

// Comment class
public class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

// Video class
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> comments;

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        comments = new List<Comment>();
    }

    // Method to add a comment
    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    // Method to return the number of comments
    public int GetCommentCount()
    {
        return comments.Count;
    }

    // Method to display comments
    public void DisplayComments()
    {
        foreach (var comment in comments)
        {
            Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
        }
    }
}

// Main program
class Program
{
    static void Main()
    {
        // Create a list to hold videos
        List<Video> videos = new List<Video>();

        // Create videos and add comments
        Video video1 = new Video("Learning C#", "John Doe", 300);
        video1.AddComment(new Comment("Alice", "Great tutorial!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "Can you explain this part more?"));
        videos.Add(video1);

        Video video2 = new Video("Understanding OOP", "Jane Smith", 450);
        video2.AddComment(new Comment("David", "Loved the examples!"));
        video2.AddComment(new Comment("Eva", "This really clears things up."));
        videos.Add(video2);

        Video video3 = new Video("Advanced C# Techniques", "Michael Brown", 600);
        video3.AddComment(new Comment("Frank", "Amazing content!"));
        video3.AddComment(new Comment("Grace", "Looking forward to more videos!"));
        video3.AddComment(new Comment("Hannah", "Can you do a follow-up?"));
        videos.Add(video3);

        // Display video details and comments
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            video.DisplayComments();
            Console.WriteLine();
        }
    }
}