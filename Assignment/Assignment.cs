using System;
using System.Collections.Generic;
using System.IO;

//Custom Attribute
[AttributeUsage(AttributeTargets.Class)]
class StudentInfoAttribute : Attribute
{
    public string Info { get; }
    public StudentInfoAttribute(string info)
    {
        Info = info;
    }
}

//Student Class (OOP)
[StudentInfo("This class stores student details")]
class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Marks { get; set; }
}

//Student Collection with Indexer
class StudentCollection
{
    private List<Student> students = new List<Student>();

    public Student this[int index]
    {
        get { return students[index]; }
        set { students[index] = value; }
    }

    public void Add(Student s)
    {
        students.Add(s);
    }

    public int Count => students.Count;

    public List<Student> GetAll()
    {
        return students;
    }
}

class Assignment
{
    //Linear Search
    static int LinearSearch(List<Student> list, int id)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Id == id)
                return i;
        }
        return -1;
    }

    //Binary Search (on sorted list by Id)
    static int BinarySearch(List<Student> list, int id)
    {
        int left = 0, right = list.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (list[mid].Id == id)
                return mid;
            else if (list[mid].Id < id)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }

    //Bubble Sort (O(n²))
    static void BubbleSort(List<Student> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                if (list[j].Marks > list[j + 1].Marks)
                {
                    var temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    //Merge Sort (Efficient)
    static List<Student> MergeSort(List<Student> list)
    {
        if (list.Count <= 1)
            return list;

        int mid = list.Count / 2;
        var left = MergeSort(list.GetRange(0, mid));
        var right = MergeSort(list.GetRange(mid, list.Count - mid));

        return Merge(left, right);
    }

    static List<Student> Merge(List<Student> left, List<Student> right)
    {
        List<Student> result = new List<Student>();

        int i = 0, j = 0;

        while (i < left.Count && j < right.Count)
        {
            if (left[i].Marks < right[j].Marks)
                result.Add(left[i++]);
            else
                result.Add(right[j++]);
        }

        while (i < left.Count) result.Add(left[i++]);
        while (j < right.Count) result.Add(right[j++]);

        return result;
    }

    //File Handling
    static string filePath = "students.txt";

    static void WriteToFile(List<Student> list)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var s in list)
                sw.WriteLine($"{s.Id},{s.Name},{s.Marks}");
        }
    }

    static void AppendToFile(Student s)
    {
        using (StreamWriter sw = new StreamWriter(filePath, true))
        {
            sw.WriteLine($"{s.Id},{s.Name},{s.Marks}");
        }
    }

    static void ReadFile()
    {
        if (File.Exists(filePath))
        {
            Console.WriteLine("\nFile Data:");
            foreach (var line in File.ReadAllLines(filePath))
                Console.WriteLine(line);
        }
    }

    static void CopyFile()
    {
        File.Copy(filePath, "backup.txt", true);
    }

    static void DeleteFile()
    {
        if (File.Exists("backup.txt"))
            File.Delete("backup.txt");
    }

    //Display Students
    static void Display(List<Student> list)
    {
        foreach (var s in list)
            Console.WriteLine($"{s.Id} {s.Name} {s.Marks}");
    }

    static void Main()
    {
        StudentCollection sc = new StudentCollection();

        sc.Add(new Student { Id = 1, Name = "Amit", Marks = 85 });
        sc.Add(new Student { Id = 2, Name = "Aditya", Marks = 60 });
        sc.Add(new Student { Id = 3, Name = "Aman", Marks = 95 });

        var list = sc.GetAll();

        //Searching
        Console.WriteLine("Linear Search (Id=2): " + LinearSearch(list, 2));

        list.Sort((a, b) => a.Id.CompareTo(b.Id));
        Console.WriteLine("Binary Search (Id=3): " + BinarySearch(list, 3));

        //Sorting
        Console.WriteLine("\nBubble Sort:");
        BubbleSort(list);
        Display(list);

        Console.WriteLine("\nMerge Sort:");
        var sorted = MergeSort(list);
        Display(sorted);

        //File Handling
        WriteToFile(list);
        AppendToFile(new Student { Id = 4, Name = "Darshan", Marks = 70 });
        ReadFile();
        CopyFile();
        DeleteFile();

        //Algorithm Comparison
        Console.WriteLine("\nAlgorithm Comparison:");
        Console.WriteLine("Bubble Sort -> Time: O(n^2), Space: O(1), Best for small data");
        Console.WriteLine("Merge Sort -> Time: O(n log n), Space: O(n), Best for large data");
    }
}

