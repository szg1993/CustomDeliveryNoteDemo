using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using ViewModel.Excep;

namespace ViewModel.ModelViewModel
{
    public class ModelViewModelBase : ViewModelBase
    {
        #region Ctors

        public ModelViewModelBase()
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Throw exception if a required property doesn't have proper value.
        /// </summary>
        public void CheckErros()
        {
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(RequiredAttribute)))
                {
                    DescriptionAttribute descAttr = (DescriptionAttribute)Attribute.GetCustomAttributes(prop)
                        .FirstOrDefault(x => x is DescriptionAttribute);

                    var propValue = prop.GetValue(this, null);

                    if (prop.PropertyType == typeof(string) && String.IsNullOrEmpty((string)propValue))
                    {
                        throw new MessageException("The following field cannot be empty: " + descAttr.Description + ".");
                    }
                    else if (prop.PropertyType == typeof(decimal) && (decimal)propValue <= 0)
                    {
                        throw new MessageException("The following field must contains a positive number: " + descAttr.Description + ".");
                    }
                    else if (prop.PropertyType == typeof(DateTime?))
                    {
                        if ((DateTime?)propValue == null)
                        {
                            throw new MessageException("Please select a date in the following field: " + descAttr.Description + ".");
                        }
                        else if (((DateTime)propValue).Date < DateTime.Now.Date)
                        {
                            throw new MessageException("The following date cannot be in the past: " + descAttr.Description + ".");
                        }
                    }
                }
            }
        }

        #endregion

        #region Static

        /// <summary>
        /// Check if the given decimal value is correct or not.
        /// </summary>
        /// <param name="nbr"></param>
        /// <returns></returns>
        public static bool IsValidDecimal(decimal? nbr)
        {
            if (nbr == null || nbr <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
