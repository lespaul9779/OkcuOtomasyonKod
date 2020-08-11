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
    public partial class KullGiris : DevExpress.XtraEditors.XtraForm
    {
        public KullGiris()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private void KullGiris_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sql = @"select * from kullanici where kulad=@p1 and sifre=@p2";
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            sorgu.Parameters.AddWithValue("@p1", txtkulad.Text);
            sorgu.Parameters.AddWithValue("@p2", int.Parse(txtsifre.Text));
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            if (dr.Read())
            {
                menu menu = new menu();
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Yanlış!!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.baglan().Close();
        }
    }
}