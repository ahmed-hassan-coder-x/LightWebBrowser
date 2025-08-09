   namespace LightWebBrowser
   {
       partial class SettingsForm
       {
           private System.ComponentModel.IContainer components = null;

           protected override void Dispose(bool disposing)
           {
               if (disposing && (components != null))
               {
                   components.Dispose();
               }
               base.Dispose(disposing);
           }

           private void InitializeComponent()
           {
               this.btnSave = new System.Windows.Forms.Button();
               this.SuspendLayout();
               // 
               // btnSave
               // 
               this.btnSave.Location = new System.Drawing.Point(12, 12);
               this.btnSave.Name = "btnSave";
               this.btnSave.Size = new System.Drawing.Size(75, 23);
               this.btnSave.TabIndex = 0;
               this.btnSave.Text = "Save";
               this.btnSave.UseVisualStyleBackColor = true;
               this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
               // 
               // SettingsForm
               // 
               this.ClientSize = new System.Drawing.Size(200, 100);
               this.Controls.Add(this.btnSave);
               this.Name = "SettingsForm";
               this.Text = "Settings";
               this.ResumeLayout(false);
           }

           private System.Windows.Forms.Button btnSave;
       }
   }
   
