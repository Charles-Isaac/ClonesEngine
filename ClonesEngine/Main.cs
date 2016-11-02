using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClonesEngine;

namespace ClonesEngine
{
    public partial class Main : Form
    {
        GestionnaireDePacket GP;
        public Main()
        {
            InitializeComponent();
            GP = new GestionnaireDePacket();
            this.DoubleBuffered = true;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            GP.PlayerList[GP.ID].Position = this.PointToClient(Cursor.Position);
            GP.Send(TramePreGen.InfoJoueur(GP.PlayerList[GP.ID], GP.ID, GP.PacketID));
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 1; i <= GP.PlayerCount; i++)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(GP.PlayerList[i].Couleur)), GP.PlayerList[i].Position.X - 10, GP.PlayerList[i].Position.Y - 10, 20, 20);
            }
            e.Graphics.DrawString(GP.ID.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), 10, 10);
            this.Invalidate();
        }
    }
}
