namespace OnlineExam.SQL.Queries;

public static class ExaminationsQueries
{
    public static string AllExaminations => "SELECT * FROM [Examinations] (NOLOCK)";

    public static string ExaminationById => "SELECT * FROM [Examinations] (NOLOCK) WHERE [Id] = @Id";
    public static string ExaminationByUserId => "SELECT * FROM [Examinations] (NOLOCK) WHERE [UserId] = @UserId";

    public static string AddExamination =>
        @"INSERT INTO [Examinations] ([Id], [ExamTitle], [ExamDescription], [StartDate],[EndDate],[QuestionCount],[DurationInHours]) 
            VALUES (@Id, @ExamTitle, @ExamDescription, @StartDate, @EndDate, @QuestionCount, @DurationInHours)";

    public static string UpdateExamination =>
        @"UPDATE [Examinations] 
        SET [ExamTitle] = @ExamTitle, 
            [ExamDescription] = @ExamDescription, 
            [StartDate] = @StartDate,
            [EndDate] = @EndDate,
            [QuestionCount] = @QuestionCount,
            [DurationInHours] = @DurationInHours
        WHERE [Id] = @Id";

    public static string DeleteExamination => "DELETE FROM [Examinations] WHERE [Id] = @Id";
}
