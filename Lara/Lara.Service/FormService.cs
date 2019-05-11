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

        public bool CreateForm(FormModel formModel)
        {
            try
            {
                if (string.IsNullOrEmpty(formModel.Name) || formModel.Name.Length > 50 || (!string.IsNullOrEmpty(formModel.Description) && formModel.Description.Length > 50))
                {
                    return false;
                }
                using (LARA_Entities entities = new LARA_Entities())
                {
                    var form = entities.Forms.FirstOrDefault(f => f.Name == formModel.Name);

                    if (form != null)
                    {
                        return false;
                    }

                    var newForm = new Form()
                    {
                        Name = formModel.Name,
                        Description = formModel.Description,
                        CreatedAt = DateTime.Now,
                        CreatedBy = formModel.CreatedBy.Value
                    };

                    entities.Forms.Add(newForm);
                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
