using BE;
using BLL;
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

namespace çağdaşcivata
{
    public partial class REMİNDER_FORM : Form
    {
        #region form drawing

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
        #endregion
        public REMİNDER_FORM()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        private void REMİNDER_FORM_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in ubll.Readusername())
            {
                names.Add(item);
            }
            textBox1.AutoCompleteCustomSource = names;
            datagrid_refresh();
            datagridviewsetting(dataGridView1);
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            userlogin = w.userlogin;
            textBox2.Enabled = false;
            richTextBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            button3.Enabled = false;
            label6.Text = BLL.TotalReminders();
        }
        #region copy
        USER userlogin = new USER();
        USER_BLL ubll = new USER_BLL();
        MESSAGE_BOX messageb = new MESSAGE_BOX();
        REMİNDER_BLL BLL = new REMİNDER_BLL();
        CUSTOMER c = new CUSTOMER();
        USER u = new USER();
        #endregion
        #region method 
        void datagrid_refresh()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = BLL.Read_all();
            dataGridView1.Columns["id"].Visible = false;

        }
        public void datagridviewsetting(DataGridView d)
        {
            d.DefaultCellStyle.BackColor = Color.FromArgb(108, 117, 125);
            d.RowHeadersVisible = false;
            d.BorderStyle = BorderStyle.None;
            d.BackgroundColor = Color.FromArgb(108, 117, 125);
            d.DefaultCellStyle.SelectionForeColor = Color.White;
            d.DefaultCellStyle.SelectionBackColor = Color.FromArgb(73, 80, 87);
            d.EnableHeadersVisualStyles = false;
            d.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            d.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 117, 125);
            d.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void Text_clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            textBox1.Enabled = true;
            textBox1.Focus();
        }
        void text_take(REMİNDER r)
        {
            r.Title = textBox2.Text;
            r.Reminderİnfo = richTextBox1.Text;
            r.RegDate = DateTime.Now;
            r.ReminDate = dateTimePicker1.Value.Date;
        }
        #endregion
        int id;
        string Username;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null && richTextBox1.Text != null)
            {
                REMİNDER r = new REMİNDER();
                text_take(r);
                if (button3.Text == "Kayıt et")
                {
                    if (ubll.Access(userlogin, "Htırlatıcı Bölümü", can.Create))
                    {
                        messageb.Show("Bilgi", BLL.Create(r, u), "", Button.ok, Logo.info);
                        datagrid_refresh();
                        Text_clear();
                    }
                    else
                    {
                        messageb.Show("Uyarı", "Buna yetkiniz yok .", "Yönetici ile Görüşün .", Button.ok, Logo.info);
                    }
                }
                else
                {
                    if (ubll.Access(userlogin, "Htırlatıcı Bölümü", can.Update))
                    {
                        messageb.Show("Bilgi", BLL.Update(r, id, u ), "", Button.ok, Logo.info);
                        datagrid_refresh();
                        Text_clear();
                    }
                    else
                    {
                        messageb.Show("Uyarı", "Buna yetkiniz yok .", "Yönetici ile Görüşün .", Button.ok, Logo.info);
                    }
                }
            }
            else
            {
                messageb.Show("Uyarı", "Bilgileri Doldurduğunuzdan emin olun .", "", Button.ok, Logo.info);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            u = ubll.Readuser(textBox1.Text);
            if (u != null)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = true;
                richTextBox1.Enabled = true;
                dateTimePicker1.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            Object value = (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id"].Value);
            if (value != DBNull.Value)
            {
                id = Convert.ToInt32(value);
            }

            object value1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Görevli"].Value;
            if (value1 != DBNull.Value)
            {
                Username = value1.ToString();
            }

        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(userlogin, "Htırlatıcı Bölümü", can.Update))
            {
                if (id != 0)
                {
                    USER user = ubll.Readuser(Username);
                    REMİNDER r = BLL.Readbyid(id);

                    if (user != null)
                    {
                        textBox1.Text = user.UserName;
                    }
                    else
                    {
                        textBox1.Text = "Kullanıcı Bulunamadı";
                    }

                    textBox2.Text = r.Title;
                    richTextBox1.Text = r.Reminderİnfo;
                    dateTimePicker1.Value = r.ReminDate;
                    button3.Text = "Düzenle";
                }
                else
                {
                    messageb.Show("Bilgi", "Hatırlatıcıyı Seçtiğinizden emin olun", "Tekrar deneyin", Button.ok, Logo.info);
                }
            }
            else
            {
                messageb.Show("Uyarı", "Buna yetkiniz yok .", "Yönetici ile Görüşün .", Button.ok, Logo.info);
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ubll.Access(userlogin, "Htırlatıcı Bölümü", can.Delete))
            {

                DialogResult i = messageb.Show("Silinsin mı ?", "Eminmısın?", " ", Button.yesorno, Logo.warning);
                if (i == DialogResult.Yes)
                {
                    messageb.Show("Bilgi", BLL.Delete(id), " ", Button.ok, Logo.info);

                }
                datagrid_refresh();
            }
            else
            {
                messageb.Show("Uyarı", "Buna yetkiniz yok .", "Yönetici ile Görüşün .", Button.ok, Logo.info);
            }
        }

        private void yapıldıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult i = messageb.Show("Yapıldı mı ?", "Hatırlatıcı kapatılsın mı ?", "", Button.yesorno, Logo.info);
            if (i == DialogResult.Yes)
            {
                REMİNDER R = BLL.Readbyid(id);
                BLL.İsDone(R, id);
            }
            datagrid_refresh();
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }
    }
}
