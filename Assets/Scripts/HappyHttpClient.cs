using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HappyHttpClient
{
    private readonly ISerializationOption _serializationOption;

    public HappyHttpClient(ISerializationOption serializationOption)
    {
        _serializationOption = serializationOption;
    }

    public async Task<TResultType> Get<TResultType>(string url)
    {
        try
        {
            using var www = UnityWebRequest.Get(url);

            www.SetRequestHeader("Content-Type", _serializationOption.ContentType);

            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}");

            var result = _serializationOption.Deserialize<TResultType>(www.downloadHandler.text);

            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
            return default;
        }
    }
}