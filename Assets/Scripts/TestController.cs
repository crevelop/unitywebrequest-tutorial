using UnityEngine;

public class TestController : MonoBehaviour
{
    [ContextMenu("Test Get")]
    public async void TestGet()
    {
        var url = "https://jsonplaceholder.typicode.com/todos/1";

        var httpClient = new HappyHttpClient(new JsonSerializationOption());
        var result = await httpClient.Get<User>(url);
    }
}
