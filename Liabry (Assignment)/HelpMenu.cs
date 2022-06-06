using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liabry__Assignment_
{
    public partial class HelpMenu_ : Form
    {
        string formName;
        public HelpMenu_(string formName)
        {
            InitializeComponent();
            this.formName = formName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(formName == "loginForm")
            {
                Library lf = new Library();
                lf.Show();
                this.Hide();
            }else if (formName == "myBooksForm")
            {
                MyBooks mb = new MyBooks();
                mb.Show();
                this.Hide();
            }
        }
    }
}
