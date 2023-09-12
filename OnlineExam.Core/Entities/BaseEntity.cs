namespace OnlineExam.Core.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
}
