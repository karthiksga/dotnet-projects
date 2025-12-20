using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MsmqApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int i = 0;
        MsmqServ.OrderProcessorClient orderClient=new  MsmqServ.OrderProcessorClient();

        private void button1_Click(object sender, EventArgs e)
        {
            i += 1;
            orderClient.SubmitOrder(i);
        }
    }
}
