using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;
[Table("TokenCall")]
public class TokenCallModel
{
    public int Id { get; set; }
    [Column("tokenCall")]
    public string TokenCall { get; set; }
    [Column("callerId")]
    public string CallerId { get; set; }
}
