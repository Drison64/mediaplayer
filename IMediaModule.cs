using System.Windows.Forms;

namespace projectuwu
{
	public interface IMediaModule
	{
		void Initialize(Form1 form);
		void Open(string path); 
		void Close();
	}
}