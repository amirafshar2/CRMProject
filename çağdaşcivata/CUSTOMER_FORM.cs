﻿using BE;
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
    public partial class CUSTOMER_FORM : Form
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
        public CUSTOMER_FORM()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        private void CUSTOMER_FORM_Load(object sender, EventArgs e)
        {
            datagridrefresh();
            datagridviewsetting(dataGridView1);
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in ubll.Readusername())
            {
                names.Add(item.ToString());
            }
            textBox5.AutoCompleteCustomSource = names;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label13.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            richTextBox1.Enabled= false;
            textBox6.Enabled = false;
            button2.Enabled = false;
            textBox6.Enabled = false;
            richTextBox1.Enabled = false;
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            userlogin = w.userlogin;
            label6.Text = BLL.CustomerPics();
           
        }
        #region copy
        CUSTOMER_BLL BLL = new CUSTOMER_BLL();
        USER User = new USER();
        USER userlogin = new USER();
        USER_BLL ubll = new USER_BLL();
        MESSAGE_BOX message = new MESSAGE_BOX();
        MESSAGE_BOX mmessagebox = new MESSAGE_BOX();
        #endregion
        int uid;
        int id;
        #region method
        void datagridrefresh()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = BLL.readall();
            dataGridView1.Columns["İd"].Visible = false;

        }
        void cleantextbox()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Enabled = true;
            textBox5.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            textBox6.Text= string.Empty;
            textBox1.Focus();
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
        #region EVENT
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != string.Empty || textBox1.Text != string.Empty || textBox2.Text != string.Empty)
            {
                CUSTOMER c = new CUSTOMER();
                c.Adress = richTextBox1.Text;
                c.vergidairesi_bilgileri = textBox6.Text;
                c.Name = textBox1.Text;
                c.Company = textBox2.Text;
                c.Phone = textBox3.Text;
                c.Email = textBox4.Text;
                c.Regdate = DateTime.Now;
                if (button2.Text == "Kayıt")
                {
                    if (ubll.Access(userlogin, "Müşteri Bölümü", can.Create))
                    {
                        label10.Text = "* " + (BLL.create(c, User));
                        timer1.Start();
                        cleantextbox();
                        textBox1.Enabled = false;
                        textBox2.Enabled = false;
                        textBox3.Enabled = false;
                        textBox4.Enabled = false;
                        richTextBox1.Enabled = false;
                        textBox6.Enabled = false;
                        button2.Enabled = false;
                        textBox6.Enabled = false;
                        richTextBox1.Enabled = false;
                        datagridrefresh();
                    }
                    else
                    {
                        message.Show("Uyarı", "Buna Yetkiniz Yok .", "Yönetici ile Görüşün.", Button.ok, Logo.warning);
                    }

                }
                else if (button2.Text == "Düzenle")
                {
                    if (ubll.Access(userlogin, "Müşteri Bölümü", can.Update))
                    {
                        label10.Text = "* " + (BLL.Update(c, id, User));
                        timer1.Start();
                        button2.Text = "Kayıt";
                        cleantextbox();
                        textBox1.Enabled = false;
                        textBox2.Enabled = false;
                        textBox3.Enabled = false;
                        textBox4.Enabled = false;
                        richTextBox1.Enabled = false;
                        textBox6.Enabled = false;
                        button2.Enabled = false;
                        textBox6.Enabled = false;
                        richTextBox1.Enabled = false;
                        datagridrefresh();
                    }
                    else
                    {
                        message.Show("Uyarı", "Buna Yetkiniz Yok .", "Yönetici ile Görüşün.", Button.ok, Logo.warning);
                    }

                }
              
            }
            else
            {
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
                timer1.Start();
                message.Show("Uyarı", "(*) işareti bulunan yerler boş bırakılmaz ", "", Button.ok, Logo.info);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = BLL.Search(textBox8.Text);
            dataGridView1.Columns["id"].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                object value = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["İd"].Value;
                if (value != DBNull.Value)
                {
                    id = Convert.ToInt32(value);
                }
            }
            catch (Exception r)
            {

                //message.Show("Hata", "Doğru Seçeneği seçtiğinizden emin olun !", r.Message, Button.ok, Logo.warning);
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(userlogin, "Müşteri Bölümü", can.Update))
            {
                if (id != 0)
                {
                    uid = BLL.GetUserIdformcustumer(id);
                    User = ubll.Read_By_İd(uid);
                    CUSTOMER c = BLL.Readbyid(id);
                    richTextBox1.Text = c.Adress;
                    textBox6.Text = c.vergidairesi_bilgileri;
                    textBox5.Text = User.UserName;
                    textBox1.Text = c.Name;
                    textBox2.Text = c.Company;
                    textBox3.Text = c.Phone;
                    textBox4.Text = c.Email;
                    button2.Text = "Düzenle";
                }
            }
            else
            {
                message.Show("Uyarı", "Buna Yetkiniz Yok .", "Yönetici ile Görüşün.", Button.ok, Logo.warning);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label13.Visible = false;
            label12.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            label10.Text = "";
            timer1.Stop();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(userlogin, "Müşteri Bölümü", can.Delete))
            {
                DialogResult i = mmessagebox.Show("Sil", "Silinsin mı ? ", "Are you sure ?", Button.yesorno, Logo.warning);
                if (i == DialogResult.Yes)
                {
                    if (id != 0)
                    {
                        label10.Text = "* " + BLL.Delet(id);
                        timer1.Start();
                        datagridrefresh();
                    }
                }
            }
            else
            {
                message.Show("Uyarı", "Buna Yetkiniz Yok .", "Yönetici ile Görüşün.", Button.ok, Logo.warning);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox5.Text != string.Empty)
            {
                User = ubll.Readuser(textBox5.Text);
                if (User != null)
                {
                    textBox5.Enabled = false;
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    button2.Enabled = true;
                    textBox6.Enabled = true;
                    richTextBox1.Enabled = true;
                }
                else
                {
                    message.Show("Uyarı", "Müşteri temsilcisi Kayıtlı değil", "", Button.ok, Logo.warning);
                }

               
                
               

            }
            else
            {
                message.Show("Uyarı", "Müşteri temsilcinizi seçtiğinizden emin olun", "", Button.ok, Logo.info);
            }
        }
        #endregion

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
