using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	private KeysCodes _keysKodes;

	[SerializeField] private Slider sliderMouseSentity;
	[SerializeField] private TMP_InputField inputFieldMouseSentity;

	[SerializeField] private TextMeshProUGUI[] keysCodesTextButton = new TextMeshProUGUI[10];

	[SerializeField] private TextMeshProUGUI applay;
	[SerializeField] private string applayStr;

	private Coroutine _waitPressButtonC;

	public void OnEnable()
	{
		Open();
	}

	public void Open()
	{
		_keysKodes = ConfigKeyCode.Current;

		sliderMouseSentity.value = _keysKodes.SentityMause.x;
		inputFieldMouseSentity.text = _keysKodes.SentityMause.x.ToString();

		keysCodesTextButton[0].text = _keysKodes.Forward.ToString();
		keysCodesTextButton[1].text = _keysKodes.Back.ToString();
		keysCodesTextButton[2].text = _keysKodes.Left.ToString();
		keysCodesTextButton[3].text = _keysKodes.Right.ToString();
		keysCodesTextButton[4].text = _keysKodes.Interact.ToString();
		keysCodesTextButton[5].text = _keysKodes.Inventory.ToString();
		keysCodesTextButton[6].text = _keysKodes.Stels.ToString();
		keysCodesTextButton[7].text = _keysKodes.Sprint.ToString();
		keysCodesTextButton[8].text = _keysKodes.Jump.ToString();
		keysCodesTextButton[9].text = _keysKodes.Pause.ToString();
	}

	public void ResetSettings()
	{
		ConfigKeyCode.Current = ConfigKeyCode.Default;
		Open();
	}

	public void Applay()
	{
		ConfigKeyCode.Current = _keysKodes;
		applay.text = applayStr;
	}

	private void CheckUpdate()
	{
		if (ConfigKeyCode.Current.Equals(_keysKodes))
		{
			applay.text = applayStr;
		}
		else
		{
			applay.text = $"{applayStr} *";
		}
	}

	public void SliderMouseSentityMove()
	{
		_keysKodes.SentityMause = Vector2.one * sliderMouseSentity.value;
		inputFieldMouseSentity.text = sliderMouseSentity.value.ToString();
		CheckUpdate();
	}
	
	public void InputFieldMouseSentityInput()
	{
		if(float.TryParse(inputFieldMouseSentity.text, out float i))
		{
			_keysKodes.SentityMause = Vector2.one * i;
			sliderMouseSentity.value = i;
		}
		CheckUpdate();
	}

	public void SetKeyKode(int i)
	{
		if(_waitPressButtonC != null)
		{
			StopCoroutine(_waitPressButtonC);
		}

		_waitPressButtonC = StartCoroutine(WaitPressButton(i));
	}

	private IEnumerator WaitPressButton(int i)
	{
		keysCodesTextButton[i].text = "Enter your button";

		yield return null;

		KeyCode? pressedKey = null;
		while (pressedKey == null)
		{
			foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(key))
				{
					pressedKey = key;
					break;
				}
			}
			yield return null;
		}

		keysCodesTextButton[i].text = pressedKey.Value.ToString();

		switch (i)
		{
			case 0: _keysKodes.Forward = pressedKey.Value; break;
			case 1: _keysKodes.Back = pressedKey.Value; break;
			case 2: _keysKodes.Left = pressedKey.Value; break;
			case 3: _keysKodes.Right = pressedKey.Value; break;
			case 4: _keysKodes.Interact = pressedKey.Value; break;
			case 5: _keysKodes.Inventory = pressedKey.Value; break;
			case 6: _keysKodes.Stels = pressedKey.Value; break;
			case 7: _keysKodes.Sprint = pressedKey.Value; break;
			case 8: _keysKodes.Jump = pressedKey.Value; break;
			case 9: _keysKodes.Pause = pressedKey.Value; break;
		}

		_waitPressButtonC = null;
		CheckUpdate();
	}

	private bool IsMouseKey(KeyCode key)
	{
		return key >= KeyCode.Mouse0 && key <= KeyCode.Mouse6;
	}
}
