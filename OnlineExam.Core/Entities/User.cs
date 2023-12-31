﻿using System.ComponentModel.DataAnnotations;

namespace OnlineExam.Core.Entities;

public class User : BaseEntity
{
    [MaxLength(100)]
    public string UserName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; }
}
