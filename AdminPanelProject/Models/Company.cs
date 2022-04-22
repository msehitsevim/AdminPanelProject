using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models;

public class Company
{
    [Key]
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string Description { get; set; }

}

