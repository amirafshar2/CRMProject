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
using System.Windows;
using System.Windows.Forms;

namespace çağdaşcivata
{
    public partial class PRODUCT_FORM : Form
    {
        #region formdrawing
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
        #region copy
        PRODUCT_BLL bll = new PRODUCT_BLL();
        USER_BLL ubll = new USER_BLL();
        USER userlogin = new USER();
        MESSAGE_BOX message = new MESSAGE_BOX();
        OpenFileDialog oppf = new OpenFileDialog();
        Image pic;
        #endregion
        public PRODUCT_FORM()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void PRODUCT_FORM_Load(object sender, EventArgs e)
        {
            datagreadrefresh(bll.Readall());
            datagridviewsetting(dataGridView1);
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            userlogin = w.userlogin;
            label6.Text = bll.Productpics();
            panel1.AutoScroll = true;
            groupBox2.Visible = false;
            y1.Visible = false;
            y2.Visible = false;
            y3.Visible = false;
            y4.Visible = false;
            y5.Visible = false;
            y6.Visible = false;
        }
        public int id;
        #region method
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
       
        void datagreadrefresh(List<PRODUCT> p)
        {             
            List<PRODUCT> products =  p ;
            int a = 0;
            int s = 0;
            foreach (var item in products)
            {
                if (s<=20)
                {
                    mydatagrid md = new mydatagrid();
                    md.Urunlbl.Text = item.Category;
                    md.idlbl.Text = item.id.ToString();
                    md.urunadılbl.Text = item.Name;
                    md.dınnolbl.Text = item.DINnumber;
                    md.caplbl.Text = item.Cap;
                    md.boylbl.Text = item.Boy;
                    md.kalitelbl.Text = item.Quality;
                    md.fiyatlbl.Text = item.Price.ToString();
                    md.stoklbl.Text = item.Stock.ToString();
                    md.kaplamalbl.Text = item.Kaplama;
                    if (item.picture != "")
                    {
                        md.pictureBox1.Image = Image.FromFile(item.picture);
                    }
                    else
                    {
                        md.pictureBox1.Image = Properties.Resources.Adsız_tasarım__2_1;
                    }
                    panel1.Controls.Add(md);
                    md.Location = new System.Drawing.Point(0, 0 + a);
                    a = a + 90;
                    s = s + 1;
                }
               
            }

        }
        void clearcombobox()
        {
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            comboBox6.Text = string.Empty;
            comboBox7.Text = string.Empty;
            maskedTextBox1.Text = string.Empty;
            comboBox9.Text = string.Empty;
            maskedTextBox2.Text = string.Empty;
            maskedTextBox3.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            pictureBox3.Image = Properties.Resources.Adsız_tasarım__2_1;
            comboBox1.Focus();

        }

        void insertdataproduct(PRODUCT p)
        {
            if (comboBox1.Text != string.Empty || comboBox2.Text != string.Empty || comboBox3.Text != string.Empty || comboBox6.Text != string.Empty || maskedTextBox1.Text != string.Empty || maskedTextBox2.Text != string.Empty)
            {
                p.Category = comboBox1.Text;
                p.Name = comboBox2.Text;
                p.Cap = comboBox3.Text;
                p.Boy = comboBox4.Text;
                p.Quality = comboBox5.Text;
                p.Kaplama = comboBox6.Text;
                p.Price = Convert.ToDouble(maskedTextBox1.Text);
                p.Stock = Convert.ToInt32(maskedTextBox2.Text);
                p.Packing = maskedTextBox3.Text;
                p.BrandName = comboBox7.Text;
                p.Feature = richTextBox1.Text;
                p.DINnumber = comboBox9.Text;
                string pathh = bll.check_pic(p.Category, p.Name, p.Kaplama);
                if (pathh != null)
                {
                    p.picture = pathh;
                }
                else
                {
                    p.picture = Savepic(p.Category, p.Name, p.Kaplama);
                }
            }

            else
            {
                y1.Visible = true;
                y2.Visible = true;
                y3.Visible = true;
                y4.Visible = true;
                y5.Visible = true;
                y6.Visible = true;
                timer1.Start();
                message.Show("Uyarı !!!", "bilgileri doldurduğunuzdan emin olun ", "", Button.ok, Logo.warning);
            }
        }


        void combobox_item_clear()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox9.Items.Clear();
        }
        #region listofcombobox
        List<string> citems = new List<string> { "AKB TD", "AKB YD ", "AKB WHİTWORTH", "FLANŞLI AKB TIRTIKSIZ", "FLANŞLI AKB TIRTIKLI", "İMB", "HB İMB", "BOMB İMB", "RBOMB İMB", "KASA", "KANAL", "STSKUR", "ÖZEL CİVATA" };
        List<string> cCAPitems = new List<string> { "3", "4", "5", "6", "7", "8", "10", "12", "14", "16", "18", "20", "22", "24", "27", "30", "33", "36", "1/2", "1/4", "3/4", "3/8", "5/16", "5/8", "7/16", "7/8", "9/16" };
        List<string> cBoyitems = new List<string> { "6", "8", "10", "12", "15", "16", "18", "20", "25", "30", "35", "40", "45", "50", "55", "60", "65", "70", "75", "85", "90", "95", "100", "105", "110", "115", "120", "125", "130", "135", "140", "145", "150", "160", "170", "180", "190", "200", "210", "220", "230", "240", "250", "300", "1/2", "1/4", "3/4", "1'", "2 1/2", "2'", "3 1/2", "3'", "3/4", "4 1/2", "4'", "5 1/2", "5'", "5/8", "6'" };
        List<string> ckaliteitems = new List<string> { "6,8", "8.8", "10.9", "12.9", "A2", "A4" };
        List<string> cDINNOitems = new List<string> { "DIN931", "DIN933", "DIN6921", "DIN912", "DIN7991", "DIN7380", "DIN603", "DIN916" };

        List<string> vitems = new List<string> { "SUNTA", "YSB METRİK", "YHB METRİK", "RYSB METRİK", "YSB SAC", "YHB SAC", "RYSB SAC", "YMB SAC", "YSB PLS", "YHB PLS", "RYSB PLS", "YSB MU", "YHB MU ", "RYSB MU", "ALÇIPAN SİVRİ", "ALÇIPAN MU", "BULDEX", "RAKB SAC", "TRİFON", "ALYAN ÇEKTİRME", "TRAPEZ", "UÇGEN VİDA", "TORKS", "METRİK TORKS", "BETOFAST", "ÖZEL VİDA SİVRİ", "ÖZEL VİDA METRİK", "İSPANYOLET" };
        List<string> VCAPitems = new List<string> { "2", "2,5", "2,9", "3", "3,5", "3,9", "4", "4,2", "4,5", "4,8", "5", "5,5", "6", "6,3", "6,8", "7,5", "8", "10", "12", "16" };
        List<string> VBoyitems = new List<string> { "6", "6,5", "8", "9,5", "10", "12", "13", "15", "16", "18", "19", "20", "22", "25", "30", "32", "35", "38", "40", "45", "50", "60", "66", "70", "75", "80", "85", "90", "100", "105", "110", "120", "125", "130", "135", "140", "145", "150", "160", "180", "200", "220", "240" };
        List<string> Vkaliteitems = new List<string> { "ISIL İŞLEMLİ", "ISILİŞLEMSIZ", "A2", "A4","10,9","12,9" };
        List<string> VDINNOitems = new List<string> { "DIN 571", "DIN 7504", "DIN 7504K RAKB", "DIN 7504N YSB", "DIN 7504P YHB", "DIN 7505A YHB", "DIN 7976 AKB", "DIN 7981 YSB", "DIN 7982 YHB", "DIN 7985 YSB", "DIN 965 YHB", "DIN 968 RYSB" };

        List<string> sitems = new List<string> { "SOMUN", "FLANŞLI", "FİBERLİ", "KÖR SOMUN", "KARE SOMUN", "KONTRA SOMUN", "PERÇİN SOMUN SK", "PERÇİN SOMUN GK", "PERÇİN SOMUN AK SK", "PERÇİN SOMUN AK GK", "AĞAÇ SOMUN", "KELEBEK SOMUN", "TABLALI SOMUN", "UZATMA SOMUNU", "SOL DİŞ SOMUN", "KAFES SOMUN", "ALTIKÖŞE KAYNAK SOMUN", "DÖRT KÖŞE KAYNAK SOMUN" };
        List<string> SCAPitems = new List<string> { "1", "3", "4", "5", "6", "7", "8", "10", "12", "14", "16", "18", "20", "22", "24", "27", "30", "33", "36", "39", "42", "45", "48", "52", "56", "60", "64", "68", "72", "76", "90", "1/2", "1/4", "3/16", "3/4", "3/8", "5/16", "5/8", "7/16", "9/16" };
        List<string> SBoyitems = new List<string> { "2,40", "2,70", "3,00", "3,20", "4,00", "4,30", "4,50", "4,65", "5,00" };
        List<string> Skaliteitems = new List<string> { "4", "5", "6", "8", "A2", "A4", "Plastik" };
        List<string> SDINNOitems = new List<string> { "DIN 934", "DIN 6923", "DIN 985", "DIN 315", "DIN 557", "DIN 929", "DIN 439-936", "DIN 1587", "DIN 928", "DIN 1624" };

        List<string> ditems = new List<string> { "GÖMLEKLİ DÜBEL", "KLİPSLİ DÜBEL", "BORULU DÜBEL", "S TİPİ DÜBEL", "KANCALI DÜBEL", "ASMA TAVAN DÜBEL", "PLASTİK DÜBEL", "ÇAKMA DÜBEL", "ROKET DÜBEL", "BOŞLUK DÜBEL", "ALÇIPAN DÜBEL", "PARAŞUT DÜBEL" };
        List<string> DCAPitems = new List<string> { "10 mm", "12 mm", "3 mm", "6 mm", "7 mm", "8 mm", "10", "12", "14", "16", "20", "24", "6", "8" };
        List<string> DBoyitems = new List<string> { "45", "55", "65", "70", "75", "85", "90", "100", "110", "120", "140", "145", "150", "180", "200", "220", "250", "260", "300" };
        List<string> Dkaliteitems = new List<string> { };
        List<string> DDINNOitems = new List<string> { };

        List<string> sAaitems = new List<string> { "GİJON" };
        List<string> SACAPitems = new List<string> { "4", "5", "6", "8", "10", "12", "14", "16", "18", "20", "22", "24", "27", "30", "33", "36" };
        List<string> SABoyitems = new List<string> { "1000", "2000", "3000" };
        List<string> SAkaliteitems = new List<string> { "6,8", "8,8", "A2", "A4" };
        List<string> SADINNOitems = new List<string> { "DIN 975", "DIN 976" };

        List<string> pitems = new List<string> { "PUL METRİK", "PUL WHİTWORTH", "KALLIN PUL", "ŞASE PUL", "YAYLI RONDELA", "TIRTILLI PUL" };
        List<string> PCAPitems = new List<string> { "3", "4", "5", "6", "8", "10", "12", "14", "16", "18", "20", "22", "24", "27", "30", "36", "1/2", "1/4", "3/4", "3/8", "5/16", "5/8", "7/16", "7/8", "9/16" };
        List<string> PBoyitems = new List<string> { };
        List<string> Pkaliteitems = new List<string> { "6,8", "8,8", "10", "A2", "A4", "ALÜMİNYUM", "BAKIR", "ÇELİK", "DEMİR" };
        List<string> PDINNOitems = new List<string> { "DIN 125", "DIN 25201", "DIN 6798", "DIN 6798", "DIN 6916", "DIN 9021" };
        #endregion
           //Özel Civata
           //Özel Vida
           //Özel Somun
           //Özel Dübel
           //Özel Saplama
           //Özel Pul

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            if (comboBox1.Text == "Civata" || comboBox1.Text == "Özel Civata")
            {
                combobox_item_clear();
                foreach (var item in citems)
                {
                    comboBox2.Items.Add(item);
                }
                foreach (var item in cCAPitems)
                {
                    comboBox3.Items.Add(item);
                }
                foreach (var item in cBoyitems)
                {
                    comboBox4.Items.Add(item);
                }
                foreach (var item in ckaliteitems)
                {
                    comboBox5.Items.Add(item);
                }
                foreach (var item in cDINNOitems)
                {
                    comboBox9.Items.Add(item);
                }


            }
            else if (comboBox1.Text == "Vida" || comboBox1.Text == "Özel Vida")
            {
                combobox_item_clear();
                foreach (var item in vitems)
                {
                    comboBox2.Items.Add(item);
                }
                foreach (var item in VCAPitems)
                {
                    comboBox3.Items.Add(item);
                }
                foreach (var item in VBoyitems)
                {
                    comboBox4.Items.Add(item);
                }
                foreach (var item in Vkaliteitems)
                {
                    comboBox5.Items.Add(item);
                }
                foreach (var item in VDINNOitems)
                {
                    comboBox9.Items.Add(item);
                }







            }
            else if (comboBox1.Text == "Somun" || comboBox1.Text == "Özel Somun")
            {
                combobox_item_clear();

                foreach (var item in sitems)
                {
                    comboBox2.Items.Add(item);
                }
                foreach (var item in SCAPitems)
                {
                    comboBox3.Items.Add(item);
                }
                foreach (var item in SBoyitems)
                {
                    comboBox4.Items.Add(item);
                }
                foreach (var item in Skaliteitems)
                {
                    comboBox5.Items.Add(item);
                }
                foreach (var item in SDINNOitems)
                {
                    comboBox9.Items.Add(item);
                }






            }
            else if (comboBox1.Text == "Dübel" || comboBox1.Text == "Özel Dübel")
            {
                combobox_item_clear();

                foreach (var item in ditems)
                {
                    comboBox2.Items.Add(item);
                }
                foreach (var item in DCAPitems)
                {
                    comboBox3.Items.Add(item);
                }
                foreach (var item in DBoyitems)
                {
                    comboBox4.Items.Add(item);
                }
                foreach (var item in Dkaliteitems)
                {
                    comboBox5.Items.Add(item);
                }
                foreach (var item in DDINNOitems)
                {
                    comboBox9.Items.Add(item);
                }





            }
            else if (comboBox1.Text == "Saplama" || comboBox1.Text == "Özel Saplama")
            {
                combobox_item_clear();
                foreach (var item in sAaitems)
                {
                    comboBox2.Items.Add(item);
                }
                foreach (var item in SACAPitems)
                {
                    comboBox3.Items.Add(item);
                }
                foreach (var item in SABoyitems)
                {
                    comboBox4.Items.Add(item);
                }
                foreach (var item in SAkaliteitems)
                {
                    comboBox5.Items.Add(item);
                }
                foreach (var item in SADINNOitems)
                {
                    comboBox9.Items.Add(item);
                }


            }
            else if (comboBox1.Text == "Pul" || comboBox1.Text == "Özel Pul")
            {
                combobox_item_clear();
                foreach (var item in pitems)
                {
                    comboBox2.Items.Add(item);
                }
                foreach (var item in PCAPitems)
                {
                    comboBox3.Items.Add(item);
                }
                foreach (var item in PBoyitems)
                {
                    comboBox4.Items.Add(item);
                }
                foreach (var item in Pkaliteitems)
                {
                    comboBox5.Items.Add(item);
                }
                foreach (var item in PDINNOitems)
                {
                    comboBox9.Items.Add(item);
                }


            }
            else
            {
                combobox_item_clear();

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PRODUCT p = new PRODUCT();
            if (comboBox1.Text != string.Empty || comboBox2.Text != string.Empty || comboBox3.Text != string.Empty || comboBox6.Text != string.Empty || maskedTextBox1.Text != string.Empty || maskedTextBox2.Text != string.Empty)
            {
                insertdataproduct(p);


                if (button2.Text == "Kayıt")
                {
                    if (ubll.Access(userlogin, "Ürün Bölümü", can.Create))
                    {
                        label22.Text = "* " + bll.create(p);
                        timer1.Start();
                        clearcombobox();
                        panel1.Controls.Clear();
                        datagreadrefresh(bll.Readall());
                    }
                    else
                    {
                        message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
                    }
                }
                else
                {
                    if (ubll.Access(userlogin, "Ürün Bölümü", can.Update))
                    {
                        label22.Text = "* " + bll.Update(p, id);
                        button2.Text = "Kayıt";
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;
                        comboBox6.Enabled = true;
                        timer1.Start();
                        clearcombobox();
                        panel1.Controls.Clear();
                        datagreadrefresh(bll.Readall());
                    }
                    else
                    {
                        message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
                    }
                }
            }

            else
            {
                y1.Visible = true;
                y2.Visible = true;
                y3.Visible = true;
                y4.Visible = true;
                y3.Visible = true;
                y4.Visible = true;
                timer1.Start();
                message.Show("Uyarı !!!", "bilgileri doldurduğunuzdan emin olun ", "", Button.ok, Logo.warning);
            }
          
                 
        }
       
        
        string Savepic(string name , string category , string kaplama )
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Productpic\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string picname =category + name + kaplama + ".png";
            string picpath;
            if (oppf.FileName != null)
            {
                picpath = oppf.FileName;
            }
            else
            {
                picpath = Properties.Resources.Adsız_tasarım__2_1.ToString();
            }
           

            if (!string.IsNullOrEmpty(picpath))
            {

                try
                {
                    File.Copy(picpath, path + picname, true);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Bu resimi Kayıt edemiyoruz \n " + e.Message);
                }
            }
            else
            {
                try
                {

                    if (File.Exists(path + picname))
                    {

                        File.Delete(path + picname);
                    }


                    File.Create(path + picname).Close();
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("Bu resimi Kayıt edemiyoruz \n " + e.Message);
                }

            }

            return path + picname;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label22.Text = "";
            y1.Visible = false;
            y2.Visible = false;
            y3.Visible = false;
            y4.Visible = false;
            y5.Visible = false;
            y6.Visible = false;
            timer1.Stop();
        }
      
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
        
            object value = (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id"].Value);
            if (value != DBNull.Value)
            {
                id = Convert.ToInt32(value);
            }
        }
        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(userlogin, "Ürün Bölümü", can.Update))
            {
                PRODUCT p = bll.Readbyid(id);

                comboBox1.Text = p.Category;
                comboBox1.Enabled = false;
                comboBox2.Text = p.Name;
                comboBox2.Enabled = false;
                comboBox3.Text = p.Cap;
                comboBox4.Text = p.Boy;
                comboBox5.Text = p.Quality;
                comboBox6.Text = p.Kaplama;
                comboBox6.Enabled = false;
                maskedTextBox1.Text = p.Price.ToString();
                maskedTextBox2.Text = p.Stock.ToString();
                maskedTextBox3.Text = p.Packing;
                comboBox7.Text = p.BrandName;
                comboBox9.Text = p.DINnumber;
                richTextBox1.Text = p.Feature;
                pictureBox3.Image = Image.FromFile (p.picture);
                button2.Text = "Düzenle";
            }
            else
            {
                message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ubll.Access(userlogin, "Ürün Bölümü", can.Delete))
            {
                MESSAGE_BOX messagebox = new MESSAGE_BOX();
                DialogResult d = messagebox.Show("Sil", "Silinsin mı ? ", "Are you sure ?", Button.yesorno, Logo.warning);
                if (d == DialogResult.Yes)
                {
                    label22.Text = "* " + bll.Delet(id);
                    timer1.Start();
                    panel1.Controls.Clear();
                    datagreadrefresh(bll.Readall());
                }
            }
            else
            {
                message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bll.Search(textBox8.Text);

        }
      
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            oppf.Filter =  @"png (*.png)|*.png";
            oppf.Title = "Kullanıcı için bir resim seçin";
            if (oppf.ShowDialog() == DialogResult.OK)
            {

                pic = Image.FromFile(oppf.FileName);


                pictureBox3.Image = pic;
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pic = pictureBox3.Image;
            }
        }
        public static List<PRODUCT> ConvertDataTableToProductList(DataTable dt)
        {
            List<PRODUCT> productList = new List<PRODUCT>();

            foreach (DataRow row in dt.Rows)
            {
                PRODUCT product = new PRODUCT
                {
                    id = Convert.ToInt32(row["id"]),
                    Category = row["Ürün"].ToString(),
                    Name = row["Ürün Adı"].ToString(),
                    Cap = row["Çap"].ToString(),
                    Boy = row["Boy"].ToString(),
                    Packing = row["Paket"].ToString(),
                    Quality = row["Kalite"].ToString(),
                   
                    Stock = Convert.ToInt32(row["Stok"]),
                    Price = Convert.ToDouble(row["Fiyat"]),
                    Kaplama = row["Kaplama"].ToString(),
                  
                    DINnumber = row["DIN"].ToString(),
                    BrandName = row["Marka"].ToString(),
                  
                    picture = row["Görsel"].ToString(),
                   
                
                };

                productList.Add(product);
            }

            return productList;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                panel1.Controls.Clear();
                datagreadrefresh(ConvertDataTableToProductList(bll.Search(textBox1.Text)));

            }
            else
            {
                panel1.Controls.Clear();
                datagreadrefresh(bll.Readall());
            }

        }
   
        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
           
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
