using UnityEngine;
using UnityEngine.Events;
using PixelCrushers.DialogueSystem.UnityGUI;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;
using Michsky.LSS;

namespace PixelCrushers.DialogueSystem.Demo
{

	/// <summary>
	/// This script provides a rudimentary main menu for the Dialogue System's Demo.
	/// </summary>
	[AddComponentMenu("")] // Use wrapper.
	public class PauseMenu : MonoBehaviour
	{

		[TextArea]
		public string startMessage = "Press Escape for Menu";
		public KeyCode menuKey = KeyCode.Escape;
		public GUISkin guiSkin;
		public bool closeWhenQuestLogOpen = true;
		public bool lockCursorDuringPlay = false;

		public UnityEvent onOpen = new UnityEvent();
		public UnityEvent onClose = new UnityEvent();

		private QuestLogWindow questLogWindow = null;
		private bool isMenuOpen = false;
		private Rect windowRect = new Rect(0, 0, 500, 500);
		private ScaledRect scaledRect = ScaledRect.FromOrigin(ScaledRectAlignment.MiddleCenter, ScaledValue.FromPixelValue(300), ScaledValue.FromPixelValue(320));

		void Start()
		{
			if (questLogWindow == null) questLogWindow = FindObjectOfType<QuestLogWindow>();
			if (!string.IsNullOrEmpty(startMessage)) DialogueManager.ShowAlert(startMessage);
		}

		private void OnDestroy()
		{
			if (isMenuOpen) Time.timeScale = 1;
		}

		void Update()
		{
			if (InputDeviceManager.IsKeyDown(menuKey) && !DialogueManager.isConversationActive && !IsQuestLogOpen())
			{
				SetMenuStatus(!isMenuOpen);
			}
			if (lockCursorDuringPlay)
			{
				CursorControl.SetCursorActive(DialogueManager.isConversationActive || isMenuOpen || IsQuestLogOpen());
			}
		}

		void OnGUI()
		{
			if (isMenuOpen && !IsQuestLogOpen())
			{
				if (guiSkin != null)
				{
					GUI.skin = guiSkin;
				}
				windowRect = GUI.Window(0, windowRect, WindowFunction, "Menu");
			}
		}

		private void WindowFunction(int windowID)
		{
			if (GUI.Button(new Rect(10, 60, windowRect.width - 20, 48), "Quest Log"))
			{
				if (closeWhenQuestLogOpen) SetMenuStatus(false);
				OpenQuestLog();
			}

			//button goes here

			if (GUI.Button(new Rect(10, 160, windowRect.width - 20, 48), "Return to Hubworld"))
			{
				SetMenuStatus(false);
				Scene scene = SceneManager.GetActiveScene();

				// Check if the name of the current Active Scene is your first Scene.
				

                if (scene.name == "HubWorld")
				{
					DialogueManager.ShowAlert("Current on HubWorld");
				}
				else
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthSystem>().TakeDamagePercent(100);
					HudInfoHandler hudInfo = GameObject.FindGameObjectWithTag("HUD").GetComponent<HudInfoHandler>();
					if (hudInfo.deathMenuShown)
					{
						hudInfo.CloseDeathScreen();
					}
					GameObject.FindGameObjectWithTag("LSSManager").GetComponent<LSS_Manager>().LoadScene("HubWorld");
				}
			}
			if (GUI.Button(new Rect(10, 210, windowRect.width - 20, 48), "Quit Game"))
			{
				SetMenuStatus(false);
				Application.Quit();
			}
			/* Original Button Layout
            if (GUI.Button(new Rect(10, 110, windowRect.width - 20, 48), "Save Game"))
            {
                SetMenuStatus(false);
                SaveGame();
            }
            
            if (GUI.Button(new Rect(10, 160, windowRect.width - 20, 48), "Load Game"))
            {
                SetMenuStatus(false);
                LoadGame();
            }
            if (GUI.Button(new Rect(10, 210, windowRect.width - 20, 48), "Clear Saved Game"))
            {
                SetMenuStatus(false);
                ClearSavedGame();
            }
            */
			if (GUI.Button(new Rect(10, 260, windowRect.width - 20, 48), "Close Menu"))
			{
				SetMenuStatus(false);
			}
		}

		public void Open()
		{
			SetMenuStatus(true);
		}

		public void Close()
		{
			SetMenuStatus(false);
		}

		private void SetMenuStatus(bool open)
		{
			isMenuOpen = open;
			if (open) windowRect = scaledRect.GetPixelRect();
			Time.timeScale = open ? 0 : 1;
			if (open) onOpen.Invoke(); else onClose.Invoke();
		}

		private bool IsQuestLogOpen()
		{
			return (questLogWindow != null) && questLogWindow.isOpen;
		}

		private void OpenQuestLog()
		{
			if ((questLogWindow != null) && !IsQuestLogOpen())
			{
				questLogWindow.Open();
			}
		}
	}
}
