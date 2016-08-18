using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using IBM.Watson.DeveloperCloud.Widgets;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
	
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
    public MicrophoneWidget micWidget;
	public bool IsPaused = false;

    [SerializeField]
    private PizzaUIManager m_PizzaUIManager;
	
	Canvas canvas;
	
	void Start()
	{
		canvas = GetComponent<Canvas>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            m_PizzaUIManager.IsPizzaPanelVisible = false;
            Pause();
		}
	}
	
	public void Pause()
	{
        canvas.enabled = !canvas.enabled;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		IsPaused = !IsPaused;
        micWidget.Active = !micWidget.Active;
		Lowpass ();
	}
	
	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			paused.TransitionTo(.01f);
		}
		
		else
			
		{
			unpaused.TransitionTo(.01f);
		}
	}
	
	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
