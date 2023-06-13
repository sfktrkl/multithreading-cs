using Xunit;

using System;
using System.Threading.Tasks;

namespace S01_TaskProgramming;

public partial class S01_01_CreatingStartingTasks
{
    public static int? TextLength(object? o)
    {
        Console.WriteLine($"Task with id {Task.CurrentId} processing object '{o}'...");
        return o?.ToString()?.Length;
    }

    [Fact]
    public void TasksWithReturnValues()
    {
        string text1 = "testing";
        string text2 = "this";

        var task1 = new Task<int?>(TextLength, text1);
        task1.Start();
        var task2 = Task.Factory.StartNew(TextLength, text2);

        // Getting the result is a blocking operation!
        Console.WriteLine($"Length of '{text1}' is {task1.Result}.");
        Console.WriteLine($"Length of '{text2}' is {task2.Result}.");
    }
}
