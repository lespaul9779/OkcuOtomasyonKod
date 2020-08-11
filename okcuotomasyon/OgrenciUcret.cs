using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Npgsql;

namespace okcuotomasyon
{
    public partial class OgrenciUcret : DevExpress.XtraEditors.XtraForm
    {
        public OgrenciUcret()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;
        void listele()
        {
            sql = @"select * from ogrucret";
            liste = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn.baglan());
            da.Fill(liste);
            gridControl1.DataSource = liste;
        }
        void ogrenciler()
        {
            sql = @"Select id,ad,soyad  from ogrencikayit";
            liste = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql,conn.baglan());
            da.Fill(liste);
            lkpogrenci.Properties.ValueMember = "id";
            lkpogrenci.Properties.DisplayMember = "ad";
            lkpogrenci.Properties.DisplayMember = "soyad";
            lkpogrenci.Properties.DataSource = liste;
            conn.baglan().Close();
        }
        private void OgrenciUcret_Load(object sender, EventArgs e)
        {
            listele();
            ogrenciler();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["ogrenciid"].ToString();
            msktarih.Text = dr["kayittarih"].ToString();
            cmbay.Text = dr["ay"].ToString();
            mskyil.Text = dr["yil"].ToString();
            txtucret.Text = dr["ucret"].ToString();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            sql = @"Select id from ogrencikayit where soyad=@p1";
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            sorgu.Parameters.AddWithValue("@p1", lkpogrenci.Text);
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                txtid.Text = dr["id"].ToString();
            }
            conn.baglan().Close();
        }

        private void lkpogrenci_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.baglan();
                sql = @"insert into ogrenciucret(ogrenciid,kayittarih,ucret,ay,yil) values (@p1,@p2,@p3,@p4,@p5)";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                sorgu.Parameters.AddWithValue("@p2", msktarih.Text);
                sorgu.Parameters.AddWithValue("@p3", decimal.Parse(txtucret.Text));
                sorgu.Parameters.AddWithValue("@p4", cmbay.Text);
                sorgu.Parameters.AddWithValue("@p5", mskyil.Text);
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Ücret Eklendi.");
            }
            catch (Exception)
            {

                MessageBox.Show("Lütfen Tüm Alanları Doldurduğunuzdan Emin Olun !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            try
            {
                conn.baglan();
                sql = @"update ogrenciucret set kayittarih=@p1,ucret=@p2,ay=@p3,yil=@p4 where id=@p5";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", msktarih.Text);
                sorgu.Parameters.AddWithValue("@p2", decimal.Parse(txtucret.Text));
                sorgu.Parameters.AddWithValue("@p3", cmbay.Text);
                sorgu.Parameters.AddWithValue("@p4", mskyil.Text);
                sorgu.Parameters.AddWithValue("@p5", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Ücret Kaydı Güncellendi.");
            }
            catch (Exception)
            {
                MessageBox.Show("ID Alanını Girdiğinizden Emin Olun !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Eminmisiniz!!!", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == DialogResult.Yes)
            {
                conn.baglan();
                sql = @"delete from ogrenciucret where ogrenciid=@p1";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Ücret Kaydı Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                sql = @"Select * from ogrenciucret where id ='" + txtid.Text + "'";
                liste = new DataTable();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn.baglan());
                da.Fill(liste);
                gridControl1.DataSource = liste;
                conn.baglan().Close();
            }
            catch (Exception)
            {

                MessageBox.Show("ID Alanını Girdiğinizden Emin Olun !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}