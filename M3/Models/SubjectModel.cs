using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace M3.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        [Display(Name = "Subject:")]
        public string Subject { get; set; }
    }
}
