namespace OnlineExam.UI;

public static class QuestionTypesQueries
{
    public static string AllQuestionTypes => "SELECT * FROM [QuestionTypes] (NOLOCK)";

    public static string QuestionTypeById => "SELECT * FROM [QuestionTypes] (NOLOCK) WHERE [Id] = @Id";
}
