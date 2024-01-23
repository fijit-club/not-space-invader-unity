using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using TMPro;

namespace FijitAddons
{
    public class NativeAPI
    {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        public static extern void sendMessageToMobileApp(string message);
#endif
    }


    [System.Serializable]
    public class Attributes
    {
        public string id;
        public int level;
    }

    [System.Serializable]
    public class Asset
    {
        public string id;
        public Attributes[] attributes;

        public Asset(string assetID)
        {
            id = assetID;

        }
    }

    [System.Serializable]
    public class Data
    {
        public List<Asset> assets;
        public string saveData;
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public int coins;
        public bool volumeBg = true;
        public bool volumeSfx = true;
        public int highScore = 0;
        public Data data;
       
        public static PlayerInfo CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<PlayerInfo>(jsonString);
        }
    }




    public class Bridge : MonoBehaviour
    {
        public PlayerInfo thisPlayerInfo;
        private static Bridge instance;
        public int coinsCollected = 0;



#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setScore(int score);

    //      [DllImport("__Internal")]
    // private static extern void registerVisibilityChangeEvent();


        [DllImport("__Internal")]
        private static extern void buyAsset(string assetId);

        [DllImport("__Internal")]
        private static extern void updateCoins(int coinsChange);

        [DllImport("__Internal")]
        private static extern void updateExp(int expChange);
        
        // [DllImport("__Internal")]
        // private static extern void upgradeAsset(string assetID, string attributeID, int level);

        [DllImport("__Internal")]
        private static extern void load();

        [DllImport("__Internal")]
        private static extern void setSavedata(string savedata);

        [DllImport("__Internal")]
        private static extern void vibrate(bool isLong);
#endif

        public static Bridge GetInstance()
        {
            return instance;
        }

        private void Start()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
        
#endif
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
                Debug.Log("Loaded");
#if UNITY_WEBGL && !UNITY_EDITOR
            load();
#endif

                //SendInitSendInitialData("{ \"coins\": 6969, \"data\":{\"cars\": [{\"id\": \"default-car\"}]}   }");
            }
            else
                Destroy(this);

            GameLoaded();


        }


        public void AddExp(int exp)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            updateExp(exp);
#endif
        }

        public void GameLoaded()// tells app game is ready to play
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            load();
#endif
        }

        public void ButtonPressed()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
                {
                    jc.CallStatic("sendMessageToMobileApp", "The button has been tapped!");
                }
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IOS && !UNITY_EDITOR
                NativeAPI.sendMessageToMobileApp("The button has been tapped!");
#endif
            }
        }

        public void SendScore(int score)
        {

            Debug.Log(coinsCollected + "sent coin");
#if UNITY_WEBGL && !UNITY_EDITOR
            //updateCoins(coinsCollected);
#endif
#if UNITY_WEBGL && !UNITY_EDITOR

            setScore(score);
#elif UNITY_EDITOR
            Debug.Log("sendingscore" + score);
#endif
        }

        public void Mute()
        {
            //SoundManager.Mute();
            AudioListener.volume = 0;
        }

        public void Unmute()
        {
            //SoundManager.Unmute();
            AudioListener.volume = 1;
        }

        public void Replay() // set funtion for game replay here 
        {
            coinsCollected = 0; // REPLAY GOES HERE
        }

        public void SendInitialData(string json)
        {
            thisPlayerInfo = PlayerInfo.CreateFromJSON(json);
            Debug.Log(json);

            if (thisPlayerInfo.volumeSfx)
            {
                Silence("false");
            }
            else
            {
                Silence("true");

            }

            //add initial asset check here 

            //Replay();
            //Events.CoinsCountChanged.Call();
        }
        public void UpdateCoins(int value) //add buffer coisn to app at end of game 
        {
            thisPlayerInfo.coins += value;
            coinsCollected += value;
            if (value > 0)
            {
                Debug.Log(value);
#if UNITY_WEBGL && !UNITY_EDITOR
            updateCoins(coinsCollected);
#endif
            }
        }

        public void AddCoin()// add single coin to 
        {
            thisPlayerInfo.coins++;
        }


        public void AddCoin(int value)//add coins to buffer 
        {
            thisPlayerInfo.coins += value;
            coinsCollected += value;
            //Debug.Log(value);

        }


        public void BuyAsset(string assetID)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
                    buyAsset(spaceshipID);
#endif
            AddAsset(assetID);
        }

        public void AddAsset(string assetID)
        {
            Asset addedAsset = new Asset(assetID);
            addedAsset.id = assetID;



            Debug.Log("added new spaceship " + addedAsset.id);

            thisPlayerInfo.data.assets.Add(addedAsset);
        }

        [ContextMenu("Send Initial Test")] //add initialdata tests here 
        public void SendTextData()
        {
            //SendInitialData("{\"coins\": 123,\"playerData\": {\"shootPower\":25,\"shootSpeed\":20}}");
            //SendInitialData("{\"coins\":3400,\"data\":{\"cannons\":[{\"id\":\"bvb-cannon-1\",\"attributes\":[{\"bvb-cannon-1-speed\":0,\"bvb-cannon-1-power\":0}]}]}}");
            //SendInitialData("{\"coins\":34,\"data\":{\"cars\":[{\"id\":\"bvb-cannon-1\",\"attributes\":[{\"id\":\"bvb-cannon-1-speed\",\"level\":91},{\"id\":\"bvb-cannon-1-power\",\"level\":92}]},{\"id\":\"bvb-cannon-2\",\"attributes\":[{\"id\":\"bvb-cannon-2-speed\",\"level\":3},{\"id\":\"bvb-cannon-2-power\",\"level\":2}]}]}}");
            //SendInitialData("{\"coins\": 3000,\"data\": null}");
            //Debug.Log(JsonUtility.ToJson( thisPlayerInfo.data));
            //Debug.Log( thisPlayerInfo.data);
            SendInitialData("{\"coins\":384696,\"volumeBg\":true,\"volumeSfx\":true,\"highScore\":949,\"data\":{\"assets\":[{\"id\":\"space-dual-shooter-ship\",\"attributes\":[]},{\"id\":\"test-spaceship-2\",\"attributes\":[]}],\"saveData\":\"saste nashe\"}}");
        }

        [ContextMenu("Do Something2")]
        public void SendTextData2()
        {
            //SetShootSpeed(50);

        }

        public int GetCoins()
        {
            return thisPlayerInfo.coins;
        }


        public void Silence(string silence)// called by app when goes in bg or is closed 
        {
            if (silence == "true")
                AudioListener.pause = true;

            if (silence == "false")
                AudioListener.pause = false;
            // Or / And
            //AudioListener.volume = silence ? 0 : 1;

            System.Console.WriteLine("called silence " + silence);

        }


    }
}
