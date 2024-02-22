
using korp_1.Models;
using korp_1;
using System.Numerics;

var filepath = $"{Directory.GetCurrentDirectory()}\\last_json.json";

if (!File.Exists(filepath))
{
    Console.WriteLine("File doesn't exist");
    throw new Exception("File doesn't exist");
}
var serializer = new SerializeBasic<Structure>(filepath);
var data = serializer.Deserialize();
while (true)
{
    Console.WriteLine("Choice your action:");
    Console.WriteLine("1 - AddMark");
    Console.WriteLine("2 - StudentInfo");

    var input = int.Parse(Console.ReadLine());

    if (input == 1)
    {
        Console.WriteLine("Write his mark:");
        var mark = int.Parse(Console.ReadLine());
        Console.WriteLine("Write subject code:");
        var subject = int.Parse(Console.ReadLine());
        Console.WriteLine("Write student code:");
        var student = int.Parse(Console.ReadLine());
        Grade syllafbus = new Grade
        {
            Subject_code = subject,
            Student_code = student,
            Mark = mark
        };
        data.Grades.Add(syllafbus);
        serializer.WriteToJson(data);
    }

    if (input == 2)
    {
        Console.WriteLine("Write student code:");
        var student_code = int.Parse(Console.ReadLine());

        if (!data.Students.Exists(s => s.Code == student_code))
        {
            Console.WriteLine("Student doesn`t exist");
            throw new Exception("Student doesn`t exist");
        }

        Console.WriteLine("Student:");
        var student = data.Students.First(s => s.Code == student_code);
        var grades = data.Grades.Where(p => p.Student_code == student_code).ToList();
        Console.WriteLine($"Фамилия: {student.Last_name}\n" +
                                         $"Имя: {student.First_name}\n" +
                                         $"Отчество: {student.Middle_name}\n");
        Console.WriteLine("His marks:");
        foreach (var grade in grades)
        {
            Console.WriteLine($"Дисциплина: {data.Subjects.First(s => s.Code == grade.Subject_code).Title}\n" +
                                              $"Оценка: {grade.Mark}\n" +
                                              $"...");
        }

        var percentage = new
        {
            great = (double)grades.Count(p => p.Mark == 5) / grades.Count,
            good = (double)grades.Count(p => p.Mark == 4) / grades.Count,
            satisfactory = (double)grades.Count(p => p.Mark == 3) / grades.Count,
        };

        Console.WriteLine($"Процент оценок:\n" +
                          $"Отлично: {Math.Round(percentage.great * 100)}%,\n" +
                          $"Хорошо: {Math.Round(percentage.good * 100)}%,\n" +
                          $"Удовлетворительно: {Math.Round(percentage.satisfactory * 100)}%");
    }
}