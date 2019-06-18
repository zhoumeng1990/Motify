namespace MotifyPackage
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
            this.SuspendLayout();
            // 
            // label_select
            // 
            this.label_select.AllowDrop = true;
            this.label_select.AutoSize = true;
            this.label_select.Location = new System.Drawing.Point(89, 45);
            this.label_select.Name = "label_select";
            this.label_select.Size = new System.Drawing.Size(71, 12);
            this.label_select.TabIndex = 0;
            this.label_select.Text = "选择apk文件";
            this.label_select.Click += new System.EventHandler(this.Label_select_Click);
            // 
            // tb_file_path
            // 
            this.tb_file_path.Location = new System.Drawing.Point(189, 42);
            this.tb_file_path.Name = "tb_file_path";
            this.tb_file_path.Size = new System.Drawing.Size(264, 21);
            this.tb_file_path.TabIndex = 1;
            this.tb_file_path.TextChanged += new System.EventHandler(this.Tb_file_path_TextChanged);
            // 
            // label_package_name
            // 
            this.label_package_name.AutoSize = true;
            this.label_package_name.Location = new System.Drawing.Point(91, 104);
            this.label_package_name.Name = "label_package_name";
            this.label_package_name.Size = new System.Drawing.Size(83, 12);
            this.label_package_name.TabIndex = 2;
            this.label_package_name.Text = "包名(package)";
            // 
            // tb_package_name
            // 
            this.tb_package_name.Location = new System.Drawing.Point(189, 101);
            this.tb_package_name.Name = "tb_package_name";
            this.tb_package_name.Size = new System.Drawing.Size(264, 21);
            this.tb_package_name.TabIndex = 3;
            // 
            // tb_loading_path
            // 
            this.tb_loading_path.Location = new System.Drawing.Point(189, 159);
            this.tb_loading_path.Name = "tb_loading_path";
            this.tb_loading_path.Size = new System.Drawing.Size(264, 21);
            this.tb_loading_path.TabIndex = 5;
            // 
            // label_loading
            // 
            this.label_loading.AutoSize = true;
            this.label_loading.Location = new System.Drawing.Point(91, 162);
            this.label_loading.Name = "label_loading";
            this.label_loading.Size = new System.Drawing.Size(65, 12);
            this.label_loading.TabIndex = 4;
            this.label_loading.Text = "启动页路径";
            // 
            // tb_icon_path
            // 
            this.tb_icon_path.Location = new System.Drawing.Point(189, 217);
            this.tb_icon_path.Name = "tb_icon_path";
            this.tb_icon_path.Size = new System.Drawing.Size(264, 21);
            this.tb_icon_path.TabIndex = 7;
            // 
            // label_icon_path
            // 
            this.label_icon_path.AutoSize = true;
            this.label_icon_path.Location = new System.Drawing.Point(91, 220);
            this.label_icon_path.Name = "label_icon_path";
            this.label_icon_path.Size = new System.Drawing.Size(53, 12);
            this.label_icon_path.TabIndex = 6;
            this.label_icon_path.Text = "icon路径";
            this.label_icon_path.Click += new System.EventHandler(this.Label1_Click);
            // 
            // tb_app_name
            // 
            this.tb_app_name.Location = new System.Drawing.Point(189, 277);
            this.tb_app_name.Name = "tb_app_name";
            this.tb_app_name.Size = new System.Drawing.Size(264, 21);
            this.tb_app_name.TabIndex = 9;
            // 
            // label_app_name
            // 
            this.label_app_name.AutoSize = true;
            this.label_app_name.Location = new System.Drawing.Point(91, 280);
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}

