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
    public partial class USER_FORM : Form
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
        public USER_FORM()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void USER_FORM_Load(object sender, EventArgs e)
        {
            datagrid2_refresh();
            datagrid_refresh(bll.Gwt_All_User());
            datagridviewsetting(dataGridView1);
            label26.BackColor = System.Drawing.Color.Transparent;
            Comboboxfill();
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            loginuser = w.userlogin;
            panel1.AutoScroll = true;
        }

        #region copy
        USER u = new USER();
        USER_GROUP ug = new USER_GROUP();
        USER loginuser = new USER();

        USER_BLL bll = new USER_BLL();
        USER_GROUP_BLL ugbll = new USER_GROUP_BLL();

        OpenFileDialog oppf = new OpenFileDialog();
        MESSAGE_BOX message = new MESSAGE_BOX();
        #endregion
        #region Variable
        public int id;
        int ugid;
        int ugiddatagrid;
        Image pic;
        #endregion
        #region PL Metod
        void Comboboxfill()
        {
            comboBox1.Items.Clear();
            List<string> title = ugbll.GetTitle();
            comboBox1.Items.AddRange(title.ToArray());
        }
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            Comboboxfill();
        }
        void ugroupcreate()
        {
            ug.Title = textBox8.Text;
            ug.Roles.Add(FillRoll(label8.Text, checkBox8.Checked, checkBox7.Checked, checkBox6.Checked, checkBox5.Checked));
            ug.Roles.Add(FillRoll(label9.Text, checkBox12.Checked, checkBox11.Checked, checkBox10.Checked, checkBox9.Checked));
            ug.Roles.Add(FillRoll(label12.Text, checkBox16.Checked, checkBox15.Checked, checkBox14.Checked, checkBox13.Checked));
            ug.Roles.Add(FillRoll(label13.Text, checkBox20.Checked, checkBox19.Checked, checkBox18.Checked, checkBox17.Checked));
            ug.Roles.Add(FillRoll(label14.Text, checkBox24.Checked, checkBox23.Checked, checkBox22.Checked, checkBox21.Checked));
            ug.Roles.Add(FillRoll(label15.Text, checkBox28.Checked, checkBox27.Checked, checkBox26.Checked, checkBox25.Checked));
            ug.Roles.Add(FillRoll(label16.Text, checkBox32.Checked, checkBox31.Checked, checkBox30.Checked, checkBox29.Checked));
            ug.Roles.Add(FillRoll(label17.Text, checkBox36.Checked, checkBox35.Checked, checkBox34.Checked, checkBox33.Checked));
            ug.Roles.Add(FillRoll(label18.Text, checkBox40.Checked, checkBox39.Checked, checkBox38.Checked, checkBox37.Checked));

            ugbll.Create(ug);
            textBox8.Text = "";
            CheckBox_clear();
        }
        USER_ACCESS_ROLE FillRoll(string Section, bool CanRead, bool CanCreate, bool CanUpdate, bool CanDelete)
        {
            USER_ACCESS_ROLE uar = new USER_ACCESS_ROLE();
            uar.Section = Section;
            uar.CanRead = CanRead;
            uar.CanCreate = CanCreate;
            uar.CanUpdate = CanUpdate;
            uar.CamDelete = CanDelete;
            return uar;
        }
        void Taketext(USER u)
        {
            u.Name = textBox1.Text;
            u.UserName = textBox2.Text;
            u.TC = textBox6.Text;
            u.PhoneNumber = maskedTextBox1.Text.ToString();
            u.E_mail = maskedTextBox2.Text.ToString();
            u.Adress = richTextBox1.Text;            
            if (textBox3.Text == textBox4.Text)
            {
                u.Password = textBox4.Text;
            }
            else { System.Windows.MessageBox.Show("Şifreler Eşleşmedı Tekrar Deneyın", "Uyarı"); }
            u.Status = comboBox1.Text;
            u.Regtime = DateTime.Now;
            if (oppf.FileName != string.Empty||oppf.FileName != null)
            {
                u.Pic = Savepic(u.UserName);
            }
           
        }
        void textclear()
        {
            textBox6.Text = string.Empty;
            maskedTextBox1.Text = string.Empty;
            maskedTextBox2.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            comboBox1.Text = string.Empty;
            pictureBox1.Image = Properties.Resources.Adsız_tasarım__2_1;

            textBox1.Focus();

        }
        void text_insert(USER u)
        {
            textBox6.Text= u.TC ;
            maskedTextBox1.Text = u.PhoneNumber ;
            maskedTextBox2.Text = u.E_mail ;
           richTextBox1.Text = u.Adress ;
            textBox1.Text = u.Name;
            textBox2.Text = u.UserName;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            comboBox1.Text = u.userGroup.Title;
            if (u.Pic != string.Empty)
            {
                pictureBox1.Image = Image.FromFile(u.Pic);
            }

            textBox1.Focus();
        }

        void datagrid_refresh (List<USER> u)
        {
            panel1.Controls.Clear();
            List<USER> user = u ;
            int a = 0;
            foreach (var item in user )
            {
                User_Datagrid_uc md = new User_Datagrid_uc();
                md.idlbl.Text = item.id.ToString();
                md.tclbl.Text = item.TC;
                md.tellbl.Text = item.PhoneNumber;
                md.usernamelbl.Text = item.UserName;
                md.adresslbl.Text = item.Adress;
                md.emaillbl.Text = item.E_mail;
                md.görevlbl.Text = item.Status;
                md.namelbl.Text = item.Name;
                
                if (item.Pic != "")
                {
                    md.pictureBox1.Image = Image.FromFile(item.Pic);
                }
                else
                {
                    md.pictureBox1.Image = Properties.Resources.Adsız_tasarım__2_1;
                }
                panel1.Controls.Add(md);
                md.Location = new System.Drawing.Point(0, 0 + a);
                a = a + 60;
            }

        }
        void datagrid2_refresh()
        {
            datagridviewsetting(dataGridView2);
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = ugbll.GetAll();
            dataGridView2.Columns["id"].Visible = false;

        }
        //string Savepic(string Username)
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory + @"\UserPic\";
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    string picname = Username + ".jpg";
        //    string picpath;



        //         picpath = oppf.FileName;



        //    if (!string.IsNullOrEmpty(picpath))
        //    {

        //        try
        //        {
        //            File.Copy(picpath, path + picname, true);
        //        }
        //        catch (Exception e)
        //        {
        //            System.Windows.Forms.MessageBox.Show("Bu resimi Kayıt edemiyoruz \n " + e.Message);
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {

        //            if (File.Exists(path + picname))
        //            {

        //                File.Delete(path + picname);
        //            }


        //            File.Create(path + picname).Close();
        //        }
        //        catch (Exception e)
        //        {
        //            System.Windows.MessageBox.Show("Bu resimi Kayıt edemiyoruz \n " + e.Message);
        //        }

        //    }

        //    return path + picname;
        //}
        string Savepic(string KullaniciAdi)
        {
            // Resimlerin kaydedileceği dizini belirle
            string dizin = AppDomain.CurrentDomain.BaseDirectory + @"\UserPic\";

            // Dizin yoksa oluştur
            if (!Directory.Exists(dizin))
            {
                Directory.CreateDirectory(dizin);
            }

            // Resim adını ve yolunu belirle
            string resimAdi = KullaniciAdi + ".jpg";
            string resimYolu = oppf.FileName;

            // Eğer resim yolu boş değilse
            if (!string.IsNullOrEmpty(resimYolu))
            {
                try
                {
                    // Eğer aynı isimde bir dosya varsa sil
                    if (File.Exists(dizin + resimAdi))
                    {
                        File.Delete(dizin + resimAdi);
                    }

                    // Dosyayı belirtilen dizine kopyala
                    File.Copy(resimYolu, dizin + resimAdi, true);
                }
                catch (Exception e)
                {
                    // Hata durumunda hata mesajını göster
                    System.Windows.Forms.MessageBox.Show("Bu resmi kaydedemiyoruz \n " + e.Message);
                }
            }
            else
            {
                try
                {
                    // Eğer resim yolu boşsa ve dosya zaten varsa sil
                    if (File.Exists(dizin + resimAdi))
                    {
                        File.Delete(dizin + resimAdi);
                    }

                    // Yeni bir boş dosya oluştur
                    File.Create(dizin + resimAdi).Close();
                }
                catch (Exception e)
                {
                    // Hata durumunda hata mesajını göster
                    System.Windows.MessageBox.Show("Bu resmi kaydedemiyoruz \n " + e.Message);
                }
            }

            // Kaydedilen resmin tam yolunu döndür
            return dizin + resimAdi;
        }

        public void datagridviewsetting(DataGridView d)
        {
            d.DefaultCellStyle.BackColor = Color.FromArgb(108, 117, 125);
            d.DefaultCellStyle.ForeColor = Color.White;
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
        private void sİlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bll.Access(loginuser, "Kulanıcı Bölümü", can.Delete))
            {
                DialogResult i = message.Show("Silme", "Kulanıcı Yetki Başlığı Silinsin mı?", "", Button.yesorno, Logo.warning);
                if (i == DialogResult.Yes)
                {
                    ugbll.delete(ugiddatagrid);
                }
                datagrid2_refresh();
            }
            else
            {
                message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
            }

        }
        #endregion
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            oppf.Filter = @"JPG (*.jpg)|*.jpg";
            oppf.Title = "Kullanıcı için bir resim seçin";
            if (oppf.ShowDialog() == DialogResult.OK)
            {

                pic = Image.FromFile(oppf.FileName);


                pictureBox1.Image = pic;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pic = pictureBox1.Image;
            }
        }


       
        private void button3_Click(object sender, EventArgs e)
        {

            if (richTextBox1.Text != string.Empty && maskedTextBox2.Text != string.Empty && maskedTextBox1.Text != string.Empty && textBox6.Text != string.Empty && textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty)
            {
                ug = ugbll.getug_bytitle(comboBox1.Text);
                Taketext(u);
                if (button3.Text == "Kayıt")
                {
                    if (bll.Access(loginuser, "Kulanıcı Bölümü", can.Create))
                    {
                        message.Show("Kayıt", bll.Create(u, ug, false), "", Button.ok, Logo.info);
                        datagrid_refresh(bll.Gwt_All_User());
                        textclear();
                    }
                    else
                    {
                        message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
                    }

                }
                else
                {
                    if (bll.Access(loginuser, "Kulanıcı Bölümü", can.Update))
                    {
                        message.Show("Düzenleme", bll.update(id, u, ug), "", Button.ok, Logo.info);
                        button3.Text = "Kayıt";
                        datagrid_refresh(bll.Gwt_All_User());
                        textclear();
                    }
                    else
                    {
                        message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
                    }
                }
            }
            else
            {
                message.Show("Kayıt", "Tüm Blgileri Doldurduğunuzdan Emin olun.", "", Button.ok, Logo.warning);
            }
            textBox1.Focus();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            object value = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id"].Value;
            if (value != DBNull.Value)
            {
                id = Convert.ToInt32(value);
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bll.Access(loginuser, "Kulanıcı Bölümü",can.Update))
            {
                if (id != 0)
                {
                    USER_GROUP ugroup = ugbll.Get(bll.get_usergroupid_by_userid(id));
                    USER u = bll.Read_By_İd(id);
                    text_insert(u);

                }
                else { System.Windows.MessageBox.Show("Kulanıcıyı Seçtiğinizden Eminmısınız?? \n Farenin Sol Dğmesini Kulanarak Seçim Yapın", " Tekrar Deneyın "); }
                button3.Text = "Düzenle";
            }
            else
            {
                message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bll.Access(loginuser, "Kulanıcı Bölümü",can.Delete))
            {
                MESSAGE_BOX messagebox = new MESSAGE_BOX();
                DialogResult d = messagebox.Show("Sil", "Silinsin mı ? ", "Are you sure ?", Button.yesorno, Logo.warning);
                if (d == DialogResult.Yes)
                {
                    if (id != 0)
                    {
                        bll.Delet(id);
                       
                        datagrid_refresh(bll.Gwt_All_User());
                    }
                    else
                    {

                        messagebox.Show("Tekrar Deneyın", "Kulanıcıyı Seçtiğinizden Eminmısınız?? \n Farenin Sol Dğmesini Kulanarak Seçim Yapın", "Are you sure ?", Button.yesorno, Logo.warning);
                    }

                }
                
            }
            else
            {
                message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bll.Access(loginuser, "Kulanıcı Bölümü", can.Create))
            {
                if (textBox8.Text != string.Empty)
                {
                    ugroupcreate();
                    datagrid2_refresh();
                    Comboboxfill();
                }
                else
                {
                    message.Show("uyarı","Ünvan boş bırakılmaz ","",Button.ok, Logo.warning);
                }
            }
            else
            {
                message.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
            }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
                object value = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["id"].Value);
                if (value != DBNull.Value)
                {
                    ugiddatagrid = Convert.ToInt32(value);
                }
            }
            catch (Exception t)
            {

                message.Show("uyarı", "Bir hata oluştu", t.Message, Button.ok, Logo.warning);
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        #region Check Box
        void CheckBox_clear()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox18.Checked = false;
            checkBox19.Checked = false;
            checkBox20.Checked = false;
            checkBox21.Checked = false;
            checkBox22.Checked = false;
            checkBox23.Checked = false;
            checkBox24.Checked = false;
            checkBox25.Checked = false;
            checkBox26.Checked = false;
            checkBox27.Checked = false;
            checkBox28.Checked = false;
            checkBox29.Checked = false;
            checkBox30.Checked = false;
            checkBox31.Checked = false;
            checkBox32.Checked = false;
            checkBox33.Checked = false;
            checkBox34.Checked = false;
            checkBox35.Checked = false;
            checkBox36.Checked = false;
            checkBox37.Checked = false;
            checkBox38.Checked = false;
            checkBox39.Checked = false;
            checkBox40.Checked = false;
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox8.Checked = true;
                checkBox12.Checked = true;
                checkBox16.Checked = true;
                checkBox20.Checked = true;
                checkBox24.Checked = true;
                checkBox28.Checked = true;
                checkBox32.Checked = true;
                checkBox36.Checked = true;
                checkBox40.Checked = true;
            }
            else
            {
                checkBox8.Checked = false;
                checkBox12.Checked = false;
                checkBox16.Checked = false;
                checkBox20.Checked = false;
                checkBox24.Checked = false;
                checkBox28.Checked = false;
                checkBox32.Checked = false;
                checkBox36.Checked = false;
                checkBox40.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox7.Checked = true;
                checkBox11.Checked = true;
                checkBox15.Checked = true;
                checkBox19.Checked = true;
                checkBox23.Checked = true;
                checkBox27.Checked = true;
                checkBox31.Checked = true;
                checkBox35.Checked = true;
                checkBox39.Checked = true;
            }
            else
            {
                checkBox7.Checked = false;
                checkBox11.Checked = false;
                checkBox15.Checked = false;
                checkBox19.Checked = false;
                checkBox23.Checked = false;
                checkBox27.Checked = false;
                checkBox31.Checked = false;
                checkBox35.Checked = false;
                checkBox39.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {

                checkBox6.Checked = true;
                checkBox10.Checked = true;
                checkBox14.Checked = true;
                checkBox18.Checked = true;
                checkBox22.Checked = true;
                checkBox26.Checked = true;
                checkBox30.Checked = true;
                checkBox34.Checked = true;
                checkBox38.Checked = true;

            }
            else
            {
                checkBox6.Checked = false;
                checkBox10.Checked = false;
                checkBox14.Checked = false;
                checkBox18.Checked = false;
                checkBox22.Checked = false;
                checkBox26.Checked = false;
                checkBox30.Checked = false;
                checkBox34.Checked = false;
                checkBox38.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {

                checkBox5.Checked = true;
                checkBox9.Checked = true;
                checkBox13.Checked = true;
                checkBox17.Checked = true;
                checkBox21.Checked = true;
                checkBox25.Checked = true;
                checkBox29.Checked = true;
                checkBox33.Checked = true;
                checkBox37.Checked = true;

            }
            else
            {
                checkBox5.Checked = false;
                checkBox9.Checked = false;
                checkBox13.Checked = false;
                checkBox17.Checked = false;
                checkBox21.Checked = false;
                checkBox25.Checked = false;
                checkBox29.Checked = false;
                checkBox33.Checked = false;
                checkBox37.Checked = false;
            }
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
            {
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            if (!checkBox8.Checked && !checkBox7.Checked && !checkBox6.Checked && !checkBox5.Checked)
            {
                checkBox8.Checked = true;
                checkBox7.Checked = true;
                checkBox6.Checked = true;
                checkBox5.Checked = true;
            }
            else
            {
                checkBox8.Checked = false;
                checkBox7.Checked = false;
                checkBox6.Checked = false;
                checkBox5.Checked = false;
            }
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            if (!checkBox12.Checked && !checkBox11.Checked && !checkBox10.Checked && !checkBox9.Checked)
            {
                checkBox12.Checked = true;
                checkBox11.Checked = true;
                checkBox10.Checked = true;
                checkBox9.Checked = true;
            }
            else
            {
                checkBox12.Checked = false;
                checkBox11.Checked = false;
                checkBox10.Checked = false;
                checkBox9.Checked = false;
            }
        }

        private void label12_Click_1(object sender, EventArgs e)
        {
            if (!checkBox16.Checked && !checkBox15.Checked && !checkBox14.Checked && !checkBox14.Checked)
            {
                checkBox16.Checked = true;
                checkBox15.Checked = true;
                checkBox14.Checked = true;
                checkBox13.Checked = true;
            }
            else
            {
                checkBox16.Checked = false;
                checkBox15.Checked = false;
                checkBox14.Checked = false;
                checkBox13.Checked = false;
            }
        }

        private void label13_Click_1(object sender, EventArgs e)
        {
            if (!checkBox20.Checked && !checkBox19.Checked && !checkBox18.Checked && !checkBox17.Checked)
            {
                checkBox20.Checked = true;
                checkBox19.Checked = true;
                checkBox18.Checked = true;
                checkBox17.Checked = true;
            }
            else
            {
                checkBox20.Checked = false;
                checkBox19.Checked = false;
                checkBox18.Checked = false;
                checkBox17.Checked = false;
            }
        }

        private void label14_Click_1(object sender, EventArgs e)
        {

            if (!checkBox24.Checked && !checkBox23.Checked && !checkBox22.Checked && !checkBox21.Checked)
            {
                checkBox24.Checked = true;
                checkBox23.Checked = true;
                checkBox22.Checked = true;
                checkBox21.Checked = true;
            }
            else
            {
                checkBox24.Checked = false;
                checkBox23.Checked = false;
                checkBox22.Checked = false;
                checkBox21.Checked = false;
            }
        }

        private void label15_Click_1(object sender, EventArgs e)
        {
            if (!checkBox28.Checked && !checkBox27.Checked && !checkBox26.Checked && !checkBox25.Checked)
            {
                checkBox28.Checked = true;
                checkBox27.Checked = true;
                checkBox26.Checked = true;
                checkBox25.Checked = true;
            }
            else
            {
                checkBox28.Checked = false;
                checkBox27.Checked = false;
                checkBox26.Checked = false;
                checkBox25.Checked = false;
            }
        }

        private void label16_Click_1(object sender, EventArgs e)
        {
            if (!checkBox32.Checked && !checkBox31.Checked && !checkBox30.Checked && !checkBox29.Checked)
            {
                checkBox32.Checked = true;
                checkBox31.Checked = true;
                checkBox30.Checked = true;
                checkBox29.Checked = true;
            }
            else
            {
                checkBox32.Checked = false;
                checkBox31.Checked = false;
                checkBox30.Checked = false;
                checkBox29.Checked = false;
            }
        }

        private void label17_Click_1(object sender, EventArgs e)
        {
            if (!checkBox36.Checked && !checkBox35.Checked && !checkBox34.Checked && !checkBox33.Checked)
            {
                checkBox36.Checked = true;
                checkBox35.Checked = true;
                checkBox34.Checked = true;
                checkBox33.Checked = true;
            }
            else
            {
                checkBox36.Checked = false;
                checkBox35.Checked = false;
                checkBox34.Checked = false;
                checkBox33.Checked = false;

            }
        }

        private void label18_Click_1(object sender, EventArgs e)
        {
            if (!checkBox40.Checked && !checkBox39.Checked && !checkBox38.Checked && !checkBox37.Checked)
            {
                checkBox40.Checked = true;
                checkBox39.Checked = true;
                checkBox38.Checked = true;
                checkBox37.Checked = true;
            }
            else
            {
                checkBox40.Checked = false;
                checkBox39.Checked = false;
                checkBox38.Checked = false;
                checkBox37.Checked = false;
            }
        }
        #endregion

        private void sİlToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if  (DialogResult.Yes == message.Show("Sil","Silinsin mı ?","", Button.yesorno, Logo.warning))
            {
                ugbll.delete(ugiddatagrid);
                datagrid2_refresh();
            }
        }
    }
}
