﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace çağdaşcivata
{
    public partial class SMS_FORM : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
       int nLeftRect, // sol üst köşenin x koordinatı
       int nTopRect, // sol üst köşenin y kordinatı
       int nRightRect, // sağ alt köşenin x kordinatı
        int nBottomRect, // sağ alt köşenin y kordinatı
       int nWidthEllipse, // height of ellipse
        int nHeightEllipse // elipsin genişliği
        );
        public SMS_FORM()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
