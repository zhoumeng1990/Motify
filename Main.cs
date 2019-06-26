using MotifyPackage.control;
using MotifyPackage.entify;
using MotifyPackage.events;
using MotifyPackage.interfaces;
using MotifyPackage.utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace MotifyPackage
{
    public partial class Main : Form, IMain
    {

        private readonly MainControl mainControl;
        private readonly MainEntity mainEntity;
        public Main()
        {
            InitializeComponent();
            mainControl = new MainControl(this);
            mainEntity = new MainEntity();
            SetDragEvent(tb_file_path, tb_loading_path, tb_icon_path, tb_signer_path, tb_channel, tb_apktool);
        }

        private void SetDragEvent(params TextBox[] tbs)
        {
            foreach (TextBox textBox in tbs)
            {
                textBox.AllowDrop = true;
                TextBoxEvent textBoxEvent = new TextBoxEvent(textBox);
                textBox.DragEnter += new DragEventHandler(textBoxEvent.TextBox_DragEnter);
                textBox.DragDrop += new DragEventHandler(textBoxEvent.TextBox_DragDrop);
            }
        }

        private void Btn_one_key_Click(object sender, EventArgs e)
        {
            if (CommonUtil.IsEmpty(tb_file_path.Text))
            {
                MessageBox.Show("APK文件路径不能为空");
            }
            /*else if (CommonUtil.IsEmpty(tb_apktool.Text))
            {
                MessageBox.Show("apktool路径不能为空");
            }*/
            else if (!(File.Exists(tb_file_path.Text)||Directory.Exists(tb_file_path.Text)))
            {
                MessageBox.Show("APK文件路径错误");
            }
            /*else if (!Path.GetExtension(tb_file_path.Text).Equals(".apk"))
            {
                MessageBox.Show("非APK文件");
            }*/
            else
            {
                mainEntity.ApkPath = tb_file_path.Text;
                mainEntity.AppName = tb_app_name.Text;
                mainEntity.IconPath = tb_icon_path.Text;
                mainEntity.LoadingPath = tb_loading_path.Text;
                mainEntity.PackageName = tb_package_name.Text;
                mainEntity.SignerPath = tb_signer_path.Text;
                mainEntity.SignerPassword = tb_signer_password.Text;
                mainEntity.Alias = tb_alias.Text;
                mainEntity.ChannePath = tb_channel.Text;
                mainEntity.ApktoolPath = tb_apktool.Text;
                mainControl.ExecuteProcess(mainEntity);
            }
        }

        public void AliasValue(string value)
        {
            if (CommonUtil.IsEmpty(mainEntity.Alias))
            {
                mainEntity.Alias = value;
            }

            tb_alias.Text = mainEntity.Alias;
        }

        public void ProcessEnd()
        {

        }
    }
}
