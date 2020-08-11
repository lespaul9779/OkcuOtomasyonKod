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
    public partial class OgrenciKayit : DevExpress.XtraEditors.XtraForm
    {
        public OgrenciKayit()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;
        void listele()
        {
            sql = @"Select * from ogrencikayit";
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
        private void OgrenciKayit_Load(object sender, EventArgs e)
        {
            listele();
            sehirLer();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["id"].ToString();
            txtad.Text = dr["ad"].ToString();
            txtsoyad.Text = dr["soyad"].ToString();
            mskdt.Text = dr["dogumtarih"].ToString();
            cmbil.Text = dr["il"].ToString();
            cmbilce.Text = dr["ilce"].ToString();
            msktc.Text = dr["tc"].ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.baglan();
                sql = @"insert into ogrencikayit(ad,soyad,dogumtarih,il,ilce,tc) values (@p1,@p2,@p3,@p4,@p5,@p6)";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", txtad.Text);
                sorgu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                sorgu.Parameters.AddWithValue("@p3", mskdt.Text);
                sorgu.Parameters.AddWithValue("@p4", cmbil.Text);
                sorgu.Parameters.AddWithValue("@p5", cmbilce.Text);
                sorgu.Parameters.AddWithValue("@p6", msktc.Text);
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Öğrenci Eklendi.");
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
                sql = @"update ogrencikayit set ad=@p1,soyad=@p2,dogumtarih=@p3,il=@p4,ilce=@p5,tc=@p6 where id=@p7";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", txtad.Text);
                sorgu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                sorgu.Parameters.AddWithValue("@p3", mskdt.Text);
                sorgu.Parameters.AddWithValue("@p4", cmbil.Text);
                sorgu.Parameters.AddWithValue("@p5", cmbilce.Text);
                sorgu.Parameters.AddWithValue("@p6", msktc.Text);
                sorgu.Parameters.AddWithValue("@p7", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Öğrenci Kaydı Güncellendi.");
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
                sql = @"delete from ogrencikayit where id=@p1";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Öğrenci Kaydı Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                sql = @"Select * from ogrencikayit where id ='" + txtid.Text + "'";
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

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            sql = @"select kasabaad from kasaba where  sehirid=@p1";
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            sorgu.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            conn.baglan().Close();
        }
    }
}