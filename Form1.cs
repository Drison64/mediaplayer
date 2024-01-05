using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace projectuwu
{
	public partial class Form1 : Form
	{

		private int _mediaIndex = 0;
		private List<Media> _medias;
		private IMediaModule currentModule;
		private Control controlToKeep;

		
		public Form1()
		{
			InitializeComponent();
			this.DragDrop += new DragEventHandler(this.Form1_DragDrop);
			controlToKeep = menuStrip1;

			_medias = new List<Media>();
		}

		private void Form1_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			foreach (string file in files) Console.WriteLine(file);
		}

		public static IMediaModule CreateMediaModule(string fileExtension)
		{
			if (fileExtension == "wav") return new AudioPlayer();
			if (fileExtension == "mp3") return new AudioPlayer();
			if (fileExtension == "png") return new ImageViewer();
			if (fileExtension == "jpeg") return new ImageViewer();
			if (fileExtension == "jpg") return new ImageViewer();
			if (fileExtension == "gif") return new ImageViewer();
			return null;
		}

		private void HandleFileSelection()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			_medias.Clear();
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				foreach (var fileName in openFileDialog.FileNames)
				{
					_medias.Add(new Media(fileName));
				}
			}
		}

		private void InitializePlayer(int mediaIndex)
		{
			if (_medias.Count == 0) return;
			if (mediaIndex > _medias.Count - 1) return;
			if (mediaIndex < 0) return;
			_mediaIndex = mediaIndex;
			if (currentModule != null) currentModule.Close();
			while (Controls.Count > 1)
			{
				foreach (Control control in this.Controls)
				{
					if (control != controlToKeep)
					{
						Controls.Remove(control);
					}
				}
			}
			currentModule = CreateMediaModule(_medias[_mediaIndex].Path.Split('.').Last());
			if (currentModule == null) return;
			currentModule.Initialize(this);
			currentModule.Open(_medias[_mediaIndex].Path);
		}

		public void NextMedia()
		{
			InitializePlayer(_mediaIndex + 1);
		}

		public void PreviousMedia()
		{
			InitializePlayer(_mediaIndex - 1);
		}
		
		//Open File
		private void openButton_Click(object sender, EventArgs e)
		{
			HandleFileSelection();
			InitializePlayer(0);
		}

		public int MediaIndex
		{
			get => _mediaIndex;
			set => _mediaIndex = value;
		}

		public List<Media> Medias => _medias;

		public Size GetSizeWithoutMenu()
		{
			return new Size(this.ClientSize.Width, this.ClientSize.Height - menuStrip1.Height);
		}
		
		private void closeButton_Click(object sender, EventArgs e)
		{
			if (currentModule != null) currentModule.Close();

			while (Controls.Count > 1)
			{
				foreach (Control control in this.Controls)
				{
					if (control != controlToKeep)
					{
						Controls.Remove(control);
					}
				}
			}
		}
	}
}