using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    [Table("Choices")]
    public class Choice : EntityIndentityBase<long>
    {
        [Required]
        [MaxLength(1000)]
        public string Option { get; set; }

        public bool IsAnswer { get; set; }

        public Guid QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}
