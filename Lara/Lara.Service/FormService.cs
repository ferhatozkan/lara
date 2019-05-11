using Lara.Repository;
using Lara.Service.Enums;
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

                foreach (var formField in formModel.FormFields)
                {
                    if (string.IsNullOrEmpty(formField.Name) || formField.DataType < 1)
                    {
                        return false;
                    }
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

                    foreach (var formField in formModel.FormFields)
                    {
                        var newFormField = new FormField()
                        {
                            FormId = newForm.Id,
                            Required = formField.IsRequired,
                            Name = formField.Name,
                            DataType = ((DataTypeEnum)formField.DataType).ToString()
                        };
                        entities.FormFields.Add(newFormField);
                        entities.SaveChanges();
                    }


                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public FormModel GetForm(int formId)
        {
            var formModel = new FormModel { FormFields = new List<FormFieldModel>() };
            using (LARA_Entities entities = new LARA_Entities())
            {
                var formEntity = entities.Forms.FirstOrDefault(f => f.Id == formId);
                if (formEntity == null)
                {
                    return formModel;
                }
                var formFieldsEntity = entities.FormFields.Where(ff => ff.FormId == formId).ToList();
                var userProfileEntity = entities.UserProfiles.FirstOrDefault(u => u.UserId == formEntity.CreatedBy);
                if (userProfileEntity == null)
                {
                    return formModel;
                }
                formModel = new FormModel
                {
                    Name = formEntity.Name,
                    Description = formEntity.Description,
                    CreatedAt = formEntity.CreatedAt,
                    CreatorUserName = userProfileEntity.UserName,
                    FormFields = formFieldsEntity.Select(ff => new FormFieldModel { IsRequired = ff.Required, Name = ff.Name, DataType = (int)((DataTypeEnum)Enum.Parse(typeof(DataTypeEnum), ff.DataType)) }).ToList()
                };
            }
            return formModel;
        }
    }
}
