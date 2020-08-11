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
    public partial class lisans : DevExpress.XtraEditors.XtraForm
    {
        public lisans()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;
        void listele()
        {
            sql = @"Select * from lisans";
            liste = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn.baglan());
            da.Fill(liste);
            gridControl1.DataSource = liste;
        }
        void sehirLer()
        {
            sql = @"Select sehirad from sehir";
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
            }
            conn.baglan().Close();
        }
        void ogrenciler()
        {
            sql = @"Select ad from ogrencikayit";
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                cmbad.Properties.Items.Add(dr[0]);
            }
            conn.baglan().Close();
        }
        private void lisans_Load(object sender, EventArgs e)
        {
            listele();
            sehirLer();
            ogrenciler();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["ogrenciid"].ToString();
            cmbad.Text = dr["ad"].ToString();
            txtsoyad.Text = dr["soyad"].ToString();
            mskdt.Text = dr["dogumtarih"].ToString();
            cmbil.Text = dr["il"].ToString();
            txtbabaad.Text = dr["babaad"].ToString();
            txtannead.Text = dr["annead"].ToString();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            sql = @"Select id, soyad ,dogumtarih ,il from ogrencikayit where ad=@p1";
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            sorgu.Parameters.AddWithValue("@p1", cmbad.Text);
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                txtid.Text = dr["id"].ToString();
                txtsoyad.Text = dr["soyad"].ToString();
                mskdt.Text = dr["dogumtarih"].ToString();
                cmbil.Text = dr["il"].ToString();
            }
            conn.baglan().Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.baglan();
                sql = @"insert into lisans(ad,soyad,dogumtarih,il,babaad,annead,ogrenciid) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", cmbad.Text);
                sorgu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                sorgu.Parameters.AddWithValue("@p3", mskdt.Text);
                sorgu.Parameters.AddWithValue("@p4", cmbil.Text);
                sorgu.Parameters.AddWithValue("@p5", txtbabaad.Text);
                sorgu.Parameters.AddWithValue("@p6", txtannead.Text);
                sorgu.Parameters.AddWithValue("@p7", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Lisans Bilgisi Eklendi.");
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
                sql = @"update lisans set ad=@p1,soyad=@p2,dogumtarih=@p3,il=@p4,babaad=@p5,annead=@p6 where ogrenciid=@p7";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", cmbad.Text);
                sorgu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                sorgu.Parameters.AddWithValue("@p3", mskdt.Text);
                sorgu.Parameters.AddWithValue("@p4", cmbil.Text);
                sorgu.Parameters.AddWithValue("@p5", txtbabaad.Text);
                sorgu.Parameters.AddWithValue("@p6", txtannead.Text);
                sorgu.Parameters.AddWithValue("@p7", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Lisans Kaydı Güncellendi.");
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
                sql = @"delete from lisans where id=@p1";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Lisans Kaydı Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}