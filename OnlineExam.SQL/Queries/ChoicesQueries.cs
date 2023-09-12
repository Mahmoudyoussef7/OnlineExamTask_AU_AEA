namespace OnlineExam.SQL.Queries;

public static class ChoicesQueries
{
    public static string AllChoices => "SELECT * FROM [Choices] (NOLOCK)";

    public static string ChoiceById => "SELECT * FROM [Choices] (NOLOCK) WHERE [Id] = @Id";

    public static string AddChoice =>
        @"INSERT INTO [Choices] ([Id], [ChoiceText]) 
            VALUES (@Id, @ChoiceText)";

    public static string UpdateChoice =>
        @"UPDATE [Choices] 
        SET [ChoiceText] = @ChoiceText
        WHERE [Id] = @Id";

    public static string DeleteChoice => "DELETE FROM [Choices] WHERE [Id] = @Id";
}
