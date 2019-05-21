using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(Camera))]
public class SelfieTaker : MonoBehaviour
{
    [SerializeField] bool take;
    [SerializeField] StuffMaker stuffMaker;

    Camera cam;//camera used specifically for "selfie" taking
    RenderTexture rt;
    Texture2D frameBufferCapture;

    int selfieId = 0;

    public int captureWidth = 1000;
    public int captureHeight = 1000;



    void Awake()
    {
        cam = GetComponent<Camera>();
        //init this cam

        rt = new RenderTexture(captureWidth, captureHeight, 32, RenderTextureFormat.ARGB32);
        cam.targetTexture = rt;

       cam.enabled = false;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //test
        if (take)
        {
            take = false;
            var selected = stuffMaker.GetOne();

            StartCoroutine(TakeOneSelfie(selected.transform));
        }
    }

    //use coroutine to avoid performance impact
    public IEnumerator TakeOneSelfie(Transform target, System.Action<string> onFinish = null)
    {

        cam.Render();

        string filePath =  Application.dataPath + "/selfie" + (selfieId++) + ".jpg";



        frameBufferCapture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false, true);




        //move camera to position. in the future maybe randomize the position a bit so each take is from a different angle?
        transform.position = target.transform.position + new Vector3(0, -10, 0);
        transform.LookAt(target);


        RenderTexture.active = rt;
        frameBufferCapture.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
        RenderTexture.active = null;


        try
        {
            //@Mark: you proabbly can do something different here to get the uri more directly
            File.WriteAllBytes(filePath, frameBufferCapture.EncodeToJPG());
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            yield break;

        }

        if (onFinish != null)
        {
            onFinish(filePath);
        }


        yield break;


    }








}
