using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AF : MonoBehaviour {
    public static AF instance;
	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        /* Mandatory - set your AppsFlyer’s Developer key. */
        AppsFlyer.setAppsFlyerKey("DdWbxT9VRELdEsZiAcnGea");
        /* For detailed logging */
        /* AppsFlyer.setIsDebug (true); */
# if UNITY_IOS
        /* Mandatory - set your apple app ID
           NOTE: You should enter the number only and not the "ID" prefix */
        AppsFlyer.setAppID("YOUR_APP_ID_HERE");
        AppsFlyer.trackAppLaunch();
# elif UNITY_ANDROID
        /* Mandatory - set your Android package name */
        AppsFlyer.setAppID("com.vela.StickManArcher");
        /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
        AppsFlyer.init("DdWbxT9VRELdEsZiAcnGea", "AppsFlyerTrackerCallbacks");
#endif
    }

    // Update is called once per frame
    void Update () {
    
	}
}
