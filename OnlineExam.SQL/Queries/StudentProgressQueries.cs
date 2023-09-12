namespace OnlineExam.SQL.Queries;

public static class StudentProgressQueries
{
    public static string AllStudentProgress => "SELECT * FROM [StudentProgress] (NOLOCK)";

    public static string StudentProgressByUserId => "SELECT * FROM [StudentProgress] (NOLOCK) WHERE [UserId] = @UserId";
    public static string ExamProgressByUserId => "SELECT * FROM [StudentProgress] (NOLOCK) WHERE [ExamId] = @ExamId and [UserId] = @UserId";

    public static string AddStudentProgress =>
        @"INSERT INTO [StudentProgress] ([Id], [UserId], [ExamId],[Score],[Timestamp]) 
            VALUES (@Id, @UserId, @ExamId, @Score, @Timestamp)";

    public static string UpdateStudentProgress =>
    @"UPDATE [StudentProgress] 
        SET  [UserId] = @UserId,
             [ExamId] = @ExamId,
             [Score] = @Score,
             [Timestamp] = @Timestamp
        WHERE [Id] = @Id";

    public static string DeleteStudentProgress => "DELETE FROM [StudentProgress] WHERE [Id] = @Id";
}
