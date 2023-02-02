using Assignment_Form.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Assignment_Form.MyValidation
{
    public class UsernameExistValidationAttribute : ValidationAttribute, IClientValidatable
    {
        public MyRestaurantdatabseContext dbobj = new MyRestaurantdatabseContext();
        public override bool IsValid(Object value)
        {
            try
            {
                var access = dbobj.User.Where(u => u.Username == value.ToString()).FirstOrDefault();
                if (access != null)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule Rule = new ModelClientValidationRule();
            Rule.ErrorMessage = base.ErrorMessage;
            Rule.ValidationType = "exist";
            return new ModelClientValidationRule[] { Rule };
        }
    }
}