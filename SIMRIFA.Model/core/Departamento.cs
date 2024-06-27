using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.core
{
    [Table("DEPARTAMENTO", Schema = "core")]
    public class Departamento: Auditoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_DEPARTAMENTO { get; set; }

        [Required]
        public string COD_DEPARTAMENTO { get; set; }

        [Required]
        public string NOMBRE_DEPARTAMENTO { get; set; }
    }
}
