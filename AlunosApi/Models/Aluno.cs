using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Models
{
    [Table("Alunos")]
    public class Aluno
    {
        [Key]
        public int AlunoId { get; set; }
        [Required]
        [StringLength(80)]
        public string AlunoNome { get; set; }
        [EmailAddress]
        [StringLength(100)]
        public string AlunoEmail { get; set; }
        [Required]
        public int AlunoIdade { get; set; }

    }
}
