using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Debug;
using IBM.Watson.DeveloperCloud.Services.NaturalLanguageClassifier.v1;

public class WatsonEnabled : MonoBehaviour
{
	void Start ()
	{
		LogSystem.InstallDefaultReactors();
	}

    void OnEnable()
    {
        //EventManager.Instance.RegisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
    }

    void OnDisable()
    {
        //EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
    }

    //private void HandleAirSupportRequest(object[] args)
    //{
    //    EventManager.Instance.SendEvent("OnDebugMessage", (args[0] as ClassifyResult).top_class + ", " + (args[0] as ClassifyResult).topConfidence);
    //    Log.Debug("WatsonEnabled", "AirSupport Event received!");
    //}
}
