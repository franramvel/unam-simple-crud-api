using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DB;
[Index(nameof(Email), IsUnique = true)]
public partial class Empleado
{
    [Required]
    public int Id { get; set; }
    [StringLength(200, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 10)]
    public required string Direccion { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    [Required]
    [DataType(DataType.PhoneNumber)]
    public required string Telefono { get; set; }
}
