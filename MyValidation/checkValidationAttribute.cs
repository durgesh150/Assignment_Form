using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Assignment_Form.MyValidation
{
    public class checkValidationAttribute:ValidationAttribute,IClientValidatable
    {
        public override bool IsValid(object value)
        {
            try
            {
                bool flag = (bool)value;
                return flag;
            }
            catch
            {
                return false;
            }

        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule myRule = new ModelClientValidationRule();
            myRule.ErrorMessage = base.ErrorMessage;
            myRule.ValidationType = "chckreq";
            return new ModelClientValidationRule[] { myRule };
        }
    }
}