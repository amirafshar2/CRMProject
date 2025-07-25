using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace çağdaşcivata
{
    public partial class ACTİVİTY_FORM : Form
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
        public ACTİVİTY_FORM()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void ACTİVİTY_FORM_Load(object sender, EventArgs e)
        {
            #region autocomlete
            AutoCompleteStringCollection cnames = new AutoCompleteStringCollection();
            foreach (var item in cbll.Readname())
            {
                cnames.Add(item);

            }
            textBox1.AutoCompleteCustomSource = cnames;
            AutoCompleteStringCollection unames = new AutoCompleteStringCollection();
            foreach (var item in ubll.Readusername())
            {
                unames.Add(item);
            }
            textBox2.AutoCompleteCustomSource = unames;
            AutoCompleteStringCollection aknames = new AutoCompleteStringCollection();
            foreach (var item in acbll.Readkatrgoryname())
            {
                aknames.Add(item);
            }
            textBox3.AutoCompleteCustomSource = aknames;
            #endregion
            datagrid_refresh();
            datagridviewsetting(dataGridView1);
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            userlogin = w.userlogin;
            false_controls();
            label6.Text = bll.Activitypics();
           
        }

        #region copy
        USER userlogin = new USER();
        CUSTOMER c = new CUSTOMER();
        USER u = new USER();
        ACTİVİTY_CATEGORY ac = new ACTİVİTY_CATEGORY();
        ACTİVİTY_CATEGORY_BLL acbll = new ACTİVİTY_CATEGORY_BLL();
        ACTİVİTY_BLL bll = new ACTİVİTY_BLL();
        REMİNDER_BLL rbll = new REMİNDER_BLL();
        USER_BLL ubll = new USER_BLL();
        CUSTOMER_BLL cbll = new CUSTOMER_BLL();
        MESSAGE_BOX message = new MESSAGE_BOX();
        #endregion
        int id;
        int uid;
        #region method
        void false_controls()
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            radioButton1.Enabled = false;
            dateTimePicker1.Enabled = false;
            richTextBox1.Enabled = false;

        }
        void text_take(ACTİVİTY a)
        {
            a.Title = textBox4.Text;
            a.İnfo = richTextBox1.Text;
            a.RegDate = DateTime.Now;

        }
        void text_clear()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            radioButton1.Checked = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox1.Focus();
        }
        void datagrid_refresh()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bll.Read_All();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Expr1"].Visible = false;
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
        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ubll.Access (userlogin, "Aktivite Bölümü ", can.Create ))
            {
                if (richTextBox1.Text != null && dateTimePicker1.Value > DateTime.Now)
                {
                    ACTİVİTY a = new ACTİVİTY();
                    text_take(a);


                    message.Show("Bilgi", bll.Create(a, u, c, ac), "", Button.ok, Logo.info);


                    if (radioButton1.Checked)
                    {
                        REMİNDER r = new REMİNDER();
                        r.Title = textBox4.Text;
                        r.Reminderİnfo = richTextBox1.Text;
                        r.ReminDate = dateTimePicker1.Value.Date;
                        r.RegDate = DateTime.Now;
                        label16.Text = rbll.Create(r, u);
                        timer1.Start();
                    }
                    text_clear();
                    datagrid_refresh();
                    false_controls();
                }
                else
                {
                    message.Show("Uyarı ", "Bilgileri tam doldurun ", " hatırlatıcı tarihini doğru seçtiğinizden emin olun", Button.ok, Logo.warning);
                }
                
            }
            else
            {
                message.Show("Uyarı ", "Buna Yetkiniz Yok . ", "Yönetici ile Görüşün.", Button.ok, Logo.warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != string.Empty)
            {
                c = cbll.Readname(textBox1.Text);
                if (c != null)
                {
                    textBox1.Enabled = false;

                    textBox2.Enabled = true;
                    button2.Enabled = true;
                }
                else
                {
                    message.Show("Uyarı","Müşteri temsilcisi bulunmadı ","",Button.ok, Logo.warning);
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                u = ubll.Readuser(textBox2.Text);
                if (u != null)
                {
                    textBox2.Enabled = false;

                    textBox3.Enabled = true;
                    button4.Enabled = true;
                }
                else
                {
                    message.Show("Uyarı", "Kullanıcı bulunmadı ", "", Button.ok, Logo.warning);
                }


            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(userlogin, "Aktivite Bölümü ", can.Delete))
            {
                DialogResult i = message.Show("Sil", "Silinsin mı ?", "", Button.yesorno, Logo.warning);
                if (i == DialogResult.Yes)
                {
                    bll.Delete(id);
                }
                datagrid_refresh();
            }
            else
            {
                message.Show("Uyarı ", "Buna Yetkiniz Yok . ", "Yönetici ile Görüşün.", Button.ok, Logo.warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
           Object Value = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id"].Value);
            if (Value != DBNull.Value)
            {
                id = Convert . ToInt32(Value);
            }
            Object Value1 = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["GörevliKodu"].Value);
            if (Value1 != DBNull.Value)
            {
                uid = Convert.ToInt32(Value1);
            }

           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != null)
            {
                ac = acbll.Readaccatagory(textBox3.Text);
                if (ac!= null)
                {
                    textBox3.Enabled = false;

                    textBox4.Enabled = true;
                    button3.Enabled = true;
                    radioButton1.Enabled = true;
                    dateTimePicker1.Enabled = true;
                    richTextBox1.Enabled = true;
                }
                else
                {
                    message.Show("Uyarı", "Katagory bulunmadı ", "Ayarlar Bölümünden Kategory eklenebilir \n eğer Yetkiniz yoksa Admin ile görüşün", Button.ok, Logo.warning);
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label16.Text = string.Empty;
            timer1.Start();
        }

        private void detaylarıGörToolStripMenuItem_Click(object sender, EventArgs e)
        {
            USER u = ubll.Read_By_İd(uid);
            ACTİVİTY a = bll.Read_byid(id);
            if (a.İnfo != null)
            {
                message.Show("Detaylar", "Açıklama :" + a.İnfo, " \n Görevli : " + u.Name, Button.ok, Logo.info);
            }
            else
            {
                message.Show("Detaylar", "Açıklama :" + "Açıklama Eklenmemiştir . ", " \n Görevli : " + u.Name, Button.ok, Logo.info);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bll.Search(textBox8.Text);
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["GörevliKodu"].Visible = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
