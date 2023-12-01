using BE;
using BLL;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Stimulsoft;
using Stimulsoft.Report;
using System.Windows.Media.Effects;

namespace çağdaşcivata
{
    public partial class İNVOİCE_FORM2 : Form
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
        public İNVOİCE_FORM2()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            panel1.Visible=false;
            dateTimePickervade.Value = DateTime.Now;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ibll.readall();
            dataGridView1.Columns["id"].Visible = false;
        }

       

        private void İNVOİCE_FORM2_Load(object sender, EventArgs e)
        {
            #region cutomer Search

            AutoCompleteStringCollection name = new AutoCompleteStringCollection();
            foreach (var item in cbll.Readname())
            {
                name.Add(item);
            }
            Custumert_Search_Txt.AutoCompleteCustomSource = name;
            #endregion
            #region Datagrid
            datagridviewsetting(dataGridView4);
            datagridviewsetting(dataGridView2);
            datagridviewsetting(dataGridView1);
            #endregion            
            UrundatagridFill(ConvertDataTableToProductList(pbll.ReadAll()));
            #region Enable Controls
            pictureBox2.Enabled = false;
            button1.Enabled = false;
            Enable_controls(false);
            #endregion
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            userlogin = w.userlogin;
            comboBoxödemeşekli.Items.AddRange(Enum.GetValues(typeof(PAYMENT_METHOD)).Cast<PAYMENT_METHOD>().Select(İ => İ.ToString()).ToArray());
        }
        #region Copy
        public int id_product;//İnvoice_datagrid_uc den id geliyor
        int id_Custumer; // datagrid4.cellclick den gelior
        CUSTOMER ChoseCudtomer = new CUSTOMER();
        CUSTOMER_BLL cbll = new CUSTOMER_BLL();
        USER Chose_Customer_user = new USER();
        USER_BLL ubll = new USER_BLL();
        public PRODUCT product = new PRODUCT();
        public List<PRODUCT> PRODUCTs = new List<PRODUCT>();
        PRODUCT_BLL pbll = new PRODUCT_BLL();
        MESSAGE_BOX mesage = new MESSAGE_BOX();
        USER userlogin = new USER();
        List<PRODUCT> productsstimulsoft = new List<PRODUCT>();


        void Enable_controls(bool e)
        {
            urunadet.Enabled = e;
            checkBoxfiyat.Enabled = e;
            fiyattxt.Enabled = e;
            radioButton100adet.Enabled = e;
            radioButton1adet.Enabled = e;
            comboBoxiskonto.Enabled = e;
            comboBoxkar.Enabled = e;
            checkBoxödeme.Enabled = e;
            comboBoxödemeşekli.Enabled = e;
            dateTimePickervade.Enabled = e;
            ödemetutartxt.Enabled = e;
            buttonsiparişoluştur.Enabled = e;
            button2.Enabled = e;
            ürün_eklebutonu.Enabled = e;

        }
        #endregion
        public void datagrid_fill_reeadall_product()
        {
            UrundatagridFill(ConvertDataTableToProductList(pbll.ReadAll()));
        }
        public void Take_produt(int id)
        {
            product = pbll.Readbyid(id);
            
        }
        private void Custumert_Search_Txt_TextChanged(object sender, EventArgs e)
        {
            dataGridView4.DataSource = null;
            dataGridView4.DataSource = cbll. İnvoice_Customer_Search(Custumert_Search_Txt.Text);
            dataGridView4.Columns["id"].Visible = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
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
        
        void Cart_Txt_clear()
        {
            urunadet.Text = "";
            UrunSearchtxt.Text = "";
            checkBoxfiyat.Checked = false;
            fiyattxt.Text = "";
            radioButton100adet.Checked = false;
            radioButton1adet.Checked = false;
            label34.Text = "........";
       
          

        }
        public void UrundatagridFill( List<PRODUCT> p)
        {
            paneldata.Controls.Clear();
            List<PRODUCT> products = p;
            int a = 0;
            int s = 0;
            foreach (var u in products)
            {
                if (s<= 10)
                {
                    İnvoice_datagrid_uc i = new İnvoice_datagrid_uc();
                    i.idlbl.Text = u.id.ToString();
                    i.categorylbl.Text = u.Category;
                    i.namelbl.Text = u.Name;
                    i.caplbl.Text = u.Cap;
                    i.boylbl.Text = u.Boy;
                    i.paketlbl.Text = u.Packing;
                    i.textBox1.Text = u.Packing;
                    i.Qualitylbl.Text = u.Quality;
                    i.Stocklbl.Text = u.Stock.ToString();
                    i.pricelbl.Text = u.Price.ToString("N2");
                    i.Brandlbl.Text = u.BrandName;
                    i.kaplamalbl.Text = u.Kaplama;
                    i.salepicslbl.Text = u.SaledPices.ToString();
                    i.dınlbl.Text = u.DINnumber;
                    i.pictureBox1.Image = System.Drawing.Image.FromFile(u.picture);
                    if (u.picture != "")
                    {
                        i.pictureBox1.Image = System.Drawing.Image.FromFile(u.picture);
                    }
                    else
                    {
                        i.pictureBox1.Image = Properties.Resources.Adsız_tasarım__2_1;
                    }
                    i.ozelliklbl.Text = u.Feature;
                    if (i.categorylbl.Text == "Civata" || i.categorylbl.Text == "Özel Civata")
                    {
                        i.Hederlbl.Text = u.DINnumber +" "+ u.Name + " M" + u.Cap + " x " + u.Boy + " " + u.Quality + " " + u.Kaplama;
                    }
                    else if (i.categorylbl.Text == "Vida" || i.categorylbl.Text == "Özel Vida")
                    {
                        if (i.namelbl.Text == "YSB METRİK" || i.namelbl.Text == "YHB METRİK" || i.namelbl.Text == "RYSB METRİK" || i.namelbl.Text == "METRİK TORKS" || i.namelbl.Text == "ÖZEL VİDA METRİK")
                        {
                            i.Hederlbl.Text = u.DINnumber + " " + u.Name + " M" + u.Cap + " x " + u.Boy + " " + u.Quality + " " + u.Kaplama;
                        }
                        else
                        {
                            i.Hederlbl.Text = u.DINnumber + " " + u.Name + " " + u.Cap + " x " + u.Boy + " " + u.Quality + " " + u.Kaplama;
                        }                       
                    }
                    else if (i.categorylbl.Text == "Somun" || i.categorylbl.Text == "Özel Somun")
                    {
                        i.Hederlbl.Text =  u.Name + " M" + u.Cap + " " + u.Quality + " " + u.Kaplama;
                    }
                    else if(i.categorylbl.Text == "Dübel" || i.categorylbl.Text == "Özel Dübel")
                    {
                        if (i.caplbl.Text == "10 mm" || i.caplbl.Text == "12 mm" || i.caplbl.Text == "3 mm" || i.caplbl.Text == "6 mm" || i.caplbl.Text == "7 mm" || i.caplbl.Text == "8 mm")
                        {
                            i.Hederlbl.Text =  u.Name + " " + u.Cap + " x " + u.Boy + " " + u.Kaplama;
                        }
                        else
                        {
                            i.Hederlbl.Text = u.Name + " M" + u.Cap + " x " + u.Boy + " " + u.Kaplama;
                        }
                    }
                    else if (i.categorylbl.Text == "Saplama" || i.categorylbl.Text == "Özel Saplama")
                    {
                        i.Hederlbl.Text = u.Name + " M" + u.Cap + " x " + u.Boy + " " + u.Kaplama;
                    }
                    else if (i.categorylbl.Text == "Pul" || i.categorylbl.Text == "Özel Pul")
                    {
                        i.Hederlbl.Text =u.DINnumber+" " + u.Name + " M" + u.Cap  + " " + u.Quality +" "+ u.Kaplama;
                    }
                    
                    paneldata.Controls.Add(i);
                    i.Location = new System.Drawing.Point(0, a);
                    a = a + 135;
                    s = s + 1;
                }

                //Civata
                //Vida
                //Somun
                //Dübel
                //Saplama
                //Pul
                //Özel Ürün

            }

        }
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            try
            {
                id_Custumer = Convert.ToInt32(dataGridView4.Rows[dataGridView4.CurrentRow.Index].Cells["id"].Value);

            }
            catch (Exception q)
            {
                return;
                
            }
        }

        private void müşteriSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (id_Custumer != 0)
                {
                    ChoseCudtomer = cbll.Readbyid(id_Custumer);
                
                
                    if (ChoseCudtomer != null)
                    {
                       Custumert_Search_Txt.Text = ChoseCudtomer.Company;
                       Custumert_Search_Txt.Enabled = false;
                       dataGridView4.Enabled = false;
                       pictureBox2.Enabled = true;
                       button1.Enabled = true;
                        
                        label52.Text = ChoseCudtomer.Company;
                       label58.Text = ChoseCudtomer.Name;
                       Emaillbl.Text = ChoseCudtomer.Email;
                       label56.Text = ChoseCudtomer.Phone;
                       label1.Text = ChoseCudtomer.Alacak.ToString("N2");
                       label3.Text = ChoseCudtomer.Bakiye.ToString("N2");
                       label5.Text = ChoseCudtomer.Adress;
                       Chose_Customer_user = ubll.Read_By_İd(cbll.User_id_From_Custumer_id(ChoseCudtomer.id));
                       label38.Text = Chose_Customer_user.Name;
                      label54.Text = ChoseCudtomer.Regdate.ToString("dd,MM,yyyy");
                    }
                    else
                    {
                        mesage.Show("Uyarı ", "Müştri Bulunamadı ", "", Button.ok, Logo.warning);
                    }
                }
                else
                {
                    mesage.Show("Uyarı ", "Müştri Seçtığınızden emin olun ", "", Button.ok, Logo.warning);
                }
            }
            catch (Exception q)
            {

                mesage.Show("Uyarı ", "İşlem sırasında bir hata oluştu", q.Message, Button.ok, Logo.warning);
            }
           


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Custumert_Search_Txt.Text = "";
            Custumert_Search_Txt.Enabled = true;
            dataGridView4.DataSource = null;
            dataGridView4.Enabled = true;
            pictureBox2.Enabled = false;
            button1.Enabled = false;
            label52.Text = "";
            label58.Text = "";
            Emaillbl.Text ="";
            label56.Text = "";
            label1.Text =   "";
            label3.Text = "";
            label5.Text = "";
            Chose_Customer_user = null;
            label38.Text = "";
            label54.Text = "";
            id_Custumer = 0;    
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
            panel1.Visible = true;
            panel1.Location = new System.Drawing.Point(10, 12);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panel1.Visible=false;
            //panel1.Controls.Clear();
            //Enable_controls(false);
            urunadet.Enabled = false;
            checkBoxfiyat.Enabled = false;
            fiyattxt.Enabled = false;
            radioButton100adet.Enabled = false;
            radioButton1adet.Enabled = false;
            comboBoxiskonto.Enabled = false;
            comboBoxkar.Enabled = false;
            checkBoxödeme.Enabled = false;
            comboBoxödemeşekli.Enabled = false;
            dateTimePickervade.Enabled = false;
            ödemetutartxt.Enabled = false;
            buttonsiparişoluştur.Enabled = false;
            button2.Enabled = false;
            ürün_eklebutonu.Enabled = false;
            Cart_Txt_clear();
            toplamtutarlbl.Text = "";
            kdvlbl.Text = "";
            tutarlbl.Text = "";
            checkBoxödeme.Checked = false;
            comboBoxödemeşekli.Text = "";
            ödemetutartxt.Text = "";
            PRODUCTs.Clear();
            dataGridView2.DataSource = null;
            product = null;
            paneldata.Enabled = true;
            UrunSearchtxt.Enabled = true;
        }

        private void UrunSearchtxt_TextChanged(object sender, EventArgs e)
        {
            if (UrunSearchtxt.Text != string.Empty)
            {

                UrundatagridFill(ConvertDataTableToProductList(pbll.Search(UrunSearchtxt.Text)));
            }
            else
            {
                UrundatagridFill(ConvertDataTableToProductList(pbll.ReadAll()));

            }
            
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            UrunSearchtxt.Enabled = true;
            Enable_controls(false);
            Cart_Txt_clear();
            paneldata.Enabled = true;          

        }

        private void urunadet_TextChanged(object sender, EventArgs e)
        {

        }

        private void UrunSearchtxt_EnabledChanged(object sender, EventArgs e)
        {
            if (UrunSearchtxt.Enabled== false)
            {
                UrundatagridFill(ConvertDataTableToProductList(pbll.ReadAll()));
            }
        }
        void fiyat_kendim_belirleyeceğım_checkbox(bool b)
        {
              fiyattxt.Enabled = b;
              radioButton100adet.Enabled = b;
              radioButton1adet.Enabled = b;
            //comboBoxkar.Enabled= b;
            //comboBoxiskonto.Enabled= b;
        }
        private void checkBoxfiyat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxfiyat.Checked)
            {
                fiyat_kendim_belirleyeceğım_checkbox(true);
            }
            else
            {
                fiyat_kendim_belirleyeceğım_checkbox(false);
            }
        }

        private void radioButton100adet_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton100adet.Checked)
            {
                comboBoxiskonto.Enabled = true;
                comboBoxkar.Enabled = true;

            }
            else
            {
                comboBoxiskonto.Enabled = false;
                comboBoxkar.Enabled = false;
            }
            
        }

        private void radioButton1adet_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1adet.Checked)
            {
                comboBoxiskonto.Enabled = true;
                comboBoxkar.Enabled = true;

            }
            else
            {
                comboBoxiskonto.Enabled = false;
                comboBoxkar.Enabled = false;
            }
        }
        İNVOCE_BLL ibll = new İNVOCE_BLL();
        void FillDatsgrid()
        {
           
               
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = PRODUCTs;
            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["DeletStatus"].Visible = false;
            dataGridView2.Columns["Feature"].Visible = false;
            dataGridView2.Columns["Product_cod"].Visible = false;
            dataGridView2.Columns["Packing"].Visible = false;
            dataGridView2.Columns["Stock"].Visible = false;
            dataGridView2.Columns["DINnumber"].Visible = false;
            dataGridView2.Columns["BrandName"].Visible = false;
            dataGridView2.Columns["picture"].Visible = false;
            dataGridView2.Columns["Category"].Visible = false;
            dataGridView2.Columns["Name"].HeaderText = "Ürün";
            dataGridView2.Columns["Cap"].HeaderText = "Çap";
            dataGridView2.Columns["Boy"].HeaderText = "Boy";
            dataGridView2.Columns["Quality"].HeaderText = "Kalite";
            dataGridView2.Columns["Price"].HeaderText = "Fiyat/AD";
            dataGridView2.Columns["Kaplama"].HeaderText = "Kaplama";
            dataGridView2.Columns["SaledPices"].HeaderText = "Adet";


        }
        private void ürün_eklebutonu_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (urunadet.Text == string.Empty || UrunSearchtxt.Text == string.Empty || fiyattxt.Text == string.Empty )
                {
                    mesage.Show("Uyarı", "Tüm bilgileri girdiğinizden emin olun", "", Button.ok, Logo.warning);
                    return;
                }
                else
                {
                    Hesaplama();
                    if (!errorr)
                    {
                        
                        buttonsiparişoluştur.Enabled = true;
                        checkBoxödeme.Enabled = true;
                        PRODUCTs.Add(product);
                       foreach (var product in PRODUCTs)
                       {
                        // Satılan miktar ve toplam geliri güncelle
                        product.SaledPices += Convert.ToInt32(urunadet.Text);
                        product.Price = +Fiyat;
                       }
                        urunadet.Text = "";
                        paneldata.Enabled = true;
                        UrunSearchtxt.Enabled = true;
                        FillDatsgrid();
                   
                    urunadet.Enabled = false;
                    checkBoxfiyat.Enabled = false;
                    fiyattxt.Enabled = false;
                    radioButton100adet.Enabled = false;
                    radioButton1adet.Enabled = false;
                    comboBoxiskonto.Enabled = false;
                    comboBoxkar.Enabled = false;
                    checkBoxödeme.Enabled = false;
                    comboBoxödemeşekli.Enabled = false;
                    dateTimePickervade.Enabled = false;
                    ödemetutartxt.Enabled = false;
                    buttonsiparişoluştur.Enabled = false;
                    button2.Enabled = false;
                    ürün_eklebutonu.Enabled = false;

                    Cart_Txt_clear();
                        buttonsiparişoluştur.Enabled = true;
                        checkBoxödeme.Enabled =true;
                    UrundatagridFill(ConvertDataTableToProductList(pbll.ReadAll()));
                }
                    else
                    {
                        errorr = false;
                        return;
                    }
                }
                errorr = false;

               
                UrunSearchtxt.Text = "";
                checkBoxfiyat.Checked = false;
                fiyattxt.Text = "";
                radioButton100adet.Checked = false;
                radioButton1adet.Checked = false;
                label34.Text = "........";
            //}
            //catch (Exception x)
            //{

            //    mesage.Show("Uyarı", "İşlem Sırasında Bir sorun Oluştu!", x.Message, Button.ok, Logo.warning);
            //}
            
        }
        #region varibale
        int idselect;
        bool errorr = false;
        int uid;
        

        double ürünadeti;

        double Fiyat;

        double adetxfiyat;

        double toplamtutar = 0;

        double kdv;

        double totalfinal = 0;

        double total;

        double kdvlifiyat = 0;

        double karsızfiyat;
        int idinv;

        #endregion

        void Hesaplama()
        {
            if (!errorr)
            {
                Fiyat = product.Price;
                label34.Text = Fiyat.ToString("N2");
                if (checkBoxfiyat.Checked)
                {
                    Fiyat_hesaplama_formul();
                  
                }
                label34.Text = Fiyat.ToString("N2");
                ürünadeti = Convert.ToDouble(urunadet.Text);

                adetxfiyat = ürünadeti * Fiyat;

                toplamtutar = adetxfiyat + toplamtutar;

                kdv = ((adetxfiyat / 100) * 20);

                kdvlifiyat = kdv + kdvlifiyat;

                total = toplamtutar + kdvlifiyat;
                toplamtutarlbl.Text = toplamtutar.ToString("N2");
                kdvlbl.Text = kdvlifiyat.ToString("N2");
                tutarlbl.Text = total.ToString("N2");
                
            }
            else
            {
                return;
            }


        }
       
        void Fiyat_hesaplama_formul()
        {
            if (radioButton100adet.Checked)
            {
                if (fiyattxt.Text != string.Empty)
                {
                    if (comboBoxiskonto.Text == string.Empty)
                    {
                        if (comboBoxkar.Text == string.Empty)
                        {
                            Fiyat = Convert.ToDouble(fiyattxt.Text) / 100;
                        }
                        else if (comboBoxkar.Text != string.Empty)
                        {
                            Fiyat = (((Convert.ToDouble(fiyattxt.Text) / 100) / 100) * Convert.ToDouble(comboBoxkar.Text)) + (Convert.ToDouble(fiyattxt.Text) / 100);
                        }
                    }
                    else
                    {
                        if (comboBoxkar.Text == string.Empty)
                        {
                            Fiyat = (Convert.ToDouble(fiyattxt.Text) / 100) - (((Convert.ToDouble(fiyattxt.Text) / 100) / 100) * Convert.ToDouble(comboBoxiskonto.Text));
                        }
                        else if (comboBoxkar.Text != string.Empty)
                        {
                            karsızfiyat = ((Convert.ToDouble(fiyattxt.Text) / 100) - (((Convert.ToDouble(fiyattxt.Text) / 100) / 100) * Convert.ToDouble(comboBoxiskonto.Text)));
                            Fiyat = ((karsızfiyat / 100) * Convert.ToDouble(comboBoxkar.Text)) + karsızfiyat;
                        }
                    }
                }
                else
                {
                    mesage.Show("Dikkat", " Lütfen Liste fiyatını yazın ", "", Button.ok, Logo.warning);
                    errorr = true;
                    return;
                }

            }
            else if (radioButton1adet.Checked)
            {
                if (fiyattxt.Text != string.Empty)
                {
                    if (comboBoxiskonto.Text == string.Empty)
                    {
                        if (comboBoxkar.Text == string.Empty)
                        {
                            Fiyat = Convert.ToDouble(fiyattxt.Text);
                        }
                        else if (comboBoxkar.Text != string.Empty)
                        {
                            Fiyat = (((Convert.ToDouble(fiyattxt.Text) / 100)) * Convert.ToDouble(comboBoxkar.Text)) + (Convert.ToDouble(fiyattxt.Text));
                        }
                    }
                    else
                    {
                        if (comboBoxkar.Text == string.Empty)
                        {
                            Fiyat = (Convert.ToDouble(fiyattxt.Text)) - (((Convert.ToDouble(fiyattxt.Text) / 100)) * Convert.ToDouble(comboBoxiskonto.Text));
                        }
                        else if (comboBoxkar.Text != string.Empty)
                        {
                            karsızfiyat = (Convert.ToDouble(fiyattxt.Text)) - (((Convert.ToDouble(fiyattxt.Text) / 100)) * Convert.ToDouble(comboBoxiskonto.Text));
                            Fiyat = ((karsızfiyat / 100) * Convert.ToDouble(comboBoxkar.Text)) + karsızfiyat;
                        }
                    }
                }
                else
                {
                    mesage.Show("Dikkat", " Lütfen Liste fiyatını yazın ", "", Button.ok, Logo.warning);
                    errorr = true;
                    return;
                }
            }
            else
            {
                mesage.Show("Dikkat", " /100 Adet Fİyatı mı  /1 Adet Fiyatı mı Belirtin ! ", "", Button.ok, Logo.warning);
                errorr = true;
                return;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBoxödeme_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxödeme.Checked)
            {
                comboBoxödemeşekli.Enabled = true;
                dateTimePickervade.Enabled = true;
                ödemetutartxt.Enabled = true;
            }
            else
            {
                comboBoxödemeşekli.Enabled = false;
                dateTimePickervade.Enabled = false;
                ödemetutartxt.Enabled = false;
            }
        }

        private void buttonsiparişoluştur_Click(object sender, EventArgs e)
        {
            if (ubll.Access(userlogin, "Satiş Bölümü", can.Create))
            {
                try
                {
                    İNVOİCE i = new İNVOİCE();
                    i.TotalPrice = Convert.ToDouble(tutarlbl.Text);
                    cbll.CreateBakiye(ChoseCudtomer, Convert.ToDouble(tutarlbl.Text));
                    i.RegDate = DateTime.Now.Date;
                    if (checkBoxödeme.Checked)
                    {
                        cbll.Create_payment(ChoseCudtomer, Convert.ToDouble(ödemetutartxt.Text));
                        i.İsCheckedOut = true;
                        i.CeackOutDate = DateTime.Now.Date;
                        PAYMENT_METHOD seçilenödeme = (PAYMENT_METHOD)Enum.Parse(typeof(PAYMENT_METHOD), comboBoxödemeşekli.SelectedItem.ToString());
                        i.ödemeŞekli = seçilenödeme;
                        i.VadeDate = dateTimePickervade.MinDate;
                        i.ÖdemeDate = DateTime.Now.Date;
                        i.ÖdemeTurarı = Convert.ToDouble(ödemetutartxt.Text);
                        i.Bakiye = i.TotalPrice - i.ÖdemeTurarı;
                    }
                    else
                    {
                        i.İsCheckedOut = false;
                    }


                    DialogResult res = mesage.Show("Kayıt", "Fişden Çıktı Almak istermısın?", ibll.Create(i, ChoseCudtomer, PRODUCTs, Chose_Customer_user), Button.yesorno, Logo.info);
                    if (res == DialogResult.Yes)
                    {
                         StiReport sti = new StiReport();
                        sti.Load(AppDomain.CurrentDomain.BaseDirectory+ "İnvoice.mrt");
                        sti.Dictionary.Variables["Company"].Value = "ÇAĞDAŞ CIVATA BAĞ.ELM.TEK.HIR.SAN.LTD.ŞTİ";
                        sti.Dictionary.Variables["Adress"].Value = "BİRLİK SAN SİT 2 CAD NO 41 \n BEYLİKDÜZÜODB MAH / BEYLİKDÜZÜ / İSTANBUL";
                        sti.Dictionary.Variables["NameUser"].Value = userlogin.Name ;
                        sti.Dictionary.Variables["Phone"].Value = userlogin.PhoneNumber;
                        sti.Dictionary.Variables["E-mail"].Value = ChoseCudtomer.Email;
                        sti.Dictionary.Variables["CustomerCompany"].Value = ChoseCudtomer.Company;
                        sti.Dictionary.Variables["CustomerAdress"].Value =ChoseCudtomer.Adress;
                        sti.Dictionary.Variables["CustomerName"].Value = ChoseCudtomer.Name;
                        sti.Dictionary.Variables["CustomerPhone"].Value = ChoseCudtomer.Phone;
                        sti.Dictionary.Variables["CustomerE-mail"].Value = ChoseCudtomer.Adress;
                        sti.Dictionary.Variables["İnvoiceNumber"].Value =ibll.İnvoicenum();
                        sti.Dictionary.Variables["Date"].Value = DateTime.Now.Date.ToString("dd,MM,yyyy") ;
                        sti.RegBusinessObject("PRODUCT",PRODUCTs ) ;
                        sti.Render();
                        sti.Show();

                    }
                    urunadet.Enabled = false;
                    checkBoxfiyat.Enabled = false;
                    fiyattxt.Enabled = false;
                    radioButton100adet.Enabled = false;
                    radioButton1adet.Enabled = false;
                    comboBoxiskonto.Enabled = false;
                    comboBoxkar.Enabled = false;
                    checkBoxödeme.Enabled = false;
                    comboBoxödemeşekli.Enabled = false;
                    dateTimePickervade.Enabled = false;
                    ödemetutartxt.Enabled = false;
                    buttonsiparişoluştur.Enabled = false;
                    button2.Enabled = false;
                    ürün_eklebutonu.Enabled = false;
                    Cart_Txt_clear();
                    toplamtutarlbl.Text = "";
                    kdvlbl.Text = "";
                    tutarlbl.Text = "";
                    checkBoxödeme.Checked = false;
                    comboBoxödemeşekli.Text = "";
                    ödemetutartxt.Text = "";
                    PRODUCTs.Clear();
                    dataGridView2.DataSource = null;
                    product = null;
                    paneldata.Enabled = true;
                    UrunSearchtxt.Enabled = true;
                }
                catch (Exception a)
                {
                    mesage.Show("Bilgi", "Sipariş oluşturur ken bir sorun oluştu!!", a.Message, Button.ok, Logo.warning);
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ibll.readall();
                dataGridView1.Columns["id"].Visible = false;
            }
            else
            {
                mesage.Show("Uyarı", "Buna Yetkiniz yok. ", "Yetkili ile Görüşün.", Button.ok, Logo.warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
            object i = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id"].Value;
            if (i != null)
            {
                idinv = Convert.ToInt32 (i);
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == mesage.Show("Sil","Eminmısın?","",Button.yesorno,Logo.warning))
            {
                ibll.Delete(idinv);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ibll.readall();
                dataGridView1.Columns["id"].Visible = false;
            }
        }
        
        private void ödemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PAMENT_FORM P = new PAMENT_FORM();    
            
            P.ShowDialog();
          
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ibll.SearchCustomer(textBox8.Text);
            dataGridView1.Columns["id"].Visible = false;
        }

        private void çıktıAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            İNVOİCE i = ibll.read_by_id(idinv);
            CUSTOMER c = ibll.Get_customer_Bay_invoice_İd(i.id);
            USER u = ubll.Read_By_İd (cbll.GetUserIdformcustumer(c.id));
            //List<PRODUCT> p = ibll.Get_İnvoice_Product(i.id);

           
            //if (i != null)
            //{
            //    StiReport sti = new StiReport();
            //    sti.Load(AppDomain.CurrentDomain.BaseDirectory + "İnvoice.mrt");
            //    sti.Dictionary.Variables["Company"].Value = "ÇAĞDAŞ CIVATA BAĞ.ELM.TEK.HIR.SAN.LTD.ŞTİ";
            //    sti.Dictionary.Variables["Adress"].Value = "BİRLİK SAN SİT 2 CAD NO 41 \n BEYLİKDÜZÜODB MAH / BEYLİKDÜZÜ / İSTANBUL";
            //    sti.Dictionary.Variables["NameUser"].Value = u.Name;
            //    sti.Dictionary.Variables["Phone"].Value = u.PhoneNumber;
            //    sti.Dictionary.Variables["E-mail"].Value = c.Email;
            //    sti.Dictionary.Variables["CustomerCompany"].Value = c.Company;
            //    sti.Dictionary.Variables["CustomerAdress"].Value = c.Adress;
            //    sti.Dictionary.Variables["CustomerName"].Value = c.Name;
            //    sti.Dictionary.Variables["CustomerPhone"].Value = c.Phone;
            //    sti.Dictionary.Variables["CustomerE-mail"].Value = c.Adress;
            //    sti.Dictionary.Variables["İnvoiceNumber"].Value = i.invoiceNumber;
            //    sti.Dictionary.Variables["Date"].Value = i.RegDate.ToString("dd,MM,yyyy");
            //    sti.RegBusinessObject("PRODUCT", p);
            //    sti.Render();
            //    sti.Show();

            //}
        }
    }
}
