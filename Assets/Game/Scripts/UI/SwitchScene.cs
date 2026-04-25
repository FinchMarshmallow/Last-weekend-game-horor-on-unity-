using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
	public void LoadByName(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void LoadByID(int id)
	{
		SceneManager.LoadScene(id);
	}
}
