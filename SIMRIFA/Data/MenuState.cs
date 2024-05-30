using Microsoft.AspNetCore.Components;

namespace SIMRIFA.Data
{
	public class MenuState :  ComponentBase
	{
		public bool ShowMenu { get; set; } = true;

		public event Action OnChange;

		public void SetMenuVisibility(bool isVisible)
		{
			ShowMenu = isVisible;
			NotifyStateChanged();
		}

		private void NotifyStateChanged() => OnChange?.Invoke();
	}
}
