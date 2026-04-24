using System;
using System.Collections.Generic;
class MusicPlaylistManager
{
    // Playlist (easy insert/remove)
    private LinkedList<string> playlist = new LinkedList<string>();
    // Songs sorted by rating (key = rating, value = song)
    private SortedList<int, string> songsByRating = new SortedList<int,
    string>();
    // Artist → Song (sorted by artist name)
    private SortedDictionary<string, string> artistSongs = new
    SortedDictionary<string, string>();
    // Add song to playlist
    public void AddSong(string song)
    {
        playlist.AddLast(song);
        Console.WriteLine($"Added to playlist: {song}");
    }
    // Remove song from playlist
    public void RemoveSong(string song)
    {
        if (playlist.Remove(song))
            Console.WriteLine($"Removed from playlist: {song}");
        else
        {
            Console.WriteLine("Song not found in playlist.");
        }
    }
    // Add song with rating
    public void AddSongWithRating(int rating, string song)
    {
        if (!songsByRating.ContainsKey(rating))
        {
            songsByRating.Add(rating, song);
            Console.WriteLine($"Added song '{song}' with rating {rating}");
        }
        else
        {
            Console.WriteLine("Rating already exists. Use unique ratings.");
        }
    }

    // Add artist-song mapping
    public void AddArtistSong(string artist, string song)
    {
        artistSongs[artist] = song;
        Console.WriteLine($"Mapped Artist '{artist}' → Song '{song}'");
    }
    // Display Playlist
    public void ShowPlaylist()
    {
        Console.WriteLine("\nPlaylist:");
        foreach (var song in playlist)
        {
            Console.WriteLine(song);
        }
    }
    // Display songs sorted by rating
    public void ShowSongsByRating()
    {
        Console.WriteLine("\nSongs by Rating (Sorted):");
        foreach (var item in songsByRating)
        {
            Console.WriteLine($"Rating {item.Key} → {item.Value}");
        }
    }

    // Display songs sorted by artist
    public void ShowSongsByArtist()
    {
        Console.WriteLine("\nSongs by Artist (Sorted):");
        foreach (var item in artistSongs)
        {
            Console.WriteLine($"{item.Key} → {item.Value}");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        MusicPlaylistManager manager = new MusicPlaylistManager();
        // Add songs to playlist
        manager.AddSong("Song A");
        manager.AddSong("Song B");
        manager.AddSong("Song C");
        // Remove a song
        manager.RemoveSong("Song B");
        // Add songs with ratings
        manager.AddSongWithRating(5, "Song A");
        manager.AddSongWithRating(3, "Song C");
        manager.AddSongWithRating(4, "Song D");
        // Add artist-song mapping
        manager.AddArtistSong("Rahat Fateh Ali Kha", "Duaa");
        manager.AddArtistSong("Atif Aslam", "Jeene Laga Hoon");
        manager.AddArtistSong("Shreya Ghoshal", "Sun Raha Hai");
        // Display data
        manager.ShowPlaylist();
        manager.ShowSongsByRating();
        manager.ShowSongsByArtist();
        Console.WriteLine("\nExecution Completed.");
    }
}