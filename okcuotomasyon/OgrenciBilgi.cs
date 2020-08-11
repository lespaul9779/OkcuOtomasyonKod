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
    public partial class OgrenciBilgi : DevExpress.XtraEditors.XtraForm
    {
        public OgrenciBilgi()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;
        void listele()
        {
            sql = @"select * from ogrbilgi";
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
        private void OgrenciBilgi_Load(object sender, EventArgs e)
        {
            listele();
            sehirLer();
        }

        private void txtbaba_TextChanged(object sender, EventArgs e)
        {

        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["id"].ToString();
            txtad.Text = dr["adsoyad"].ToString();
            txtbaba.Text = dr["babaad"].ToString();
            txtanne.Text = dr["annead"].ToString();
            msktel.Text = dr["tel"].ToString();
            cmbil.Text = dr["dogumyeri"].ToString();
            txtmail.Text = dr["mail"].ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.baglan();
                sql = @"update ogrencibilgi set babaad=@p1,annead=@p2,tel=@p3,dogumyeri=@p4,mail=@p5 where id=@p6";
                sorgu = new NpgsqlCommand(sql, conn.baglan());
                sorgu.Parameters.AddWithValue("@p1", txtbaba.Text);
                sorgu.Parameters.AddWithValue("@p2", txtanne.Text);
                sorgu.Parameters.AddWithValue("@p3", msktel.Text);
                sorgu.Parameters.AddWithValue("@p4", cmbil.Text);
                sorgu.Parameters.AddWithValue("@p5", txtmail.Text);
                sorgu.Parameters.AddWithValue("@p6", int.Parse(txtid.Text));
                sorgu.ExecuteNonQuery();
                conn.baglan().Close();
                listele();
                MessageBox.Show("Öğrenci Bilgileri Güncellendi.");
            }
            catch (Exception)
            {
                MessageBox.Show("ID Alanını Girdiğinizden Emin Olun !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                sql = @"Select * from ogrbilgi where id ='" + txtid.Text + "'";
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