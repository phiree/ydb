using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dianzhu.Model;
using Dianzhu.CSClient.IView;

namespace Dianzhu.CSClient.WinformView
{
    public partial class UC_SearchResult : UserControl,IView.IViewSearchResult
    {
        public UC_SearchResult()
        {
            InitializeComponent();
        }

        IList<DZService> searchedService;

        public event SelectService SelectService;

        public IList<DZService> SearchedService
        {
            get
            {
                return searchedService;
            }
            set
            {
                searchedService = value;
                pnlSearchResult.Controls.Clear();
                //foreach (DZService service in searchedService)
                //{
                //    LoadServiceToPanel(service);
                //}Hashtable ht = (Hashtable)list[i];
                foreach (DZService service in searchedService)
                {
                    LoadServiceToPanel(service);
                }
            }
        }
        private void LoadServiceToPanel(DZService service)
        {
            FlowLayoutPanel pnl = new FlowLayoutPanel();
            pnl.Name = "servicePnl" + service.Id.ToString();
            pnl.BorderStyle = BorderStyle.FixedSingle;
            pnl.FlowDirection = FlowDirection.LeftToRight;
            Label lblBusinessName = new Label();
            lblBusinessName.BorderStyle = BorderStyle.FixedSingle;
            lblBusinessName.Text = service.Business.Name;
            lblBusinessName.Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
            pnl.Controls.Add(lblBusinessName);
            Label lblServiceName = new Label();
            lblServiceName.BorderStyle = BorderStyle.FixedSingle;
            lblServiceName.Text = service.Description.ToString();
            pnl.Controls.Add(lblServiceName);
            
            Button btnSelectService = new Button();
            btnSelectService.Text = "选择";
            btnSelectService.Tag = service;// service.Id.ToString();
            btnSelectService.Click += new EventHandler(btnSelectService_Click);
            pnl.Controls.Add(btnSelectService);
            pnlSearchResult.Controls.Add(pnl);

        }
        void btnSelectService_Click(object sender, EventArgs e)
        {
            //将已经选择的 panel 背景颜色还原
            foreach (Control con in pnlSearchResult.Controls)
            {
                if (con.BackColor == Color.Green)
                {
                    con.BackColor = DefaultBackColor;
                    break;
                }
            }
            DZService selectedService = (DZService)((Button)sender).Tag;

            Panel pnl = (Panel)pnlSearchResult.Controls.Find("servicePnl" + selectedService.Id, true)[0];
            pnl.BackColor = Color.Green;
            SelectService(selectedService);
          
        }
    }
}
