using Lara.Repository;
using Lara.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Service
{
    public class FormService
    {
        public List<FormModel> GetFormList()
        {
            var formList = new List<FormModel>();
            using (LARA_Entities entities = new LARA_Entities())
            {
                formList = entities.Forms.Select(f => new FormModel { Id = f.Id, Name = f.Name, Description = f.Description, CreatedAt = f.CreatedAt, CreatedBy = f.CreatedBy }).ToList();

                if (formList == null)
                {
                    return new List<FormModel>(); 
                }
            }

            return formList;
        }
    }
}
