namespace OnlineExam.SQL.Queries;
public static class QuestionTypesQueries
{
    public static string AllQuestionTypes => "SELECT * FROM [QuestionTypes] (NOLOCK)";

    public static string QuestionTypeById => "SELECT * FROM [QuestionTypes] (NOLOCK) WHERE [Id] = @Id";

    public static string AddQuestionType =>
    @"INSERT INTO [QuestionType] ([Id], [TypeName]) 
            VALUES (@Id, @TypeName)";

    public static string UpdateQuestionType =>
        @"UPDATE [QuestionType] 
        SET [TypeName] = @TypeName
        WHERE [Id] = @Id";

    public static string DeleteQuestionType => "DELETE FROM [QuestionType] WHERE [Id] = @Id";
}
