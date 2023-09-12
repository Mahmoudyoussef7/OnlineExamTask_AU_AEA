namespace OnlineExam.SQL.Queries;

public static class AnswersQueries
{
    public static string AllAnswers => "SELECT * FROM [Answers] (NOLOCK)";

    public static string AnswerById => "SELECT * FROM [Answers] (NOLOCK) WHERE [Id] = @Id";
    public static string GetAnswerStudentForQuestion => "SELECT * FROM [Answers] (NOLOCK) WHERE [QuestionId] = @QuestionId and [UserId] = @UserId and [ExamId]=@ExamId";
    public static string GetAnswerExamUser => "SELECT * FROM [Answers] (NOLOCK) WHERE [UserId] = @UserId and [ExamId]=@ExamId";

    public static string AddAnswer =>
        @"INSERT INTO [Answers] ([Id],[UserId], [ExamId], [QuestionId], [AnswerText],[SelectedChoiceId]) 
            VALUES (@Id, @StudentId, @ExamId, @QuestionId,@AnswerText,@SelectedChoiceId)";

    public static string UpdateAnswer =>
        @"UPDATE [Answers] 
        SET [UserId] = @UserId, 
            [ExamId] = @ExamId, 
            [QuestionId] = @QuestionId, 
            [AnswerText] = @AnswerText,
            [SelectedChoiceId]=@SelectedChoiceId
        WHERE [Id] = @Id";

    public static string DeleteAnswer => "DELETE FROM [Answers] WHERE [Id] = @Id";
}
