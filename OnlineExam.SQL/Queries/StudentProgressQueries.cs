namespace OnlineExam.SQL.Queries;

public static class StudentProgressQueries
{
    public static string AllStudentProgress => "SELECT * FROM [Examinations] (NOLOCK)";

    public static string StudentProgressByUserId => "SELECT * FROM [Examinations] (NOLOCK) WHERE [UserId] = @UserId";

    public static string AddStudentProgress =>
        @"INSERT INTO [StudentProgress] ([Id], [UserId], [ExamId], [IsCompleted],[Score],[Timestamp]) 
            VALUES (@Id, @UserId, @ExamId, @IsCompleted, @Score, @Timestamp)";
}
