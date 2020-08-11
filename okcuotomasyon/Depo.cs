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
    public partial class Depo : DevExpress.XtraEditors.XtraForm
    {
        public Depo()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;
        private void Depo_Load(object sender, EventArgs e)
        {

            sql = @"select yaydepo()";
            liste = new DataTable();
            sorgu = new NpgsqlCommand(sql,conn.baglan());
            NpgsqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                txtyay.Text = dr[0].ToString();
            }
            conn.baglan().Close();

            sql = @"select okdepo()";
            liste = new DataTable();
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            NpgsqlDataReader dr1 = sorgu.ExecuteReader();
            while (dr1.Read())
            {
                txtok.Text = dr1[0].ToString();
            }
            conn.baglan().Close();

            sql = @"select hedefdepo()";
            liste = new DataTable();
            sorgu = new NpgsqlCommand(sql, conn.baglan());
            NpgsqlDataReader dr2 = sorgu.ExecuteReader();
            while (dr2.Read())
            {
                txthedef.Text = dr2[0].ToString();
            }
            conn.baglan().Close();

        }
    }
}