using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Singleton
{
    public class LoggedUser
    {
        public long Id { get; set; }
        public string Name  { get; set; }

        private static LoggedUser employee;
        public static LoggedUser Employee
        {
            get
            {
                if (employee == null)
                {
                    employee = new LoggedUser();
                }

                return employee;
            }
        }

        private LoggedUser()
        {

        }
    }
}
