using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Windows.Input;

namespace TopDownShooterComm
{

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
