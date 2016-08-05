using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Debug;

public class WatsonEnabled : MonoBehaviour
{
	void Start ()
	{
		LogSystem.InstallDefaultReactors();
	}

	//void OnEnable()
	//{
	//	EventManager.Instance.RegisterEventReceiver("OnShowDebug", OnShowDebug);
	//}

	//void OnDisable()
	//{
	//	EventManager.Instance.UnregisterEventReceiver("OnShowDebug", OnShowDebug);
	//}

	//private void OnShowDebug(object[] args)
	//{
	//	Log.Debug("WatsonEnabled", "Event received!");
	//	//DebugConsole.Instance.
	//}
}
