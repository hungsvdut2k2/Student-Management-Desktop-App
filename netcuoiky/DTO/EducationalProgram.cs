using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class EducationalProgram
    {
        [Key]
        public string EducationalProgramId { get; set; }
        public string EducationalProgramName { get; set; }
    }
}
