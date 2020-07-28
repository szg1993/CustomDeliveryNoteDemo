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
        public void CheckProps()
        {
            string ex = null;

            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(RequiredAttribute)))
                {
                    DescriptionAttribute descAttr = (DescriptionAttribute)Attribute.GetCustomAttributes(prop)
                        .FirstOrDefault(x => x is DescriptionAttribute);

                    var propValue = prop.GetValue(this, null);

                    if (prop.PropertyType == typeof(string) && String.IsNullOrEmpty((string)propValue))
                    {
                        ex = "The following field cannot be empty: " + descAttr.Description + ".";
                    }
                    else if (prop.PropertyType == typeof(double?) && ((double?)propValue == null || (double)propValue <= 0))
                    {
                        ex = "The following field must contains a positive number: " + descAttr.Description + ".";
                    }
                    else if (prop.PropertyType == typeof(DateTime?) && ((DateTime?)propValue == null || ((DateTime)propValue).Date < DateTime.Now.Date))
                    {
                        ex = "Please select a future date in the following field: " + descAttr.Description + ".";
                    }
                }

                if (!String.IsNullOrEmpty(ex))
                {
                    throw new MessageException(ex);
                }
            }
        }

        #endregion
    }
}
