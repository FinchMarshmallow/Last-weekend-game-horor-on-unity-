using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	private KeysCodes _keysKodes;

	[SerializeField] private Slider sliderMouseSentity;
	[SerializeField] private TMP_InputField inputFieldMouseSentity;

	private void Open()
	{
		_keysKodes = ConfigKeyCode.Current;
	}
}
