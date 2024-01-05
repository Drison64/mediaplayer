using System.IO;

namespace projectuwu
{
	public struct Media
	{
		public Media(string path)
		{
			Path = path;
		}

		public string Path { get; }
	}
}