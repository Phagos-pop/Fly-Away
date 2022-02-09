
namespace Firebase.Sample.RemoteConfig {
  using Firebase.Extensions;
  using System;
  using System.Threading.Tasks;
  using UnityEngine;


    public
    class UIHandler : MonoBehaviour
    {
        private string logText = "";
        const int kMaxLogSize = 16382;
        Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
        protected bool isFirebaseInitialized = false;

        private long InterTimeInterval;
        public bool IsInterTimeIntervalPassed = true;
        //public Text text;

        protected virtual void Start()
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
        }

        void InitializeFirebase()
        {
            // [START set_defaults]
            System.Collections.Generic.Dictionary<string, object> defaults =
              new System.Collections.Generic.Dictionary<string, object>();

            defaults.Add("Inter_time_interval", 75);

            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
              .ContinueWithOnMainThread(task => {
                  FetchDataAsync();
                  DebugLog("RemoteConfig configured and ready!");
                  isFirebaseInitialized = true;
                  DisplayData();
              });

        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }


        public void DisplayData()
        {
            DebugLog("Current Data:");
            DebugLog("config_test_int: " +
                     Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
                     .GetValue("Inter_time_interval").LongValue);

        }

        public Task FetchDataAsync()
        {
            DebugLog("Fetching data...");
            System.Threading.Tasks.Task fetchTask =
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                TimeSpan.Zero);
            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }
        //[END fetch_async]

        void FetchComplete(Task fetchTask)
        {
            if (fetchTask.IsCanceled)
            {
                DebugLog("Fetch canceled.");
            }
            else if (fetchTask.IsFaulted)
            {
                DebugLog("Fetch encountered an error.");
            }
            else if (fetchTask.IsCompleted)
            {
                DebugLog("Fetch completed successfully!");
            }

            var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
            switch (info.LastFetchStatus)
            {
                case Firebase.RemoteConfig.LastFetchStatus.Success:
                    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                    .ContinueWithOnMainThread(task => {
                        DebugLog(String.Format("Remote data loaded and ready (last fetch time {0}).",
                                   info.FetchTime));
                    });

                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Failure:
                    switch (info.LastFetchFailureReason)
                    {
                        case Firebase.RemoteConfig.FetchFailureReason.Error:
                            DebugLog("Fetch failed for unknown reason");
                            break;
                        case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                            DebugLog("Fetch throttled until " + info.ThrottledEndTime);
                            break;
                    }
                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Pending:
                    DebugLog("Latest Fetch call still pending.");
                    break;
            }
        }

        public void DebugLog(string s)
        {
            print(s);
            logText += s + "\n";

            while (logText.Length > kMaxLogSize)
            {
                int index = logText.IndexOf("\n");
                logText = logText.Substring(index + 1);
            }
        }
    }
}