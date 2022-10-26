using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal interface IWindowsFormAppListener
    {

        void _HandleEvents(object sender, EventArgs e);

        void windowsFormAppControl();

    }
}
