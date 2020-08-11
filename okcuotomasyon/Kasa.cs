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
    public partial class Kasa : DevExpress.XtraEditors.XtraForm
    {
        public Kasa()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;

        void gelir()
        {
            sql = @"select * from ucretdurum";
            liste = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn.baglan());
            da.Fill(liste);
            gridControl1.DataSource = liste;
        }
        void gider()
        {
            sql = @"select * from odemetakip";
            liste = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn.baglan());
            da.Fill(liste);
            gridControl2.DataSource = liste;
        }
        private void Kasa_Load(object sender, EventArgs e)
        {
            gelir();
            gider();
            sql = @"select ogrenciucret()";
            liste = new DataTable();
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                lblgelirler.Text = dr[0].ToString();
            }
            conn.baglan().Close();

            sql = @"select odemeler()";
            liste = new DataTable();
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            NpgsqlDataReader dr1 = sorgu.ExecuteReader();
            while (dr1.Read())
            {
                lblgiderler.Text = dr1[0].ToString();
            }
            conn.baglan().Close();
        }
    }
}