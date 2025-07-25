using BE;
using BLL;
using FoxLearn;
using FoxLearn.License;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace çağdaşcivata
{
    public partial class LOAD_FORMcs : Form
    {
        #region FORM DRAWİNG
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
        );
        #endregion
        public LOAD_FORMcs()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            panel1.Location = new System.Drawing.Point(42, 530);
            panel2.Location = new System.Drawing.Point(42, 530);
            panel3.Location = new System.Drawing.Point(-370, 200);
            panel1.Visible = false;
            label2.Visible = false;
            panel3.Visible = false;
            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            foreach (var item in ubll.user_save_login())
            {
                auto.Add(item.ToString());
            }
            textBox4.AutoCompleteCustomSource = auto;
        }
        private void LOAD_FORMcs_Load(object sender, EventArgs e)
        {
            timer2.Start();
            toolTip1.SetToolTip(pictureBox3, "Kayıtlı giriş bilgilerini temizle");
            toolTip1.SetToolTip(textBox4, "Kulanıcı Adınızı Girin");
            toolTip1.SetToolTip(textBox3, "Şifrenizi Girin");
            pictureBox4.Visible = false;

          

        }
        #region copy
        USER_BLL ubll = new USER_BLL();
        USER_GROUP_BLL ugbll = new USER_GROUP_BLL();
        USER_GROUP ugg = new USER_GROUP();
        MESSAGE_BOX message = new MESSAGE_BOX();
        Timer t1 = new Timer();
        Timer t2 = new Timer();
        Timer t3 = new Timer();
        Timer t5 = new Timer();
        Timer t6 = new Timer();
        OpenFileDialog oppf = new OpenFileDialog();
        public USER Login_User = new USER();

        #endregion
        #region variable
        bool _İsregstered;
        const int ProductCode = 1;
        string id;
        int y1 = 213;
        int y2 = 45;
        int y3 = 205;
        int x = -20;
        int x1 = -100;
        int x6 = 33;
        int x7 = -370;
        Image pic;
        #endregion
        #region PLMetod
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
        string Savepic(string Username)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\UserPic\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string picname = Username + ".jpg";

            string picpath = oppf.FileName;

            if (!string.IsNullOrEmpty(picpath))
            {

                try
                {
                    File.Copy(picpath, path + picname, true);
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("Bu resimi Kayıt edemiyoruz \n " + e.Message);
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

        void ugroupcreate()
        {
            ugg.Title = "Admin";
            ugg.Roles.Add(FillRoll("Müşteri Bölümü", true, true, true, true));
            ugg.Roles.Add(FillRoll("Ürün Bölümü", true, true, true, true));
            ugg.Roles.Add(FillRoll("Satiş Bölümü", true, true, true, true));
            ugg.Roles.Add(FillRoll("Aktivite Bölümü ", true, true, true, true));
            ugg.Roles.Add(FillRoll("Htırlatıcı Bölümü", true, true, true, true));
            ugg.Roles.Add(FillRoll("Kulanıcı Bölümü", true, true, true, true));
            ugg.Roles.Add(FillRoll("Sms Portalı", true, true, true, true));
            ugg.Roles.Add(FillRoll("Rapor Bölümü", true, true, true, true));
            ugg.Roles.Add(FillRoll("Ayarlar Bölümü", true, true, true, true));
            ugbll.Create(ugg);
        }
        #endregion
        #region EVENT
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = ubll.user_pass_login(textBox4.Text);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes == message.Show("Sil", "Kayıtlı Kulanıcı giriş bilgileri silinsin mı ?", "", Button.yesorno, Logo.warning))
            {
                ubll.userpass_delet();
                textBox4.AutoCompleteCustomSource = null;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            w.Close();
            System.Windows.Forms.Application.Exit();
        }

        private void timertick1(object sender, EventArgs e)
        {
            if (progressBar1.Value >= 100)
            {
                t1.Stop();
                progressBar1.Visible = false;
                pictureBox4.Visible = true;
                label1.Visible = false;
                label2.Visible = true;
                t2.Enabled = true;
                t2.Interval = 10;
                t2.Tick += timertick2;
                t2.Start();
            }
            else if (progressBar1.Value >= 30)
            {
                _İsregstered = ubll.İsregestered();
                progressBar1.Value++;
            }
            else
            {
                progressBar1.Value++;
            }
        }
        private void timertick2(object sender, EventArgs e)
        {

            if (label2.Location.Y >= 150)
            {
                y1 = y1 - 10;
                label2.Location = new System.Drawing.Point(90, y1);
                y2 = y2 - 1;
                y3 = y3 - 3;
                label2.Location = new System.Drawing.Point(90, y1);
                pictureBox1.Location = new System.Drawing.Point(68, y2);
                if (_İsregstered)
                {
                    panel2.Visible = true;
                    panel2.Location = new System.Drawing.Point(43, y3);

                }
                else
                {
                    panel1.Visible = true;
                    panel1.Location = new System.Drawing.Point(43, y3);

                }

            }
            else
            {
                t2.Stop();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(label22.Text);
            string productKey = textBox2.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "Personal accounting";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }

                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", System.Windows.Forms.Application.StartupPath), lic);

                    message.Show("Bilgi", "Program Aktivleştirildi", "Tebrikler", Button.ok, Logo.info);
                    t5.Enabled = true;
                    t5.Interval = 20;
                    t5.Tick += ticker5;
                    t5.Start();

                }
            }
        }
        private void ticker5(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            if (panel1.Location.X <= 450)
            {
                x = x + 55;
                x1 = x1 + 5;
                panel1.Location = new System.Drawing.Point(x, 205);
                panel3.Visible = true;
                panel3.Location = new System.Drawing.Point(x, 200);
            }
            else
            {
                t5.Stop();
            }
            t5.Stop();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            timer1.Start();
            t3.Enabled = true;
            System.Windows.Clipboard.SetText(label22.Text);
            id = label22.Text;
            label21.Text = "";
            label21.Visible = true;
            label21.Text = "Bilgisayar Kodu Kopyalandı ";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label21.Text = "";
            label21.Visible = false;
            timer1.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label21.Visible = false;
            label2.Visible = false;
            t1.Enabled = true;
            t1.Interval = 60;
            t1.Tick += timertick1;
            t1.Start();
            label22.Text = FoxLearn.License.ComputerInfo.GetComputerId();
            timer2.Stop();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            oppf.Filter = @"JPG (*.jpg)|*.jpg";
            oppf.Title = "Kullanıcı için bir resim seçin";
            if (oppf.ShowDialog() == DialogResult.OK)
            {
                pic = Image.FromFile(oppf.FileName);
                pictureBox2.Image = pic;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            USER u = new USER();
            if (textBox7.Text == textBox8.Text)
            {
                u.Password = textBox7.Text;
                u.Name = textBox6.Text;
                u.UserName = textBox5.Text;
                u.Regtime = DateTime.Now.Date;
                u.Status = "Müdür";
                u.Pic = Savepic(textBox5.Text);
                if (u.Pic != string.Empty)
                {
                    pictureBox2.Image = Image.FromFile(u.Pic);
                }
                ugroupcreate();

                message.Show("Bilgi", ubll.Create(u, ugbll.getug_bytitle("Admin"), checkBox1.Checked), "", Button.ok, Logo.info);
                panel3.Visible = false;
                panel1.Visible = false;
                t6.Enabled = true;
                t6.Interval = 2;
                t6.Tick += tick6;
                panel2.Location = new System.Drawing.Point(-370, 260);
                t6.Start();
            }
            else
            {
                textBox7.Text = "";
                textBox8.Text = "";
                message.Show("Şifre", "Şifre tekrarı hatalı ", "", Button.ok, Logo.warning);
            }
        }

        private void tick6(object sender, EventArgs e)
        {
            if (panel3.Location.X <= 470)
            {
                x = x6 + 10;
                x1 = x7 + 10;
                
                panel3.Location = new System.Drawing.Point(x, 205);
                panel2.Visible = true;
                panel2.Location = new System.Drawing.Point(x, 200);
            }
            else
            {
                t5.Stop();
            }
            t5.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login_User = ubll.Login(textBox4.Text, textBox3.Text, checkBox2.Checked);
            if (Login_User != null)
            {
                MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
                w.userlogin = Login_User;
                this.Close();
            }
            else
            {
                message.Show("Uyarı", "Kulanıcı Adı ve ya şifre hatalı ", "tekrar deneyın", Button.ok, Logo.warning);
            }
        }
        #endregion

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
