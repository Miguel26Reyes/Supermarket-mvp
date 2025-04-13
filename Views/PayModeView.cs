using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket_mvp.Views
{
    public partial class PayModeView : Form, IPayModeView
    {
        private bool isEdit;
        private bool issuccessful;
        private string message;
        public string PayModeId 
        { 
            get { return TxtPayModeId.Text; } 
            set { TxtPayModeId.Text = value;  }
        
        }
        public string PayModeName 
        { 
            get { return TxtPayModeName.Text; }
            set { TxtPayModeName.Text = value; }
        }
        public string PayModeObservation 
        { 
            get { return TxtPayModeObservation.Text; } 
            set { TxtPayModeObservation.Text = value; }
        }
        public string SearchValue 
        { 
            get { return TxtSearch.Text; }
            set { TxtSearch.Text = value; }
        }
        public bool IsEdit 
        { 
            get { return IsEdit; } 
            set { IsEdit = value; }
        }
        public bool IsSuccessful 
        { 
            get { return IsSuccessful; }
            set { IsSuccessful = value; }
        }
        public string Message 
        { 
            get { return Message; }
            set { Message = value; }
        }

        public PayModeView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();

            tabControl1.TabPages.Remove(tabPagePayModeDetail);

            BtnClose.Click += delegate { this.Close(); };
        }

        private void AssociateAndRaiseViewEvents()
        {
            BtnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };

            TxtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        public void SetPayModeListBidingSource(BindingSource payModeList)
        {
            DgPayMode.DataSource = payModeList;
        }

        private static PayModeView instance;

        public static PayModeView GetInstance(Form parentContainer) 
        {
            if(instance == null || instance.IsDisposed) 
            {
                instance = new PayModeView();
                instance.MdiParent = parentContainer;

                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if(instance.WindowState == FormWindowState.Minimized) 
                {
                    instance.WindowState = FormWindowState.Normal;
                }
                instance.BringToFront();

            }
            return instance;
        }


        
       
    }
}
