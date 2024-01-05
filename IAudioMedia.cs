namespace projectuwu
{
	public interface IAudioMedia : IMediaModule, ISeekable
	{
		void SetVolume(float volume);
		void Stop();
	}
}