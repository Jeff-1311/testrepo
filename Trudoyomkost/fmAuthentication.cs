using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trudoyomkost
{
    public partial class fmAuthentication : Form
    {
        mainForm fm;
        List<Users> _usersList;
        private TextBox tbLogin;
        private TextBox tbPass;
        private Button button1;
        bool _isFistlogin = true;
        public fmAuthentication(mainForm fm)
        {
            
            this.fm = fm;
            InitializeComponent();
            //tbLogin.Text = "admin";
            //tbPass.Text = "admin";
            using (var newLocalDb = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                _usersList = LinqQueryForTrudoyomkost.FillUsersList(newLocalDb);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            foreach (var item in _usersList)
            {
                if (tbLogin.Text == item.UserName && tbLogin.Text == item.UserPass)
                {
                    if (_isFistlogin)
                    {
                        fm.CurrentUser = item;
                        fm.IsAuthentication = true;
                        fm.authorizationBackGround.RunWorkerAsync();
                        fm.loadingBox.Visible = true;
                        _isFistlogin = false;
                    }
                    this.Hide();
                    tbLogin.Text = "";
                }
            }
        }

        private void InitializeComponent()
        {
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(12, 12);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(100, 20);
            this.tbLogin.TabIndex = 0;
            this.tbLogin.Text = "admin";
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(138, 12);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '*';
            this.tbPass.Size = new System.Drawing.Size(100, 20);
            this.tbPass.TabIndex = 1;
            this.tbPass.Text = "admin";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Войти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fmAuthentication
            // 
            this.ClientSize = new System.Drawing.Size(261, 62);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fmAuthentication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
       
    }
}
