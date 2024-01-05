using System;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;

namespace projectuwu
{
	public class AudioPlayer : IAudioMedia
	{
		
		private Timer _timer;
		private WaveOutEvent _player;
		private AudioFileReader _audioFile;
		private Form1 _form;
		
		private ProgressBar _progressBar1;
		private Label _songName;
		private Button _previousButton;
		private Button _playPauseButton;
		private Button _nextButton;
		
		#region Components
		public void Initialize(Form1 form)
		{
			this._songName = new System.Windows.Forms.Label();
			this._previousButton = new System.Windows.Forms.Button();
			this._playPauseButton = new System.Windows.Forms.Button();
			this._nextButton = new System.Windows.Forms.Button();
			this._progressBar1 = new System.Windows.Forms.ProgressBar();
			
			_form = form;
			_player = new WaveOutEvent();
			_player.PlaybackStopped += PlaybackStopped;
			_timer = new Timer();
			_timer.Interval = 16;
			_timer.Tick += Tick;
			_previousButton.Click += PreviousSong;
			_nextButton.Click += NextSong;
			_progressBar1.Click += Seek;
			// 
			// songName
			// 
			this._songName.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this._songName.Location = new System.Drawing.Point(8, form.MainMenuStrip.Height);
			this._songName.Name = "_songName";
			this._songName.Size = new System.Drawing.Size(_form.ClientSize.Width - _songName.Location.X*2, 42);
			this._songName.TabIndex = 1;
			this._songName.Text = "label1";
			this._songName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			// 
			// previousButton
			// 
			this._previousButton.Location = new System.Drawing.Point(_form.ClientRectangle.Width / 2 - 32 - 80, _form.ClientRectangle.Bottom - 72);
			this._previousButton.Name = "_previousButton";
			this._previousButton.Size = new System.Drawing.Size(64, 64);
			this._previousButton.TabIndex = 2;
			this._previousButton.Text = "\u23f4\ufe0e";
			this._previousButton.UseVisualStyleBackColor = true;
			this._previousButton.Anchor = AnchorStyles.Bottom;
			// 
			// playPauseButton
			// 
			this._playPauseButton.Location = new System.Drawing.Point(_form.ClientRectangle.Width / 2 - 32, _form.ClientRectangle.Bottom - 72);
			this._playPauseButton.Name = "_playPauseButton";
			this._playPauseButton.Size = new System.Drawing.Size(64, 64);
			this._playPauseButton.TabIndex = 3;
			this._playPauseButton.Text = "button2";
			this._playPauseButton.UseVisualStyleBackColor = true;
			this._playPauseButton.Anchor = AnchorStyles.Bottom;
			this._playPauseButton.Click += PlayPauseButtonOnClick;
			// 
			// nextButton
			// 
			this._nextButton.Location = new System.Drawing.Point(_form.ClientRectangle.Width / 2 - 32 + 80, _form.ClientRectangle.Bottom - 72);
			this._nextButton.Name = "_nextButton";
			this._nextButton.Size = new System.Drawing.Size(64, 64);
			this._nextButton.TabIndex = 4;
			this._nextButton.Text = "\u23f5\ufe0e";
			this._nextButton.UseVisualStyleBackColor = true;
			this._nextButton.Anchor = AnchorStyles.Bottom;
			this._nextButton.Click += NextSong;
			// 
			// progressBar1
			// 
			this._progressBar1.Location = new System.Drawing.Point(8, 109);
			this._progressBar1.Name = "_progressBar1";
			this._progressBar1.Size = new System.Drawing.Size(_form.ClientSize.Width - _progressBar1.Location.X*2, 18);
			this._progressBar1.TabIndex = 5;
			this._progressBar1.Maximum = 10000;
			this._progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			form.Controls.Add(_progressBar1);
			form.Controls.Add(_nextButton);
			form.Controls.Add(_playPauseButton);
			form.Controls.Add(_previousButton);
			form.Controls.Add(_songName);
		}

		private void PlayPauseButtonOnClick(object sender, EventArgs e)
		{
			if (_player.PlaybackState == PlaybackState.Playing) _player.Pause();
			else Play();
		}

		#endregion

		private void PlaybackStopped(object sender, EventArgs e)
		{
			_timer.Stop();
		}

		private void Seek(object sender, EventArgs e)
		{

			int left = _progressBar1.Left;

			_audioFile = new AudioFileReader(_form.Medias[_form.MediaIndex].Path);
			_audioFile.CurrentTime = TimeSpan.FromSeconds(((double) _form.PointToClient(Control.MousePosition).X - left) / _progressBar1.Width * _audioFile.TotalTime.TotalSeconds);
			_player.Stop();
			_player.Init(_audioFile);
			Play();
			
		}

		private void Play()
		{
			_player.Play();
			_timer.Start();
			OnChangeStream();
		}

		private void Tick(object sender, EventArgs e)
		{
			if (_audioFile == null) return;
		
			if (_player.PlaybackState == PlaybackState.Playing) _playPauseButton.Text = "\u23f8\ufe0e";
			else _playPauseButton.Text = "\u23f5\ufe0e";
			
			int value = (int) (_audioFile.CurrentTime.TotalMilliseconds /
				_audioFile.TotalTime.TotalMilliseconds * 1000);
			if (value > 10000)
			{
				_timer.Stop();
				_player.Stop();
				_form.NextMedia();
				return;
			}
			_progressBar1.Value = value;
		}

		private void OnChangeStream()
		{
			_songName.Text = new FileInfo(_form.Medias[_form.MediaIndex].Path).Name;
		}

		private void PreviousSong(object sender, EventArgs e)
		{
			_form.PreviousMedia();
		}
		
		private void NextSong(object sender, EventArgs e)
		{
			_form.NextMedia();
		}

		public void Open(string path)
		{
			_audioFile = new AudioFileReader(path);
			_player.Init(_audioFile);
			_player.Play();
			_timer.Start();
			OnChangeStream();
		}

		public void Close()
		{
			_player.Stop();
			_timer.Stop();
		}

		public void Seek(int position)
		{
			throw new NotImplementedException();
		}

		public void SetVolume(float volume)
		{
			_player.Volume = volume;
		}

		public void Stop()
		{
			Close();
		}
	}
}