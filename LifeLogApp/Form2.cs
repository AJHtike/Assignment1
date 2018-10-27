using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeLogApp
{
    public partial class ChildForm : Form
    {
        private static ChildForm aForm = null;
        //public static ChildForm Instance()
        //{
        //    if (aForm == null)
        //    {
        //        aForm = new ChildForm();
        //    }
        //    return aForm;
        //}

        public ChildForm()
        {
            InitializeComponent();
            Console.WriteLine("A child form is born");
    
        }

        public ChildForm(string id, string txt, string date)
        {
            InitializeComponent();
            List<string> vs = new List<string>();
            vs.Add(id);
            vs.Add(txt);
            vs.Add(date);

            foreach (var obj in vs)
            {
                listBox1.Items.Add(obj);
            }


        }

        private void EventListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nothing to implement
        }

        private void ChildForm_Load(object sender, EventArgs e)
        {
            //nothing to implement

        }

    }
}
