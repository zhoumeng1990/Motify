using ModifyPackage.control;
using ModifyPackage.entify;
using ModifyPackage.events;
using ModifyPackage.interfaces;
using ModifyPackage.utils;
using MotifyPackage.utils;
using System;
using System.Collections.Generic;
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
            SetDragEvent(tb_file_path, tb_apktool, tb_package_name, tb_loading_path, tb_icon_path, tb_signer_path, tb_channel);
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
            Dictionary<string, string> dictionary = XmlDataUtil.GetManifestXML();
            if (dictionary != null)
            {
                foreach (KeyValuePair<String, String> keyValue in dictionary)
                {
                    switch (keyValue.Key)
                    {
                        case "apktoolPath":
                            tb_apktool.Text = keyValue.Value;
                            break;

                        case "signerPath":
                            tb_signer_path.Text = keyValue.Value;
                            break;

                        case "signerPassword":
                            tb_signer_password.Text = keyValue.Value;
                            break;

                        case "alias":
                            tb_alias.Text = keyValue.Value;
                            break;
                    }
                }
            }
        }

        private void Btn_one_key_Click(object sender, EventArgs e)
        {
            if (CommonUtil.IsEmpty(tb_file_path.Text))
            {
                MessageBox.Show("文件执行路径不能为空");
            }
            else if (!(File.Exists(tb_file_path.Text) || Directory.Exists(tb_file_path.Text)))
            {
                MessageBox.Show("文件执行路径错误");
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
                mainEntity.Alias = tb_alias.Text;
                mainEntity.ChannePath = tb_channel.Text;
                mainEntity.ApktoolPath = tb_apktool.Text;

                /*DialogResult MsgBoxResult;//设置对话框的返回值  
                MsgBoxResult = MessageBox.Show("请选择你要按下的按钮",//对话框的显示内容   
                "提示",//对话框的标题   
                MessageBoxButtons.YesNoCancel,//定义对话框的按钮，这里定义了YSE和NO两个按钮   
                MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号   
                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样  
                if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）  
                {
                    Console.WriteLine(" 你选择了按下”Yes“的按钮！");
                }
                if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）  
                {
                    Console.WriteLine(" 你选择了按下”No“的按钮！");
                }

                if (MsgBoxResult == DialogResult.Cancel)
                {
                    Console.WriteLine(" 你选择了按下”cancle“的按钮！");
                }*/
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
