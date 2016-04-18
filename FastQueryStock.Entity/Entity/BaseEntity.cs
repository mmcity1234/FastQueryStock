using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Entity.Entity
{
    public class BaseEntity
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public string Id { get; set; }

   

    }
}
