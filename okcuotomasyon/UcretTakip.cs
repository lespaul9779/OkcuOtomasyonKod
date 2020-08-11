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
    public partial class UcretTakip : DevExpress.XtraEditors.XtraForm
    {
        public UcretTakip()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string sql;
        private NpgsqlCommand sorgu;
        private DataTable liste;
        void listele()
        {
            sql = @"select * from ucretdurum";
            liste = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn.baglan());
            da.Fill(liste);
            gridControl1.DataSource = liste;
        }
        private void UcretTakip_Load(object sender, EventArgs e)
        {
            listele();
        }
    }
}