using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace okcuotomasyon
{
    public partial class menu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public menu()
        {
            InitializeComponent();
        }
        OgrenciKayit ogr;
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ogr == null || ogr.IsDisposed)
            {
                ogr = new OgrenciKayit();
                ogr.MdiParent = this;
                ogr.Show();
            }
        }
        lisans ls;
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ls == null || ls.IsDisposed)
            {
                ls = new lisans();
                ls.MdiParent = this;
                ls.Show();
            }
        }
        YayKayit yk;
        private void yay_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (yk == null || yk.IsDisposed)
            {
                yk = new YayKayit();
                yk.MdiParent = this;
                yk.Show();
            }
        }
        OkKayit ok;
        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ok == null || ok.IsDisposed)
            {
                ok = new OkKayit();
                ok.MdiParent = this;
                ok.Show();
            }
        }
        HedefKayit hk;
        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (hk == null || hk.IsDisposed)
            {
                hk = new HedefKayit();
                hk.MdiParent = this;
                hk.Show();
            }
        }
        Sahiplik sh;
        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (sh == null || sh.IsDisposed)
            {
                sh = new Sahiplik();
                sh.MdiParent = this;
                sh.Show();
            }
        }
        Calisanlar cl;
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (cl == null || cl.IsDisposed)
            {
                cl = new Calisanlar();
                cl.MdiParent = this;
                cl.Show();
            }
        }
        odemeler od;
        private void odemeler_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (od == null || od.IsDisposed)
            {
                od = new odemeler();
                od.MdiParent = this;
                od.Show();
            }
        }
        OgrenciBilgi ob;
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ob == null || ob.IsDisposed)
            {
                ob = new OgrenciBilgi();
                ob.MdiParent = this;
                ob.Show();
            }
        }
        OgrenciUcret ogu;
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ogu == null || ogu.IsDisposed)
            {
                ogu = new OgrenciUcret();
                ogu.MdiParent = this;
                ogu.Show();
            }
        }
        UcretTakip ut;
        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ut == null || ut.IsDisposed)
            {
                ut = new UcretTakip();
                ut.MdiParent = this;
                ut.Show();
            }
        }
        Depo dp;
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (dp == null || dp.IsDisposed)
            {
                dp = new Depo();
                dp.MdiParent = this;
                dp.Show();
            }
        }
        Kasa ks;
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ks == null || ks.IsDisposed)
            {
                ks = new Kasa();
                ks.MdiParent = this;
                ks.Show();
            }
        }
    }
}