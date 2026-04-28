using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
	public static bool IsPause { get; private set; }

	[SerializeField] private UnityEvent open, close;

	public void OpenPause()
	{
		IsPause = true;
		open?.Invoke();
		PlayerInputsData.IsPause = true;
	}

	public void ClosePause()
	{
		IsPause = false;
		close?.Invoke();
		PlayerInputsData.IsPause = false;
	}

	private void Update()
	{
		if (IsPause)
			return;

		if(Input.GetKeyDown(ConfigKeyCode.Current.Pause))
			OpenPause();
	}

}
