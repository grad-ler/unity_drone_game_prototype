using UnityEngine;

public class LR_Loader_Callback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            
            LR_Loader.LoaderCallback();
        }
    }
}
