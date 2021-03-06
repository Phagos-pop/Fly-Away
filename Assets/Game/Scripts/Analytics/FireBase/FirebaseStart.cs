using UnityEngine;

public class FirebaseStart : MonoBehaviour
{
    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(Task =>
        {
            Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("game_start");
        });        
    }
}
