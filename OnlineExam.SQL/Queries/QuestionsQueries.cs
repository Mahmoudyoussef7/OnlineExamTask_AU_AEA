namespace OnlineExam.SQL.Queries;

public static class QuestionsQueries
{
    public static string AllQuestions => "SELECT * FROM [Questions] (NOLOCK)";

    public static string QuestionById => "SELECT * FROM [Questions] (NOLOCK) WHERE [Id] = @Id";
    public static string QuestionByExamId => "SELECT * FROM [Questions] (NOLOCK) WHERE [ExamId] = @Id";

    public static string AddQuestion =>
        @"INSERT INTO [Questions] ([Id], [ExamId], [QuestionText], [QuestionTypeId], [CorrectAnswer], [Points]) 
            VALUES (@Id, @ExamId,@QuestionText,@QuestionTypeId, @CorrectAnswer, @Points)";                    

    public static string UpdateQuestion =>
        @"UPDATE [Questions] 
        SET [ExamId] = @ExamId, 
            [QuestionText] = @QuestionText, 
            [QuestionTypeId] = @QuestionTypeId,
            [CorrectAnswer] = @CorrectAnswer,
            [Points] = @Points
        WHERE [Id] = @Id";

    public static string DeleteQuestion => "DELETE FROM [Questions] WHERE [Id] = @Id";
}
