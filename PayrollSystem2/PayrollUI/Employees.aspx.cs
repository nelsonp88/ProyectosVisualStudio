using PayrollBL;
using PayrollEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PayrollUI
{
    public partial class Employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<IEmployee> employees = new PayrollManager().GetAllEmployees();
        }
    }
}