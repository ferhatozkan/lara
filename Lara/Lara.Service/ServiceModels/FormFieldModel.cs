using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Service.ServiceModels
{
    public class FormFieldModel
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string Required { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }

    }
}
