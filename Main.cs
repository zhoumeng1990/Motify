using ModifyPackage.control;
using ModifyPackage.entify;
using ModifyPackage.events;
using ModifyPackage.interfaces;
using ModifyPackage.utils;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace ModifyPackage
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
            InitDataShow();
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

        //获取配置文件的信息
        private void InitDataShow()
        {
            /*tb_apktool.Text = MotifyPackage.Properties.Settings.Default.apktoolPath;
            tb_signer_path.Text = MotifyPackage.Properties.Settings.Default.signerPath;
            tb_signer_password.Text = MotifyPackage.Properties.Settings.Default.signerPassword;
            tb_alias.Text = MotifyPackage.Properties.Settings.Default.signerAlias;*/

            /*tb_apktool.Text = ConfigurationManager.AppSettings["apktoolPath"];
            tb_signer_path.Text = ConfigurationManager.AppSettings["signerPath"];
            tb_signer_password.Text = ConfigurationManager.AppSettings["signerPassword"];
            tb_alias.Text = ConfigurationManager.AppSettings["signerAlias"];*/
        }

        private void Btn_one_key_Click(object sender, EventArgs e)
        {
            if (CommonUtil.IsEmpty(tb_file_path.Text))
            {
                MessageBox.Show("文件执行路径不能为空");
            }
            /*else if (CommonUtil.IsEmpty(tb_apktool.Text))
            {
                MessageBox.Show("apktool路径不能为空");
            }*/
            else if (!(File.Exists(tb_file_path.Text) || Directory.Exists(tb_file_path.Text)))
            {
                MessageBox.Show("文件执行路径错误");
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

                /*Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["apktoolPath"].Value = tb_apktool.Text;
                configuration.AppSettings.Settings["signerPath"].Value = tb_signer_path.Text;
                configuration.AppSettings.Settings["signerPassword"].Value = tb_signer_password.Text;
                configuration.AppSettings.Settings["signerAlias"].Value = tb_alias.Text;
                configuration.Save();
                MessageBox.Show(ConfigurationManager.AppSettings["apktoolPath"]);*/
               /* MotifyPackage.Properties.Settings.Default.apktoolPath = tb_apktool.Text;
                MotifyPackage.Properties.Settings.Default.signerPath = tb_signer_path.Text;
                MotifyPackage.Properties.Settings.Default.signerPassword = tb_signer_password.Text;
                MotifyPackage.Properties.Settings.Default.signerAlias = tb_alias.Text;
                MotifyPackage.Properties.Settings.Default.Save();*/

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
