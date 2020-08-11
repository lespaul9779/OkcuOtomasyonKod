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
    public partial class Calisanlar : DevExpress.XtraEditors.XtraForm
    {
        public Calisanlar()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;
        void listele()
        {
            sql = @"Select * from calisanlar";
            liste = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn.baglan());
            da.Fill(liste);
            gridControl1.DataSource = liste;
        }
        private void Calisanlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["id"].ToString();
            txtad.Text = dr["ad"].ToString();
            txtsoyad.Text = dr["soyad"].ToString();
            msktel.Text = dr["tel"].ToString();
            txtgorev.Text = dr["gorev"].ToString();
            msktc.Text = dr["tc"].ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.baglan();
                sql = @"insert into calisanlar(ad,soyad,tc,tel,gorev) values (@p1,@p2,@p3,@p4,@p5)";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", txtad.Text);
                sorgu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                sorgu.Parameters.AddWithValue("@p3", msktc.Text);
                sorgu.Parameters.AddWithValue("@p4", msktel.Text);
                sorgu.Parameters.AddWithValue("@p5", txtgorev.Text);
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Çalışan Eklendi.");
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
                sql = @"update calisanlar set ad=@p1,soyad=@p2,tc=@p3,tel=@p4,gorev=@p5 where id=@p6";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", txtad.Text);
                sorgu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                sorgu.Parameters.AddWithValue("@p3", msktc.Text);
                sorgu.Parameters.AddWithValue("@p4", msktel.Text);
                sorgu.Parameters.AddWithValue("@p5", txtgorev.Text);
                sorgu.Parameters.AddWithValue("@p6", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Çalışan Kaydı Güncellendi.");
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
                sql = @"delete from calisanlar where id=@p1";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Çalışan Kaydı Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                sql = @"Select * from calisanlar where id ='" + txtid.Text + "'";
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