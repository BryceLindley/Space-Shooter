using UnityEngine;

public class RemoteSettingsStartup : MonoBehaviour {

	void Awake()
    {
        //The first is an if statement that checks to see if we are on the bootUp scene.
        //The second if statement checks for Internet connectivity. If there is an Internet
        //connection, we update our player's lives through the RemoteSettings integer value
        //we set on the Dashboard; all of this is wrapped in a lambda.
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            RemoteSettings.Updated += () =>
            {
                GameManager.playerLives = RemoteSettings.GetInt("PlayersStartUpLives", GameManager.playerLives);
            };
        }

    }
}
