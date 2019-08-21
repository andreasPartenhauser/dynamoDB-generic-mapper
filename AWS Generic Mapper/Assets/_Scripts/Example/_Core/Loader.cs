using UnityEngine;
using Amazon;
using DynamoDBMapper.Backend.Client;

public class Loader : MonoBehaviour
{
    void Awake()
    {
        SetupAWS();
    }

    private void SetupAWS()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        gameObject.AddComponent<AWSPlayerClient>();
    }
}