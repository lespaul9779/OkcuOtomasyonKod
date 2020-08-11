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
    public partial class Sahiplik : DevExpress.XtraEditors.XtraForm
    {
        public Sahiplik()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;
        void listele()
        {
            sql = @"Select * from sahiplik";
            liste = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn.baglan());
            da.Fill(liste);
            gridControl1.DataSource = liste;
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
        void yay()
        {
            sql = @"Select yayad from yay";
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                txtyay.Properties.Items.Add(dr[0]);
            }
            conn.baglan().Close();
        }
        void ok()
        {
            sql = @"Select okad from ok";
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                txtok.Properties.Items.Add(dr[0]);
            }
            conn.baglan().Close();
        }
        private void Sahiplik_Load(object sender, EventArgs e)
        {
            listele();
            ogrenciler();
            yay();
            ok();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["id"].ToString();
            cmbad.Text = dr["ad"].ToString();
            txtsoyad.Text = dr["soyad"].ToString();
            txtyay.Text = dr["malzeme1"].ToString();
            txtok.Text = dr["malzeme2"].ToString();
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
            }
            conn.baglan().Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.baglan();
                sql = @"insert into sahiplik(ad,soyad,malzeme1,malzeme2) values (@p1,@p2,@p3,@p4)";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", cmbad.Text);
                sorgu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                sorgu.Parameters.AddWithValue("@p3", txtyay.Text);
                sorgu.Parameters.AddWithValue("@p4", txtok.Text);
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Sahiplilk Bilgisi Eklendi.");
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
                sql = @"update sahiplik set ad=@p1,soyad=@p2,malzeme1=@p3,malzeme2=@p4 where id=@p5";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", cmbad.Text);
                sorgu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                sorgu.Parameters.AddWithValue("@p3", txtyay.Text);
                sorgu.Parameters.AddWithValue("@p4", txtok.Text);
                sorgu.Parameters.AddWithValue("@p5", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Sahiplik Kaydı Güncellendi.");
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
                sql = @"delete from sahiplik where id=@p1";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Sahiplik Kaydı Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}