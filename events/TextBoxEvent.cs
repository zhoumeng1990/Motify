using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MotifyPackage.events
{
    class TextBoxEvent
    {
        private readonly TextBox textBox;

        public TextBoxEvent(TextBox textBox)
        {
            this.textBox = textBox;
        }

        public void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            string fname = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            textBox.Text = fname;
        }
    }
}
