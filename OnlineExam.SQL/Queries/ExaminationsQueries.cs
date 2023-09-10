using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.SQL.Queries;

public static class ExaminationsQueries
{
    public static string AllExaminations => "SELECT * FROM [Examinations] (NOLOCK)";

    public static string ExaminationById => "SELECT * FROM [Examinations] (NOLOCK) WHERE [Id] = @Id";

    public static string AddExamination =>
        @"INSERT INTO [Examinations] ([Id], [ExamTitle], [ExamDescription], [StartDate],[EndDate],[DurationInHours]) 
            VALUES (@Id, @ExamTitle, @ExamDescription, @StartDate, @EndDate, @DurationInHours)";

    public static string UpdateExamination =>
        @"UPDATE [Examinations] 
        SET [ExamTitle] = @ExamTitle, 
            [ExamDescription] = @ExamDescription, 
            [StartDate] = @StartDate,
            [EndDate] = @EndDate,
            [DurationInHours] = @DurationInHours
        WHERE [Id] = @Id";

    public static string DeleteExamination => "DELETE FROM [Examinations] WHERE [Id] = @Id";
}
