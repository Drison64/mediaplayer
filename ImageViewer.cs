using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TagLib;

namespace projectuwu
{
	public class ImageViewer : IMediaModule
	{

		private PictureBox PictureBox;
		private Label Label;
		private Button NextButton;
		private Button PreviousButton;
		private Form1 _form;
		
		public void Initialize(Form1 form)
		{
			this.PictureBox = new System.Windows.Forms.PictureBox();
			this.Label = new System.Windows.Forms.Label();
			this.NextButton = new Button();
			this.PreviousButton = new Button();

			_form = form;
			
			// 
			// label1
			// 
			this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.Label.Location = new System.Drawing.Point(8, form.MainMenuStrip.Height);
			this.Label.Name = "label1";
			this.Label.Size = new System.Drawing.Size(776, 28);
			this.Label.TabIndex = 1;
			this.Label.Text = "label1";
			
			this.NextButton.Location = new System.Drawing.Point(_form.ClientRectangle.Right - 72, _form.ClientRectangle.Height / 2 - 32);
			this.NextButton.Name = "_nextButton";
			this.NextButton.Size = new System.Drawing.Size(64, 64);
			this.NextButton.TabIndex = 4;
			this.NextButton.Text = "\u23f5\ufe0e";
			this.NextButton.UseVisualStyleBackColor = true;
			this.NextButton.Anchor = AnchorStyles.Right;
			this.NextButton.Click += NextImage;
			
			this.PreviousButton.Location = new System.Drawing.Point(_form.ClientRectangle.Left + 8, _form.ClientRectangle.Height / 2 - 32);
			this.PreviousButton.Name = "_previousButton";
			this.PreviousButton.Size = new System.Drawing.Size(64, 64);
			this.PreviousButton.TabIndex = 4;
			this.PreviousButton.Text = "\u23f4\ufe0e";
			this.PreviousButton.UseVisualStyleBackColor = true;
			this.PreviousButton.Anchor = AnchorStyles.Left;
			this.PreviousButton.Click += PreviousImage;
			
			// 
			// pictureBox1
			// 
			this.PictureBox.Location = new System.Drawing.Point(8 + 80, form.MainMenuStrip.Height + this.Label.Height);
			this.PictureBox.Name = "pictureBox1";
			this.PictureBox.Size = new System.Drawing.Size(776, 398);
			this.PictureBox.TabIndex = 0;
			this.PictureBox.TabStop = false;
			this.PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.PictureBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			
			Size formSizeWithoutMenu = _form.GetSizeWithoutMenu();
			this.PictureBox.Size = new Size((int) (formSizeWithoutMenu.Width * 0.9) - 144, (int) ((formSizeWithoutMenu.Height - Label.Height)* 0.9));
			// 
			// ImageViewerDesigner
			// 

			form.Controls.Add(this.Label);
			form.Controls.Add(this.PictureBox);
			form.Controls.Add(this.NextButton);
			form.Controls.Add(this.PreviousButton);
			form.SizeChanged += FormSizeChanged;
		}

		private void PreviousImage(object sender, EventArgs e)
		{
			_form.PreviousMedia();
		}

		private void NextImage(object sender, EventArgs e)
		{
			_form.NextMedia();
		}

		private void FormSizeChanged(object sender, EventArgs e)
		{
			Size formSizeWithoutMenu = _form.GetSizeWithoutMenu();
			this.PictureBox.Size = new Size((int) (formSizeWithoutMenu.Width * 0.9) - 144, (int) ((formSizeWithoutMenu.Height - Label.Height)* 0.9));
		}

		public void Open(string path)
		{
			this.PictureBox.Image = new Bitmap(path);
			this.Label.Text = Path.GetFileNameWithoutExtension(path);
		}

		public void Close()
		{
			_form.SizeChanged -= FormSizeChanged;
		}
	}
}