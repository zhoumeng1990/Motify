using MotifyPackage.control;
using MotifyPackage.entify;
using MotifyPackage.events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MotifyPackage
{
    public partial class Main : Form
    {

        private readonly MainControl mainControl;
        private readonly MainEntity mainEntity;
        public Main()
        {
            InitializeComponent();
            mainControl = new MainControl();
            mainEntity = new MainEntity();
            SetDragEvent(tb_file_path,tb_loading_path,tb_icon_path,tb_signer_path,tb_channel);
        }

        private void SetDragEvent(params TextBox[] tbs)
        {
            foreach(TextBox textBox in tbs)
            {
                textBox.AllowDrop = true;
                TextBoxEvent textBoxEvent = new TextBoxEvent(textBox);
                textBox.DragEnter += new DragEventHandler(textBoxEvent.TextBox_DragEnter);
                textBox.DragDrop += new DragEventHandler(textBoxEvent.TextBox_DragDrop);
            }
        }

        private void Label_select_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Btn_one_key_Click(object sender, EventArgs e)
        {
            if(tb_file_path.Text==null|| tb_file_path.Text.Equals(""))
            {
                Console.WriteLine("请选择路径");
            }
            else
            {
                mainEntity.ApkPath = tb_file_path.Text;
                mainEntity.AppName = tb_app_name.Text;
                mainEntity.IconPath = tb_icon_path.Text;
                mainEntity.LoadingPath = tb_loading_path.Text;
                mainEntity.PackageName = tb_package_name.Text;
                mainEntity.SignerPath = tb_signer_path.Text;
                mainEntity.SignerPassword = tb_signer_password.Text;
                mainEntity.ChannePath = tb_channel.Text;
                mainControl.ExecuteProcess(mainEntity);
            }
        }

        private void Tb_file_path_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Tb_package_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tb_signer_path_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tb_channel_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
