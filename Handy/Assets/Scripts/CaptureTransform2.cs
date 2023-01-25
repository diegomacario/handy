using UnityEngine;

// Tags a component in the view hierarchy to be captured by `PlaybackManager`
public class CaptureTransform2 : MonoBehaviour
{
    // The name of the component to capture (will default to the component's name)
    public string captureName;

    // Whether to capture the world transform or the local transform
    public bool captureWorldTransform = false;

    // Whether this is the first transform that's being captured or not
    public bool firstCapture = true;

    private void Start()
    {
        if (string.IsNullOrEmpty(captureName))
        {
            captureName = name;
        }
    }
}
