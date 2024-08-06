namespace mineduc
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelLogo = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnActividades = new System.Windows.Forms.Button();
            this.btnComite = new System.Windows.Forms.Button();
            this.btnEscuela = new System.Windows.Forms.Button();
            this.panelDesktopPane = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnBitacora = new System.Windows.Forms.Button();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTitleBar.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.panelLogo.Controls.Add(this.pictureBox1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(406, 117);
            this.panelLogo.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::mineduc.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(108, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(191, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(406, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1373, 117);
            this.panelTitleBar.TabIndex = 5;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(512, 38);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(390, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Ministerio de educación";
            // 
            // btnActividades
            // 
            this.btnActividades.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnActividades.FlatAppearance.BorderSize = 0;
            this.btnActividades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActividades.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActividades.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActividades.Location = new System.Drawing.Point(0, 474);
            this.btnActividades.Name = "btnActividades";
            this.btnActividades.Size = new System.Drawing.Size(406, 184);
            this.btnActividades.TabIndex = 4;
            this.btnActividades.Text = "Actividades";
            this.btnActividades.UseVisualStyleBackColor = true;
            this.btnActividades.Click += new System.EventHandler(this.btnActividades_Click);
            // 
            // btnComite
            // 
            this.btnComite.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnComite.FlatAppearance.BorderSize = 0;
            this.btnComite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnComite.Location = new System.Drawing.Point(0, 290);
            this.btnComite.Name = "btnComite";
            this.btnComite.Size = new System.Drawing.Size(406, 184);
            this.btnComite.TabIndex = 2;
            this.btnComite.Text = "Comité";
            this.btnComite.UseVisualStyleBackColor = true;
            this.btnComite.Click += new System.EventHandler(this.btnComite_Click);
            // 
            // btnEscuela
            // 
            this.btnEscuela.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEscuela.FlatAppearance.BorderSize = 0;
            this.btnEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscuela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEscuela.Location = new System.Drawing.Point(0, 117);
            this.btnEscuela.Name = "btnEscuela";
            this.btnEscuela.Size = new System.Drawing.Size(406, 173);
            this.btnEscuela.TabIndex = 1;
            this.btnEscuela.Text = "Escuela";
            this.btnEscuela.UseVisualStyleBackColor = true;
            this.btnEscuela.Click += new System.EventHandler(this.btnEscuela_Click);
            // 
            // panelDesktopPane
            // 
            this.panelDesktopPane.BackColor = System.Drawing.Color.LightGray;
            this.panelDesktopPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktopPane.Location = new System.Drawing.Point(406, 0);
            this.panelDesktopPane.Name = "panelDesktopPane";
            this.panelDesktopPane.Size = new System.Drawing.Size(1373, 872);
            this.panelDesktopPane.TabIndex = 6;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelMenu.Controls.Add(this.btnBitacora);
            this.panelMenu.Controls.Add(this.btnActividades);
            this.panelMenu.Controls.Add(this.btnComite);
            this.panelMenu.Controls.Add(this.btnEscuela);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(406, 872);
            this.panelMenu.TabIndex = 4;
            // 
            // btnBitacora
            // 
            this.btnBitacora.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBitacora.FlatAppearance.BorderSize = 0;
            this.btnBitacora.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBitacora.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBitacora.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBitacora.Location = new System.Drawing.Point(0, 658);
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Size = new System.Drawing.Size(406, 184);
            this.btnBitacora.TabIndex = 5;
            this.btnBitacora.Text = "Bitácora";
            this.btnBitacora.UseVisualStyleBackColor = true;
            this.btnBitacora.Click += new System.EventHandler(this.btnBitacora_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1779, 872);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelDesktopPane);
            this.Controls.Add(this.panelMenu);
            this.Name = "frmPrincipal";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnActividades;
        private System.Windows.Forms.Button btnComite;
        private System.Windows.Forms.Button btnEscuela;
        private System.Windows.Forms.Panel panelDesktopPane;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnBitacora;
    }
}

