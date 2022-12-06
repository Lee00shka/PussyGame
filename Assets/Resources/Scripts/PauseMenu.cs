using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] GameObject pauseMenu;
	bool GameIsPaused = false;
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}

		}
	}

	public void Pause()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Resume()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Home(int sceneID)
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(sceneID);
	}
}
