using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    public class Stock
    {      
        public int Cantidad { get; set; }

        [Key]
        public string Lote { get; set; }

        [Column(TypeName ="Date")]
        public DateTime Fecha { get; set; }

        public int ArticuloId { get; set; }
  
        public int AlmacenId { get; set; }

    }
}
