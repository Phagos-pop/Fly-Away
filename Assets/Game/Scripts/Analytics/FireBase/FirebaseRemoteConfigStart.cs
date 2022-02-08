using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseRemoteConfigStart : MonoBehaviour
{
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Dictionary<string, object> defaults = new Dictionary<string, object>();
    long InterTimeInterval;
    public bool IsInterTimeIntervalPassed = true;

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
        InterTimeInterval = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("Inter_time_interval").LongValue;
        Debug.Log("Значение: " + InterTimeInterval);
    }

    void InitializeFirebase()
    {
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
          .ContinueWithOnMainThread(task => {
              Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
              Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
          });
    }

    public void StartInterstitialTimer()
    {
        StartCoroutine(TimerForInter());
    }   

    private IEnumerator TimerForInter()
    {
        Debug.Log("Timer Start");
        IsInterTimeIntervalPassed = false;
        yield return new WaitForSeconds(InterTimeInterval);
        IsInterTimeIntervalPassed = true;
        Debug.Log("Time Out");
    }
}
