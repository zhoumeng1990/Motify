namespace ModifyPackage
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_select = new System.Windows.Forms.Label();
            this.tb_file_path = new System.Windows.Forms.TextBox();
            this.label_package_name = new System.Windows.Forms.Label();
            this.tb_package_name = new System.Windows.Forms.TextBox();
            this.tb_loading_path = new System.Windows.Forms.TextBox();
            this.label_loading = new System.Windows.Forms.Label();
            this.tb_icon_path = new System.Windows.Forms.TextBox();
            this.label_icon_path = new System.Windows.Forms.Label();
            this.tb_app_name = new System.Windows.Forms.TextBox();
            this.label_app_name = new System.Windows.Forms.Label();
            this.btn_one_key = new System.Windows.Forms.Button();
            this.tb_signer_path = new System.Windows.Forms.TextBox();
            this.label_signer_path = new System.Windows.Forms.Label();
            this.tb_signer_password = new System.Windows.Forms.TextBox();
            this.label_signer_password = new System.Windows.Forms.Label();
            this.tb_alias = new System.Windows.Forms.TextBox();
            this.label_alias = new System.Windows.Forms.Label();
            this.tb_channel = new System.Windows.Forms.TextBox();
            this.label_channel = new System.Windows.Forms.Label();
            this.tb_apktool = new System.Windows.Forms.TextBox();
            this.label_apktool = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_select
            // 
            this.label_select.AllowDrop = true;
            this.label_select.AutoSize = true;
            this.label_select.Location = new System.Drawing.Point(89, 21);
            this.label_select.Name = "label_select";
            this.label_select.Size = new System.Drawing.Size(77, 12);
            this.label_select.TabIndex = 0;
            this.label_select.Text = "选择执行路径";
            // 
            // tb_file_path
            // 
            this.tb_file_path.Location = new System.Drawing.Point(189, 18);
            this.tb_file_path.Name = "tb_file_path";
            this.tb_file_path.Size = new System.Drawing.Size(264, 21);
            this.tb_file_path.TabIndex = 1;
            // 
            // label_package_name
            // 
            this.label_package_name.AutoSize = true;
            this.label_package_name.Location = new System.Drawing.Point(91, 101);
            this.label_package_name.Name = "label_package_name";
            this.label_package_name.Size = new System.Drawing.Size(83, 12);
            this.label_package_name.TabIndex = 2;
            this.label_package_name.Text = "包名(package)";
            // 
            // tb_package_name
            // 
            this.tb_package_name.Location = new System.Drawing.Point(189, 98);
            this.tb_package_name.Name = "tb_package_name";
            this.tb_package_name.Size = new System.Drawing.Size(264, 21);
            this.tb_package_name.TabIndex = 3;
            // 
            // tb_loading_path
            // 
            this.tb_loading_path.Location = new System.Drawing.Point(189, 214);
            this.tb_loading_path.Name = "tb_loading_path";
            this.tb_loading_path.Size = new System.Drawing.Size(264, 21);
            this.tb_loading_path.TabIndex = 5;
            // 
            // label_loading
            // 
            this.label_loading.AutoSize = true;
            this.label_loading.Location = new System.Drawing.Point(91, 217);
            this.label_loading.Name = "label_loading";
            this.label_loading.Size = new System.Drawing.Size(65, 12);
            this.label_loading.TabIndex = 4;
            this.label_loading.Text = "启动页路径";
            // 
            // tb_icon_path
            // 
            this.tb_icon_path.Location = new System.Drawing.Point(189, 137);
            this.tb_icon_path.Name = "tb_icon_path";
            this.tb_icon_path.Size = new System.Drawing.Size(264, 21);
            this.tb_icon_path.TabIndex = 7;
            // 
            // label_icon_path
            // 
            this.label_icon_path.AutoSize = true;
            this.label_icon_path.Location = new System.Drawing.Point(91, 140);
            this.label_icon_path.Name = "label_icon_path";
            this.label_icon_path.Size = new System.Drawing.Size(59, 12);
            this.label_icon_path.TabIndex = 6;
            this.label_icon_path.Text = "icon 路径";
            // 
            // tb_app_name
            // 
            this.tb_app_name.Location = new System.Drawing.Point(189, 176);
            this.tb_app_name.Name = "tb_app_name";
            this.tb_app_name.Size = new System.Drawing.Size(264, 21);
            this.tb_app_name.TabIndex = 9;
            // 
            // label_app_name
            // 
            this.label_app_name.AutoSize = true;
            this.label_app_name.Location = new System.Drawing.Point(91, 179);
            this.label_app_name.Name = "label_app_name";
            this.label_app_name.Size = new System.Drawing.Size(53, 12);
            this.label_app_name.TabIndex = 8;
            this.label_app_name.Text = "应用名称";
            // 
            // btn_one_key
            // 
            this.btn_one_key.Location = new System.Drawing.Point(680, 396);
            this.btn_one_key.Name = "btn_one_key";
            this.btn_one_key.Size = new System.Drawing.Size(75, 23);
            this.btn_one_key.TabIndex = 10;
            this.btn_one_key.Text = "一键";
            this.btn_one_key.UseVisualStyleBackColor = true;
            this.btn_one_key.Click += new System.EventHandler(this.Btn_one_key_Click);
            // 
            // tb_signer_path
            // 
            this.tb_signer_path.Location = new System.Drawing.Point(189, 254);
            this.tb_signer_path.Name = "tb_signer_path";
            this.tb_signer_path.Size = new System.Drawing.Size(264, 21);
            this.tb_signer_path.TabIndex = 12;
            // 
            // label_signer_path
            // 
            this.label_signer_path.AutoSize = true;
            this.label_signer_path.Location = new System.Drawing.Point(91, 257);
            this.label_signer_path.Name = "label_signer_path";
            this.label_signer_path.Size = new System.Drawing.Size(77, 12);
            this.label_signer_path.TabIndex = 11;
            this.label_signer_path.Text = "签名文件路径";
            // 
            // tb_signer_password
            // 
            this.tb_signer_password.Location = new System.Drawing.Point(189, 295);
            this.tb_signer_password.Name = "tb_signer_password";
            this.tb_signer_password.Size = new System.Drawing.Size(264, 21);
            this.tb_signer_password.TabIndex = 14;
            // 
            // label_signer_password
            // 
            this.label_signer_password.AutoSize = true;
            this.label_signer_password.Location = new System.Drawing.Point(91, 298);
            this.label_signer_password.Name = "label_signer_password";
            this.label_signer_password.Size = new System.Drawing.Size(77, 12);
            this.label_signer_password.TabIndex = 13;
            this.label_signer_password.Text = "签名文件密码";
            // 
            // tb_alias
            // 
            this.tb_alias.Location = new System.Drawing.Point(189, 335);
            this.tb_alias.Name = "tb_alias";
            this.tb_alias.Size = new System.Drawing.Size(264, 21);
            this.tb_alias.TabIndex = 16;
            // 
            // label_alias
            // 
            this.label_alias.AutoSize = true;
            this.label_alias.Location = new System.Drawing.Point(91, 338);
            this.label_alias.Name = "label_alias";
            this.label_alias.Size = new System.Drawing.Size(77, 12);
            this.label_alias.TabIndex = 15;
            this.label_alias.Text = "签名文件别名";
            // 
            // tb_channel
            // 
            this.tb_channel.AllowDrop = true;
            this.tb_channel.Location = new System.Drawing.Point(189, 376);
            this.tb_channel.Name = "tb_channel";
            this.tb_channel.Size = new System.Drawing.Size(264, 21);
            this.tb_channel.TabIndex = 18;
            // 
            // label_channel
            // 
            this.label_channel.AutoSize = true;
            this.label_channel.Location = new System.Drawing.Point(91, 379);
            this.label_channel.Name = "label_channel";
            this.label_channel.Size = new System.Drawing.Size(53, 12);
            this.label_channel.TabIndex = 17;
            this.label_channel.Text = "渠道文件";
            // 
            // tb_apktool
            // 
            this.tb_apktool.AllowDrop = true;
            this.tb_apktool.Location = new System.Drawing.Point(189, 58);
            this.tb_apktool.Name = "tb_apktool";
            this.tb_apktool.Size = new System.Drawing.Size(264, 21);
            this.tb_apktool.TabIndex = 20;
            // 
            // label_apktool
            // 
            this.label_apktool.AutoSize = true;
            this.label_apktool.Location = new System.Drawing.Point(91, 61);
            this.label_apktool.Name = "label_apktool";
            this.label_apktool.Size = new System.Drawing.Size(71, 12);
            this.label_apktool.TabIndex = 19;
            this.label_apktool.Text = "apktool路径";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tb_apktool);
            this.Controls.Add(this.label_apktool);
            this.Controls.Add(this.tb_channel);
            this.Controls.Add(this.label_channel);
            this.Controls.Add(this.tb_alias);
            this.Controls.Add(this.label_alias);
            this.Controls.Add(this.tb_signer_password);
            this.Controls.Add(this.label_signer_password);
            this.Controls.Add(this.tb_signer_path);
            this.Controls.Add(this.label_signer_path);
            this.Controls.Add(this.btn_one_key);
            this.Controls.Add(this.tb_app_name);
            this.Controls.Add(this.label_app_name);
            this.Controls.Add(this.tb_icon_path);
            this.Controls.Add(this.label_icon_path);
            this.Controls.Add(this.tb_loading_path);
            this.Controls.Add(this.label_loading);
            this.Controls.Add(this.tb_package_name);
            this.Controls.Add(this.label_package_name);
            this.Controls.Add(this.tb_file_path);
            this.Controls.Add(this.label_select);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_select;
        private System.Windows.Forms.TextBox tb_file_path;
        private System.Windows.Forms.Label label_package_name;
        private System.Windows.Forms.TextBox tb_package_name;
        private System.Windows.Forms.TextBox tb_loading_path;
        private System.Windows.Forms.Label label_loading;
        private System.Windows.Forms.TextBox tb_icon_path;
        private System.Windows.Forms.Label label_icon_path;
        private System.Windows.Forms.TextBox tb_app_name;
        private System.Windows.Forms.Label label_app_name;
        private System.Windows.Forms.Button btn_one_key;
        private System.Windows.Forms.TextBox tb_signer_path;
        private System.Windows.Forms.Label label_signer_path;
        private System.Windows.Forms.TextBox tb_signer_password;
        private System.Windows.Forms.Label label_signer_password;
        private System.Windows.Forms.TextBox tb_alias;
        private System.Windows.Forms.Label label_alias;
        private System.Windows.Forms.TextBox tb_channel;
        private System.Windows.Forms.Label label_channel;
        private System.Windows.Forms.TextBox tb_apktool;
        private System.Windows.Forms.Label label_apktool;
    }
}

