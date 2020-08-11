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
    public partial class HedefKayit : DevExpress.XtraEditors.XtraForm
    {
        public HedefKayit()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;
        void listele()
        {
            sql = @"Select * from hedef";
            liste = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn.baglan());
            da.Fill(liste);
            gridControl1.DataSource = liste;
        }
        private void HedefKayit_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["id"].ToString();
            txtad.Text = dr["hedefad"].ToString();
            txtadet.Text = dr["adet"].ToString();
            txtboyut.Text = dr["boyut"].ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.baglan();
                sql = @"insert into hedef(hedefad,adet,boyut) values (@p1,@p2,@p3)";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", txtad.Text);
                sorgu.Parameters.AddWithValue("@p2", int.Parse(txtadet.Text));
                sorgu.Parameters.AddWithValue("@p3", txtboyut.Text);
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Hedef Eklendi.");
            }
            catch (Exception)
            {

                MessageBox.Show("Lütfen Tüm Alanları Doldurduğunuzdan Emin Olun !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Adet Alanına Sayısal Değer Giriniz !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.baglan();
                sql = @"update hedef set hedefad=@p1,adet=@p2,boyut=@p3 where id=@p4";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", txtad.Text);
                sorgu.Parameters.AddWithValue("@p2", int.Parse(txtadet.Text));
                sorgu.Parameters.AddWithValue("@p3", txtboyut.Text);
                sorgu.Parameters.AddWithValue("@p4", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Hedef Kaydı Güncellendi.");
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
                sql = @"delete from hedef where id=@p1";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Hedef Kaydı Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                sql = @"Select * from hedef where id ='" + txtid.Text + "'";
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