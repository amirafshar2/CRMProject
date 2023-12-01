using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace çağdaşcivata
{
    public partial class SETİNG_FORM : Form
    {
        ACTİVİTY_CATEGORY_BLL bll = new ACTİVİTY_CATEGORY_BLL();
        MESSAGE_BOX message = new MESSAGE_BOX();
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
        public SETİNG_FORM()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            datagridviewsetting(dataGridView1);
        }
        int id;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            object value = (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id"].Value);
            if (value != DBNull.Value)
            {
                id = Convert.ToInt32(value);
            }
         

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ACTİVİTY_CATEGORY a = new ACTİVİTY_CATEGORY();
            a.CategoryName = textBox1.Text;
            label1.Text = bll.Create(a);
            timer1.Start();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bll.Read_All();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["DeletStatus"].Visible = false;
            textBox1.Text = string.Empty;
            textBox1.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = string.Empty;
            timer1.Stop();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult i = message.Show("Sil", "Silinsin mı?", "", Button.yesorno, Logo.warning);

            if (i == DialogResult.Yes)
            {
                if (id != 0)
                {
                    bll.Delete(id);
                }
                else { label1.Text = "Oğei seçtiğinizden emin olun"; }
                timer1.Start();
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bll.Read_All();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["DeletStatus"].Visible = false;

        }
        public void datagridviewsetting(DataGridView d)
        {
            d.DefaultCellStyle.BackColor = Color.FromArgb(108, 117, 125);
            d.RowHeadersVisible = false;
            d.BackgroundColor = Color.FromArgb(108, 117, 125);
            d.BorderStyle = BorderStyle.None;
            d.DefaultCellStyle.SelectionForeColor = Color.White;
            d.DefaultCellStyle.SelectionBackColor = Color.FromArgb(73, 80, 87);
            d.EnableHeadersVisualStyles = false;
            d.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            d.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 117, 125);
            d.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SETİNG_FORM_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bll.Read_All();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["DeletStatus"].Visible = false;

        }
    }
}
