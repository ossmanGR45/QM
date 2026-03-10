using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QM.Models.DTO
{
    public class CauseDto
    {
        public int? Id { get; set; }

        public string? Description { get; set; } = null;
        public bool? Custom { get; set; } = null;
    }
}
