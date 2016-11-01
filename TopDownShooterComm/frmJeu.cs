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

    public partial class frmJeu : Form
    {
        GestionnaireDePacket GP;
        //UDPConnecter ConnectionUDP;
        bool update = true;
        bool mouse = true;
        Random RNG;
        byte ID;
        public frmJeu()
        {
            InitializeComponent();
            ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(Point), true);
            ProtoBuf.Meta.RuntimeTypeModel.Default[typeof(Point)].Add(1, "X").Add(2, "Y");
            ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(PointF), true);
            ProtoBuf.Meta.RuntimeTypeModel.Default[typeof(PointF)].Add(1, "X").Add(2, "Y");
            RNG = new Random();
            ID = (byte)(RNG.Next(255) + 1);
            GP = new GestionnaireDePacket();
            this.DoubleBuffered = true;
        }


        private void frmJeu_Paint(object sender, PaintEventArgs e)
        {
            if (true)
            {
                Invoke(new Action(() =>
                {
                    for (int i = 0; i < GP.PlayerCount; i++)
                    {
                        int X, Y;
                        X = GP.PlayerList[i].Position.X;
                        Y = GP.PlayerList[i].Position.Y;

                        e.Graphics.FillEllipse(new SolidBrush(Color.Black), X - 10, Y - 10, 20, 20);
                        /*rtb1.Text += a.ToString() + "x:" + c.ToString() + "y\n";
                        rtb1.SelectionStart = rtb1.Text.Length;
                        rtb1.ScrollToCaret();*/
                    }
                    if (mouse)
                    {
                        Point Light = this.PointToClient(Cursor.Position);
                        if (GP.PlayerCount > 0)
                        {

                            GP.PlayerList[GP.ID].Position = Light;
                        }


                    }
                    if (update)
                    {
                        this.Invalidate();
                    }
                }));


            }
        }

        private void frmJeu_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void frmJeu_Click(object sender, EventArgs e)
        {
            if (update)
            {
                update = false;
                timer1.Start();
            }
            else
            {
                //update = true;
                //timer1.Stop();
                //this.Refresh();
            }
        }

        private void frmJeu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*if (mouse)
            {
                mouse = false;
            }
            else
            {
                mouse = true;
                this.Refresh();
            }*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                for (int i = 1; i < GP.PlayerCount + 1; i++)////////////////////////////////////+ 1
                {
                    int X = 0, Y = 0;
                    X = GP.PlayerList[i].Position.X;
                    Y = GP.PlayerList[i].Position.Y;


                    Graphics g = this.CreateGraphics();
                    g.FillEllipse(new SolidBrush(Color.Black), X - 10, Y - 10, 20, 20);
                    /*rtb1.Text += a.ToString() + "x:" + c.ToString() + "y\n";
                    rtb1.SelectionStart = rtb1.Text.Length;
                    rtb1.ScrollToCaret();*/

                    if (X == 0 && Y == 0)
                    {
                        this.Refresh();
                    }
                }
                if (MouseButtons == MouseButtons.Left)//mouse)
                {


                    Point Light = this.PointToClient(Cursor.Position);
                    GP.PlayerList[GP.ID].Position = Light;
                    GP.Send(TramePreGen.InfoJoueur(GP.PlayerList[GP.ID], GP.ID, GP.PacketID));




                }

                if (update)
                {
                    this.Invalidate();
                }
            }));
        }

        private void frmJeu_KeyPress(object sender, KeyPressEventArgs e)
        {
            GP.PlayerList[GP.ID].Position = new Point(0, 0);
            GP.Send(TramePreGen.InfoJoueur(GP.PlayerList[GP.ID], GP.ID, GP.PacketID));

            
        }

        private void frmJeu_Load(object sender, EventArgs e)
        {

        }

        private void frmJeu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                byte[] b = new byte[2];
                b[0] = 5;
                b[1] = GP.ID;
                GP.Send(b);
            }
           
        }
    }
}
