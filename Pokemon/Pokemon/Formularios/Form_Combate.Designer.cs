namespace Pokemon
{
    partial class Form_Combate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Combate));
            this.timerAnimacionInicial = new System.Windows.Forms.Timer(this.components);
            this.panelAtaques = new System.Windows.Forms.Panel();
            this.mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.panelBag = new System.Windows.Forms.Panel();
            this.panelBolsa = new System.Windows.Forms.FlowLayoutPanel();
            this.panelIndicadorRival = new System.Windows.Forms.Panel();
            this.panelIndicadorTu = new System.Windows.Forms.Panel();
            this.panelCambioDePokemons = new System.Windows.Forms.Panel();
            this.panelPkmnChange6 = new System.Windows.Forms.Panel();
            this.panelPkmnChange5 = new System.Windows.Forms.Panel();
            this.panelPkmnChange4 = new System.Windows.Forms.Panel();
            this.panelPkmnChange3 = new System.Windows.Forms.Panel();
            this.panelPkmnChange2 = new System.Windows.Forms.Panel();
            this.panelPkmnChange1 = new System.Windows.Forms.Panel();
            this.panelMov4 = new System.Windows.Forms.Panel();
            this.panelMov3 = new System.Windows.Forms.Panel();
            this.panelMov2 = new System.Windows.Forms.Panel();
            this.panelMov1 = new System.Windows.Forms.Panel();
            this.btnCambiarPokemon = new System.Windows.Forms.PictureBox();
            this.btnHuir = new System.Windows.Forms.PictureBox();
            this.btnBolsa = new System.Windows.Forms.PictureBox();
            this.panelTexto = new System.Windows.Forms.Panel();
            this.labelTexto = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.PictureBox();
            this.btnLuchar = new System.Windows.Forms.PictureBox();
            this.marcoCombate = new System.Windows.Forms.PictureBox();
            this.panelCombate = new System.Windows.Forms.Panel();
            this.picBoxAtaque = new System.Windows.Forms.PictureBox();
            this.panelInfoTu = new System.Windows.Forms.Panel();
            this.picBoxEstadoTu = new System.Windows.Forms.PictureBox();
            this.vidaVerdeTu = new System.Windows.Forms.PictureBox();
            this.vidaRojaTu = new System.Windows.Forms.PictureBox();
            this.labelPvTu = new System.Windows.Forms.Label();
            this.labelNombreTu = new System.Windows.Forms.Label();
            this.labelNivelTu = new System.Windows.Forms.Label();
            this.panelInfoRival = new System.Windows.Forms.Panel();
            this.picBoxEstadoRival = new System.Windows.Forms.PictureBox();
            this.vidaVerdeRival = new System.Windows.Forms.PictureBox();
            this.labelRivalNombre = new System.Windows.Forms.Label();
            this.vidaRojaRival = new System.Windows.Forms.PictureBox();
            this.labelNivelRival = new System.Windows.Forms.Label();
            this.picBoxPkmnBack = new System.Windows.Forms.PictureBox();
            this.picBoxPkmnFront = new System.Windows.Forms.PictureBox();
            this.panelUp = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.CambiarFondoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CambiarMusicaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TablaTiposMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAnimacionFinal = new System.Windows.Forms.Timer(this.components);
            this.panelAtaques.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).BeginInit();
            this.panelBag.SuspendLayout();
            this.panelCambioDePokemons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCambiarPokemon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHuir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBolsa)).BeginInit();
            this.panelTexto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLuchar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marcoCombate)).BeginInit();
            this.panelCombate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAtaque)).BeginInit();
            this.panelInfoTu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEstadoTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vidaVerdeTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vidaRojaTu)).BeginInit();
            this.panelInfoRival.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEstadoRival)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vidaVerdeRival)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vidaRojaRival)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPkmnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPkmnFront)).BeginInit();
            this.panelUp.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerAnimacionInicial
            // 
            this.timerAnimacionInicial.Interval = 3200;
            this.timerAnimacionInicial.Tick += new System.EventHandler(this.TimerAnimacionInicial_Tick);
            // 
            // panelAtaques
            // 
            this.panelAtaques.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelAtaques.BackColor = System.Drawing.Color.Transparent;
            this.panelAtaques.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelAtaques.BackgroundImage")));
            this.panelAtaques.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelAtaques.Controls.Add(this.mediaPlayer);
            this.panelAtaques.Controls.Add(this.panelBag);
            this.panelAtaques.Controls.Add(this.panelIndicadorRival);
            this.panelAtaques.Controls.Add(this.panelIndicadorTu);
            this.panelAtaques.Controls.Add(this.panelCambioDePokemons);
            this.panelAtaques.Controls.Add(this.panelMov4);
            this.panelAtaques.Controls.Add(this.panelMov3);
            this.panelAtaques.Controls.Add(this.panelMov2);
            this.panelAtaques.Controls.Add(this.panelMov1);
            this.panelAtaques.Controls.Add(this.btnCambiarPokemon);
            this.panelAtaques.Controls.Add(this.btnHuir);
            this.panelAtaques.Controls.Add(this.btnBolsa);
            this.panelAtaques.Controls.Add(this.panelTexto);
            this.panelAtaques.Controls.Add(this.btnCancelar);
            this.panelAtaques.Controls.Add(this.btnLuchar);
            this.panelAtaques.Location = new System.Drawing.Point(0, 313);
            this.panelAtaques.Margin = new System.Windows.Forms.Padding(2);
            this.panelAtaques.Name = "panelAtaques";
            this.panelAtaques.Size = new System.Drawing.Size(454, 326);
            this.panelAtaques.TabIndex = 4;
            // 
            // mediaPlayer
            // 
            this.mediaPlayer.Enabled = true;
            this.mediaPlayer.Location = new System.Drawing.Point(3, 288);
            this.mediaPlayer.Name = "mediaPlayer";
            this.mediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mediaPlayer.OcxState")));
            this.mediaPlayer.Size = new System.Drawing.Size(35, 35);
            this.mediaPlayer.TabIndex = 63;
            this.mediaPlayer.Visible = false;
            // 
            // panelBag
            // 
            this.panelBag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelBag.Controls.Add(this.panelBolsa);
            this.panelBag.Location = new System.Drawing.Point(16, 120);
            this.panelBag.Name = "panelBag";
            this.panelBag.Size = new System.Drawing.Size(423, 124);
            this.panelBag.TabIndex = 62;
            this.panelBag.Visible = false;
            // 
            // panelBolsa
            // 
            this.panelBolsa.AutoScroll = true;
            this.panelBolsa.BackColor = System.Drawing.Color.White;
            this.panelBolsa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBolsa.Location = new System.Drawing.Point(4, 4);
            this.panelBolsa.Name = "panelBolsa";
            this.panelBolsa.Size = new System.Drawing.Size(415, 116);
            this.panelBolsa.TabIndex = 61;
            // 
            // panelIndicadorRival
            // 
            this.panelIndicadorRival.Location = new System.Drawing.Point(315, 2);
            this.panelIndicadorRival.Name = "panelIndicadorRival";
            this.panelIndicadorRival.Size = new System.Drawing.Size(124, 19);
            this.panelIndicadorRival.TabIndex = 60;
            // 
            // panelIndicadorTu
            // 
            this.panelIndicadorTu.Location = new System.Drawing.Point(16, 2);
            this.panelIndicadorTu.Name = "panelIndicadorTu";
            this.panelIndicadorTu.Size = new System.Drawing.Size(124, 19);
            this.panelIndicadorTu.TabIndex = 59;
            // 
            // panelCambioDePokemons
            // 
            this.panelCambioDePokemons.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelCambioDePokemons.BackColor = System.Drawing.Color.Transparent;
            this.panelCambioDePokemons.Controls.Add(this.panelPkmnChange6);
            this.panelCambioDePokemons.Controls.Add(this.panelPkmnChange5);
            this.panelCambioDePokemons.Controls.Add(this.panelPkmnChange4);
            this.panelCambioDePokemons.Controls.Add(this.panelPkmnChange3);
            this.panelCambioDePokemons.Controls.Add(this.panelPkmnChange2);
            this.panelCambioDePokemons.Controls.Add(this.panelPkmnChange1);
            this.panelCambioDePokemons.Location = new System.Drawing.Point(64, 28);
            this.panelCambioDePokemons.Name = "panelCambioDePokemons";
            this.panelCambioDePokemons.Size = new System.Drawing.Size(329, 226);
            this.panelCambioDePokemons.TabIndex = 58;
            this.panelCambioDePokemons.Visible = false;
            // 
            // panelPkmnChange6
            // 
            this.panelPkmnChange6.Location = new System.Drawing.Point(169, 159);
            this.panelPkmnChange6.Name = "panelPkmnChange6";
            this.panelPkmnChange6.Size = new System.Drawing.Size(156, 64);
            this.panelPkmnChange6.TabIndex = 4;
            // 
            // panelPkmnChange5
            // 
            this.panelPkmnChange5.Location = new System.Drawing.Point(2, 142);
            this.panelPkmnChange5.Name = "panelPkmnChange5";
            this.panelPkmnChange5.Size = new System.Drawing.Size(156, 64);
            this.panelPkmnChange5.TabIndex = 1;
            // 
            // panelPkmnChange4
            // 
            this.panelPkmnChange4.Location = new System.Drawing.Point(169, 86);
            this.panelPkmnChange4.Name = "panelPkmnChange4";
            this.panelPkmnChange4.Size = new System.Drawing.Size(156, 64);
            this.panelPkmnChange4.TabIndex = 3;
            // 
            // panelPkmnChange3
            // 
            this.panelPkmnChange3.Location = new System.Drawing.Point(2, 72);
            this.panelPkmnChange3.Name = "panelPkmnChange3";
            this.panelPkmnChange3.Size = new System.Drawing.Size(156, 64);
            this.panelPkmnChange3.TabIndex = 1;
            // 
            // panelPkmnChange2
            // 
            this.panelPkmnChange2.Location = new System.Drawing.Point(171, 14);
            this.panelPkmnChange2.Name = "panelPkmnChange2";
            this.panelPkmnChange2.Size = new System.Drawing.Size(156, 64);
            this.panelPkmnChange2.TabIndex = 2;
            // 
            // panelPkmnChange1
            // 
            this.panelPkmnChange1.Location = new System.Drawing.Point(2, 2);
            this.panelPkmnChange1.Name = "panelPkmnChange1";
            this.panelPkmnChange1.Size = new System.Drawing.Size(156, 64);
            this.panelPkmnChange1.TabIndex = 0;
            // 
            // panelMov4
            // 
            this.panelMov4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelMov4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMov4.BackgroundImage")));
            this.panelMov4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelMov4.Location = new System.Drawing.Point(254, 193);
            this.panelMov4.Name = "panelMov4";
            this.panelMov4.Size = new System.Drawing.Size(135, 64);
            this.panelMov4.TabIndex = 57;
            this.panelMov4.Visible = false;
            // 
            // panelMov3
            // 
            this.panelMov3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelMov3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMov3.BackgroundImage")));
            this.panelMov3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelMov3.Location = new System.Drawing.Point(64, 193);
            this.panelMov3.Name = "panelMov3";
            this.panelMov3.Size = new System.Drawing.Size(135, 64);
            this.panelMov3.TabIndex = 57;
            this.panelMov3.Visible = false;
            // 
            // panelMov2
            // 
            this.panelMov2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelMov2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMov2.BackgroundImage")));
            this.panelMov2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelMov2.Location = new System.Drawing.Point(254, 123);
            this.panelMov2.Name = "panelMov2";
            this.panelMov2.Size = new System.Drawing.Size(135, 64);
            this.panelMov2.TabIndex = 57;
            this.panelMov2.Visible = false;
            // 
            // panelMov1
            // 
            this.panelMov1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelMov1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMov1.BackgroundImage")));
            this.panelMov1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelMov1.Location = new System.Drawing.Point(64, 123);
            this.panelMov1.Name = "panelMov1";
            this.panelMov1.Size = new System.Drawing.Size(135, 64);
            this.panelMov1.TabIndex = 56;
            this.panelMov1.Visible = false;
            // 
            // btnCambiarPokemon
            // 
            this.btnCambiarPokemon.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCambiarPokemon.BackColor = System.Drawing.Color.Transparent;
            this.btnCambiarPokemon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCambiarPokemon.BackgroundImage")));
            this.btnCambiarPokemon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCambiarPokemon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCambiarPokemon.Location = new System.Drawing.Point(183, 260);
            this.btnCambiarPokemon.Name = "btnCambiarPokemon";
            this.btnCambiarPokemon.Size = new System.Drawing.Size(90, 55);
            this.btnCambiarPokemon.TabIndex = 51;
            this.btnCambiarPokemon.TabStop = false;
            this.btnCambiarPokemon.Click += new System.EventHandler(this.BtnCambiarPokemon_Click);
            this.btnCambiarPokemon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnCambiarPokemon_MouseDown);
            this.btnCambiarPokemon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnCambiarPokemon_MouseUp);
            // 
            // btnHuir
            // 
            this.btnHuir.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnHuir.BackColor = System.Drawing.Color.Transparent;
            this.btnHuir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHuir.BackgroundImage")));
            this.btnHuir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnHuir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuir.Location = new System.Drawing.Point(319, 248);
            this.btnHuir.Name = "btnHuir";
            this.btnHuir.Size = new System.Drawing.Size(93, 55);
            this.btnHuir.TabIndex = 9;
            this.btnHuir.TabStop = false;
            this.btnHuir.Click += new System.EventHandler(this.BtnHuir_Click);
            this.btnHuir.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnHuir_MouseDown);
            this.btnHuir.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnHuir_MouseUp);
            // 
            // btnBolsa
            // 
            this.btnBolsa.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBolsa.BackColor = System.Drawing.Color.Transparent;
            this.btnBolsa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBolsa.BackgroundImage")));
            this.btnBolsa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBolsa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBolsa.Location = new System.Drawing.Point(44, 248);
            this.btnBolsa.Name = "btnBolsa";
            this.btnBolsa.Size = new System.Drawing.Size(90, 55);
            this.btnBolsa.TabIndex = 8;
            this.btnBolsa.TabStop = false;
            this.btnBolsa.Click += new System.EventHandler(this.BtnBolsa_Click);
            this.btnBolsa.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnBolsa_MouseDown);
            this.btnBolsa.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnBolsa_MouseUp);
            // 
            // panelTexto
            // 
            this.panelTexto.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelTexto.BackColor = System.Drawing.Color.Transparent;
            this.panelTexto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelTexto.BackgroundImage")));
            this.panelTexto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTexto.Controls.Add(this.labelTexto);
            this.panelTexto.Location = new System.Drawing.Point(16, 28);
            this.panelTexto.Margin = new System.Windows.Forms.Padding(2);
            this.panelTexto.Name = "panelTexto";
            this.panelTexto.Size = new System.Drawing.Size(423, 87);
            this.panelTexto.TabIndex = 5;
            // 
            // labelTexto
            // 
            this.labelTexto.Font = new System.Drawing.Font("OCR A Extended", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTexto.Location = new System.Drawing.Point(17, 9);
            this.labelTexto.MaximumSize = new System.Drawing.Size(375, 76);
            this.labelTexto.Name = "labelTexto";
            this.labelTexto.Size = new System.Drawing.Size(372, 73);
            this.labelTexto.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelar.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancelar.BackgroundImage")));
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Location = new System.Drawing.Point(106, 263);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(246, 55);
            this.btnCancelar.TabIndex = 46;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            this.btnCancelar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnCancelar_MouseDown);
            this.btnCancelar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnCancelar_MouseUp);
            // 
            // btnLuchar
            // 
            this.btnLuchar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLuchar.BackColor = System.Drawing.Color.Transparent;
            this.btnLuchar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLuchar.BackgroundImage")));
            this.btnLuchar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLuchar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuchar.Location = new System.Drawing.Point(108, 131);
            this.btnLuchar.Name = "btnLuchar";
            this.btnLuchar.Size = new System.Drawing.Size(239, 113);
            this.btnLuchar.TabIndex = 7;
            this.btnLuchar.TabStop = false;
            this.btnLuchar.Click += new System.EventHandler(this.BtnLuchar_Click);
            this.btnLuchar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnLuchar_MouseDown);
            this.btnLuchar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnLuchar_MouseUp);
            // 
            // marcoCombate
            // 
            this.marcoCombate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.marcoCombate.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.marcoCombate.Location = new System.Drawing.Point(12, 7);
            this.marcoCombate.Name = "marcoCombate";
            this.marcoCombate.Size = new System.Drawing.Size(432, 272);
            this.marcoCombate.TabIndex = 6;
            this.marcoCombate.TabStop = false;
            // 
            // panelCombate
            // 
            this.panelCombate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelCombate.BackColor = System.Drawing.SystemColors.Control;
            this.panelCombate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelCombate.BackgroundImage")));
            this.panelCombate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelCombate.Controls.Add(this.picBoxAtaque);
            this.panelCombate.Controls.Add(this.panelInfoTu);
            this.panelCombate.Controls.Add(this.panelInfoRival);
            this.panelCombate.Controls.Add(this.picBoxPkmnBack);
            this.panelCombate.Controls.Add(this.picBoxPkmnFront);
            this.panelCombate.Location = new System.Drawing.Point(16, 12);
            this.panelCombate.Name = "panelCombate";
            this.panelCombate.Size = new System.Drawing.Size(423, 262);
            this.panelCombate.TabIndex = 2;
            // 
            // picBoxAtaque
            // 
            this.picBoxAtaque.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBoxAtaque.BackColor = System.Drawing.Color.Transparent;
            this.picBoxAtaque.Location = new System.Drawing.Point(167, 95);
            this.picBoxAtaque.Name = "picBoxAtaque";
            this.picBoxAtaque.Size = new System.Drawing.Size(59, 50);
            this.picBoxAtaque.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxAtaque.TabIndex = 14;
            this.picBoxAtaque.TabStop = false;
            this.picBoxAtaque.Visible = false;
            // 
            // panelInfoTu
            // 
            this.panelInfoTu.BackColor = System.Drawing.Color.Transparent;
            this.panelInfoTu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelInfoTu.BackgroundImage")));
            this.panelInfoTu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelInfoTu.Controls.Add(this.picBoxEstadoTu);
            this.panelInfoTu.Controls.Add(this.vidaVerdeTu);
            this.panelInfoTu.Controls.Add(this.vidaRojaTu);
            this.panelInfoTu.Controls.Add(this.labelPvTu);
            this.panelInfoTu.Controls.Add(this.labelNombreTu);
            this.panelInfoTu.Controls.Add(this.labelNivelTu);
            this.panelInfoTu.Location = new System.Drawing.Point(225, 188);
            this.panelInfoTu.Name = "panelInfoTu";
            this.panelInfoTu.Size = new System.Drawing.Size(200, 67);
            this.panelInfoTu.TabIndex = 12;
            this.panelInfoTu.Visible = false;
            // 
            // picBoxEstadoTu
            // 
            this.picBoxEstadoTu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxEstadoTu.BackgroundImage")));
            this.picBoxEstadoTu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxEstadoTu.Location = new System.Drawing.Point(35, 31);
            this.picBoxEstadoTu.Name = "picBoxEstadoTu";
            this.picBoxEstadoTu.Size = new System.Drawing.Size(32, 18);
            this.picBoxEstadoTu.TabIndex = 11;
            this.picBoxEstadoTu.TabStop = false;
            // 
            // vidaVerdeTu
            // 
            this.vidaVerdeTu.BackColor = System.Drawing.Color.Transparent;
            this.vidaVerdeTu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("vidaVerdeTu.BackgroundImage")));
            this.vidaVerdeTu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.vidaVerdeTu.Location = new System.Drawing.Point(101, 35);
            this.vidaVerdeTu.Margin = new System.Windows.Forms.Padding(0);
            this.vidaVerdeTu.Name = "vidaVerdeTu";
            this.vidaVerdeTu.Size = new System.Drawing.Size(86, 6);
            this.vidaVerdeTu.TabIndex = 10;
            this.vidaVerdeTu.TabStop = false;
            // 
            // vidaRojaTu
            // 
            this.vidaRojaTu.BackColor = System.Drawing.Color.Transparent;
            this.vidaRojaTu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("vidaRojaTu.BackgroundImage")));
            this.vidaRojaTu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.vidaRojaTu.Location = new System.Drawing.Point(101, 35);
            this.vidaRojaTu.Margin = new System.Windows.Forms.Padding(0);
            this.vidaRojaTu.Name = "vidaRojaTu";
            this.vidaRojaTu.Size = new System.Drawing.Size(86, 6);
            this.vidaRojaTu.TabIndex = 9;
            this.vidaRojaTu.TabStop = false;
            // 
            // labelPvTu
            // 
            this.labelPvTu.BackColor = System.Drawing.Color.Transparent;
            this.labelPvTu.Font = new System.Drawing.Font("OCR A Extended", 8.25F);
            this.labelPvTu.Location = new System.Drawing.Point(61, 47);
            this.labelPvTu.Name = "labelPvTu";
            this.labelPvTu.Size = new System.Drawing.Size(126, 20);
            this.labelPvTu.TabIndex = 5;
            this.labelPvTu.Text = "PV 30/334";
            this.labelPvTu.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelNombreTu
            // 
            this.labelNombreTu.BackColor = System.Drawing.Color.Transparent;
            this.labelNombreTu.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelNombreTu.Location = new System.Drawing.Point(32, 13);
            this.labelNombreTu.Name = "labelNombreTu";
            this.labelNombreTu.Size = new System.Drawing.Size(107, 22);
            this.labelNombreTu.TabIndex = 4;
            this.labelNombreTu.Text = "Bulbasaur";
            // 
            // labelNivelTu
            // 
            this.labelNivelTu.BackColor = System.Drawing.Color.Transparent;
            this.labelNivelTu.Font = new System.Drawing.Font("OCR A Extended", 8.25F);
            this.labelNivelTu.Location = new System.Drawing.Point(133, 14);
            this.labelNivelTu.Name = "labelNivelTu";
            this.labelNivelTu.Size = new System.Drawing.Size(54, 21);
            this.labelNivelTu.TabIndex = 6;
            this.labelNivelTu.Text = "Nv65";
            this.labelNivelTu.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panelInfoRival
            // 
            this.panelInfoRival.BackColor = System.Drawing.Color.Transparent;
            this.panelInfoRival.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelInfoRival.BackgroundImage")));
            this.panelInfoRival.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelInfoRival.Controls.Add(this.picBoxEstadoRival);
            this.panelInfoRival.Controls.Add(this.vidaVerdeRival);
            this.panelInfoRival.Controls.Add(this.labelRivalNombre);
            this.panelInfoRival.Controls.Add(this.vidaRojaRival);
            this.panelInfoRival.Controls.Add(this.labelNivelRival);
            this.panelInfoRival.Location = new System.Drawing.Point(0, 3);
            this.panelInfoRival.Name = "panelInfoRival";
            this.panelInfoRival.Size = new System.Drawing.Size(209, 59);
            this.panelInfoRival.TabIndex = 13;
            this.panelInfoRival.Visible = false;
            // 
            // picBoxEstadoRival
            // 
            this.picBoxEstadoRival.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxEstadoRival.BackgroundImage")));
            this.picBoxEstadoRival.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxEstadoRival.Location = new System.Drawing.Point(16, 32);
            this.picBoxEstadoRival.Name = "picBoxEstadoRival";
            this.picBoxEstadoRival.Size = new System.Drawing.Size(32, 18);
            this.picBoxEstadoRival.TabIndex = 12;
            this.picBoxEstadoRival.TabStop = false;
            // 
            // vidaVerdeRival
            // 
            this.vidaVerdeRival.BackColor = System.Drawing.Color.Transparent;
            this.vidaVerdeRival.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("vidaVerdeRival.BackgroundImage")));
            this.vidaVerdeRival.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.vidaVerdeRival.Location = new System.Drawing.Point(86, 40);
            this.vidaVerdeRival.Margin = new System.Windows.Forms.Padding(0);
            this.vidaVerdeRival.Name = "vidaVerdeRival";
            this.vidaVerdeRival.Size = new System.Drawing.Size(86, 6);
            this.vidaVerdeRival.TabIndex = 11;
            this.vidaVerdeRival.TabStop = false;
            // 
            // labelRivalNombre
            // 
            this.labelRivalNombre.BackColor = System.Drawing.Color.Transparent;
            this.labelRivalNombre.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelRivalNombre.Location = new System.Drawing.Point(13, 15);
            this.labelRivalNombre.Name = "labelRivalNombre";
            this.labelRivalNombre.Size = new System.Drawing.Size(105, 25);
            this.labelRivalNombre.TabIndex = 4;
            this.labelRivalNombre.Text = "Bulbasaur";
            // 
            // vidaRojaRival
            // 
            this.vidaRojaRival.BackColor = System.Drawing.Color.Transparent;
            this.vidaRojaRival.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("vidaRojaRival.BackgroundImage")));
            this.vidaRojaRival.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.vidaRojaRival.Location = new System.Drawing.Point(86, 40);
            this.vidaRojaRival.Margin = new System.Windows.Forms.Padding(0);
            this.vidaRojaRival.Name = "vidaRojaRival";
            this.vidaRojaRival.Size = new System.Drawing.Size(86, 6);
            this.vidaRojaRival.TabIndex = 12;
            this.vidaRojaRival.TabStop = false;
            // 
            // labelNivelRival
            // 
            this.labelNivelRival.BackColor = System.Drawing.Color.Transparent;
            this.labelNivelRival.Font = new System.Drawing.Font("OCR A Extended", 8.25F);
            this.labelNivelRival.Location = new System.Drawing.Point(124, 15);
            this.labelNivelRival.Name = "labelNivelRival";
            this.labelNivelRival.Size = new System.Drawing.Size(48, 25);
            this.labelNivelRival.TabIndex = 6;
            this.labelNivelRival.Text = "Nv65";
            this.labelNivelRival.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // picBoxPkmnBack
            // 
            this.picBoxPkmnBack.BackColor = System.Drawing.Color.Transparent;
            this.picBoxPkmnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBoxPkmnBack.Location = new System.Drawing.Point(66, 155);
            this.picBoxPkmnBack.Name = "picBoxPkmnBack";
            this.picBoxPkmnBack.Size = new System.Drawing.Size(62, 59);
            this.picBoxPkmnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxPkmnBack.TabIndex = 1;
            this.picBoxPkmnBack.TabStop = false;
            // 
            // picBoxPkmnFront
            // 
            this.picBoxPkmnFront.BackColor = System.Drawing.Color.Transparent;
            this.picBoxPkmnFront.Location = new System.Drawing.Point(303, 41);
            this.picBoxPkmnFront.Name = "picBoxPkmnFront";
            this.picBoxPkmnFront.Size = new System.Drawing.Size(61, 59);
            this.picBoxPkmnFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxPkmnFront.TabIndex = 0;
            this.picBoxPkmnFront.TabStop = false;
            // 
            // panelUp
            // 
            this.panelUp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUp.BackColor = System.Drawing.Color.Transparent;
            this.panelUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelUp.BackgroundImage")));
            this.panelUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelUp.Controls.Add(this.panelCombate);
            this.panelUp.Controls.Add(this.marcoCombate);
            this.panelUp.Location = new System.Drawing.Point(0, 23);
            this.panelUp.Name = "panelUp";
            this.panelUp.Size = new System.Drawing.Size(454, 290);
            this.panelUp.TabIndex = 3;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CambiarFondoMenu,
            this.CambiarMusicaMenu,
            this.TablaTiposMenu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(454, 24);
            this.menu.TabIndex = 55;
            this.menu.Text = "menuStrip1";
            // 
            // CambiarFondoMenu
            // 
            this.CambiarFondoMenu.Font = new System.Drawing.Font("Consolas", 9.25F);
            this.CambiarFondoMenu.Name = "CambiarFondoMenu";
            this.CambiarFondoMenu.Size = new System.Drawing.Size(110, 20);
            this.CambiarFondoMenu.Text = "Cambiar Fondo";
            this.CambiarFondoMenu.Click += new System.EventHandler(this.GenerarFondoCombate);
            // 
            // CambiarMusicaMenu
            // 
            this.CambiarMusicaMenu.Font = new System.Drawing.Font("Consolas", 9.25F);
            this.CambiarMusicaMenu.Name = "CambiarMusicaMenu";
            this.CambiarMusicaMenu.Size = new System.Drawing.Size(117, 20);
            this.CambiarMusicaMenu.Text = "Cambiar Musica";
            this.CambiarMusicaMenu.Click += new System.EventHandler(this.CambiarMusicaToolStripMenuItem_Click);
            // 
            // TablaTiposMenu
            // 
            this.TablaTiposMenu.Name = "TablaTiposMenu";
            this.TablaTiposMenu.Size = new System.Drawing.Size(77, 20);
            this.TablaTiposMenu.Text = "Tabla Tipos";
            this.TablaTiposMenu.Click += new System.EventHandler(this.TablaTiposMenu_Click);
            // 
            // timerAnimacionFinal
            // 
            this.timerAnimacionFinal.Interval = 3000;
            this.timerAnimacionFinal.Tick += new System.EventHandler(this.TimerAnimacionFinal_Tick);
            // 
            // Form_Combate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(454, 662);
            this.ControlBox = false;
            this.Controls.Add(this.panelUp);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.panelAtaques);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(470, 678);
            this.Name = "Form_Combate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Combate";
            this.Resize += new System.EventHandler(this.Combate_Resize);
            this.panelAtaques.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).EndInit();
            this.panelBag.ResumeLayout(false);
            this.panelCambioDePokemons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCambiarPokemon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHuir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBolsa)).EndInit();
            this.panelTexto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLuchar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marcoCombate)).EndInit();
            this.panelCombate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAtaque)).EndInit();
            this.panelInfoTu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEstadoTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vidaVerdeTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vidaRojaTu)).EndInit();
            this.panelInfoRival.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEstadoRival)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vidaVerdeRival)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vidaRojaRival)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPkmnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPkmnFront)).EndInit();
            this.panelUp.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerAnimacionInicial;
        private System.Windows.Forms.Panel panelAtaques;
        private System.Windows.Forms.Panel panelTexto;
        private System.Windows.Forms.PictureBox btnHuir;
        private System.Windows.Forms.PictureBox btnBolsa;
        private System.Windows.Forms.PictureBox btnLuchar;
        private System.Windows.Forms.PictureBox marcoCombate;
        private System.Windows.Forms.Panel panelCombate;
        private System.Windows.Forms.Label labelNombreTu;
        private System.Windows.Forms.Label labelRivalNombre;
        private System.Windows.Forms.Panel panelUp;
        private System.Windows.Forms.Panel panelInfoTu;
        private System.Windows.Forms.Panel panelInfoRival;
        private System.Windows.Forms.PictureBox btnCancelar;
        private System.Windows.Forms.PictureBox btnCambiarPokemon;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem CambiarFondoMenu;
        private System.Windows.Forms.ToolStripMenuItem CambiarMusicaMenu;
        public System.Windows.Forms.PictureBox vidaVerdeTu;
        public System.Windows.Forms.PictureBox vidaRojaTu;
        public System.Windows.Forms.Label labelPvTu;
        public System.Windows.Forms.PictureBox vidaVerdeRival;
        public System.Windows.Forms.PictureBox vidaRojaRival;
        private System.Windows.Forms.Panel panelMov1;
        private System.Windows.Forms.Panel panelMov4;
        private System.Windows.Forms.Panel panelMov3;
        private System.Windows.Forms.Panel panelMov2;
        private System.Windows.Forms.Panel panelCambioDePokemons;
        private System.Windows.Forms.Panel panelPkmnChange1;
        private System.Windows.Forms.Panel panelPkmnChange3;
        private System.Windows.Forms.Panel panelPkmnChange6;
        private System.Windows.Forms.Panel panelPkmnChange5;
        private System.Windows.Forms.Panel panelPkmnChange4;
        private System.Windows.Forms.Panel panelPkmnChange2;
        private System.Windows.Forms.Label labelTexto;
        public System.Windows.Forms.PictureBox picBoxPkmnBack;
        public System.Windows.Forms.PictureBox picBoxPkmnFront;
        public System.Windows.Forms.PictureBox picBoxAtaque;
        public System.Windows.Forms.PictureBox picBoxEstadoTu;
        public System.Windows.Forms.PictureBox picBoxEstadoRival;
        private System.Windows.Forms.Panel panelIndicadorTu;
        private System.Windows.Forms.Panel panelIndicadorRival;
        private System.Windows.Forms.ToolStripMenuItem TablaTiposMenu;
        private System.Windows.Forms.Timer timerAnimacionFinal;
        private System.Windows.Forms.FlowLayoutPanel panelBolsa;
        private System.Windows.Forms.Panel panelBag;
        public System.Windows.Forms.Label labelNivelTu;
        public System.Windows.Forms.Label labelNivelRival;
        public AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
    }
}

