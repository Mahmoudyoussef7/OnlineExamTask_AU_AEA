namespace OnlineExam.SQL.Queries;

public static class QuestionChoicesQueries
{
    public static string AllQuestionChoices => "SELECT * FROM [QuestionChoices] (NOLOCK)";
    public static string ChoicesByQuestionIdAsync => "SELECT [ChoiceId] FROM [QuestionChoices] (NOLOCK) WHERE [QuestionId] = @QuestionId";

    public static string QuestionChoicesByIds => "SELECT * FROM [QuestionChoices] (NOLOCK) WHERE [QuestionId] = @QuestionId AND [ChoiceId] = @ChoiceId";

    public static string AddQuestionChoices =>"INSERT INTO [QuestionChoices] ([QuestionId], [ChoiceId]) VALUES (@QuestionId, @ChoiceId)";

    public static string UpdateQuestionChoices =>
        @"UPDATE [QuestionChoices] 
        SET [RoleName] = @RoleName
        WHERE [Id] = @Id";

    public static string DeleteQuestionChoices => "DELETE FROM [QuestionChoices] WHERE [QuestionId] = @QuestionId AND [ChoiceId] = @ChoiceId";
}
