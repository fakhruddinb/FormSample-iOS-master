using System;

namespace FormSample
{
	public interface IProgressService
	{
		void Show();
		void Show(string message);
		void Dismiss();

	}
}

