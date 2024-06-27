using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.core
{
    [Table("MUNICIPIO", Schema = "core")]
    public class Municipio : Auditoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_MUNICIPIO { get; set; }

        [Required]
        public string COD_MUNICIPIO { get; set; }

        [Required]
        public string NOMBRE_MUNICIPIO { get; set; }

        [Required]
        public string COD_DEPARTAMENTO { get; set; }

        public virtual Departamento Departamento { get; set; }

    }
}
