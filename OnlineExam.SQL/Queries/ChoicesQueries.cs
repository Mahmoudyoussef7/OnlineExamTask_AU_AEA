using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.SQL.Queries;

public static class ChoicesQueries
{
    public static string AllChoices => "SELECT * FROM [Choices] (NOLOCK)";

    public static string ChoiceById => "SELECT * FROM [Choices] (NOLOCK) WHERE [Id] = @Id";
    public static string ChoiceByQuestionId => "SELECT * FROM [Choices] (NOLOCK) WHERE [QuestionId] = @Id";

    public static string AddChoice =>
        @"INSERT INTO [Choices] ([Id],[QuestionId], [ChoiceText], [IsCorrect]) 
            VALUES (@Id, @QuestionId, @ChoiceText, @IsCorrect)";

    public static string UpdateChoice =>
        @"UPDATE [Choices] 
        SET [QuestionId] = @QuestionId, 
            [ChoiceText] = @ChoiceText, 
            [IsCorrect] = @IsCorrect
        WHERE [Id] = @Id";

    public static string DeleteChoice => "DELETE FROM [Choices] WHERE [Id] = @Id";
}
