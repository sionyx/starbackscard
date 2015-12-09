using System.Runtime.CompilerServices;

namespace Starbucks.ViewModels
{
    public class BaseViewModel : BindableBase
	{
		private bool _inProgress;
		public bool InProgress
		{
			get { return _inProgress; }
			set
			{
				_inProgress = value;
				OnPropertyChanged();
			}
		}
	}
}
