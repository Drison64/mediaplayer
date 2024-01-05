using System;
using System.Drawing;
using System.Windows.Forms;
using TagLib.Mpeg;

namespace projectuwu
{
	public class VideoPlayer : IMediaModule, IAudioMedia
	{
		private PictureBox PictureBox;
		private Label Label;
		private Form1 _form;
		private Timer _timer;
		
		public void Initialize(Form1 form)
		{
			this.PictureBox = new System.Windows.Forms.PictureBox();
			this.Label = new System.Windows.Forms.Label();
			this._timer = new Timer();
			
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
			// 
			// pictureBox1
			// 
			this.PictureBox.Location = new System.Drawing.Point(8, form.MainMenuStrip.Height + this.Label.Height);
			this.PictureBox.Name = "pictureBox1";
			this.PictureBox.Size = new System.Drawing.Size(776, 398);
			this.PictureBox.TabIndex = 0;
			this.PictureBox.TabStop = false;
			this.PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.PictureBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			// 
			// ImageViewerDesigner
			// 

			form.Controls.Add(this.Label);
			form.Controls.Add(this.PictureBox);
			form.SizeChanged += FormSizeChanged;
			_timer.Tick += Tick;
		}

		private void Tick(object sender, EventArgs e)
		{
		}

		private void FormSizeChanged(object sender, EventArgs e)
		{
			Size formSizeWithoutMenu = _form.GetSizeWithoutMenu();
			this.PictureBox.Size = new Size((int) (formSizeWithoutMenu.Width * 0.9), (int) ((formSizeWithoutMenu.Height - Label.Height)* 0.9));
		}

		public void Open(string path)
		{
			File file = new File(path);
			VideoHeader header = new VideoHeader(file, 0l);

			_timer.Interval = (int) Math.Floor(1000 / header.VideoFrameRate);

			this.PictureBox.Image = new Bitmap(path);
		}

		public void Close()
		{
			_form.SizeChanged -= FormSizeChanged;
		}

		public void Seek(int position)
		{
			throw new NotImplementedException();
		}

		public void SetVolume(float volume)
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}
	}
}