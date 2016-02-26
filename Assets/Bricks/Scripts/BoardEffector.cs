using UnityEngine;
using System.Collections;

public class BoardEffector : MonoBehaviour 
{
   // [SerializeField]
   // Material heightmapMaterial;

   // [SerializeField] Vector2 textureSize;

	[Range(0.0f, 0.1f)]
	[SerializeField] float touchAreaMultiplicator = 0.05f;

//	[Range(0.0f, 40.0f)]
//	[SerializeField] float forceMultiplicator = 20;

    [Range(0.0f, 10.0f)]
    [SerializeField] float fading = 1;

    public float magicNumber = 1.8f;

//	BaseTile[] allTiles;
    [SerializeField] Texture2D heightTex;

    Color[] allPixels;
    bool touchActive = false;
	Vector2 touchPosition = new Vector2(-1, -1);
	int textureWidth = 0;
	int textureHeight = 0;

	void Awake()
	{
//		TilesGenerator.onCreateTiles += OnCreateTiles;
		TouchController.onTouch += OnTouch;
        TouchController.onBeginTouch += OnBeginTouch;
        TouchController.onEndTouch += OnEndTouch;
	}

    void Start()
    {
		textureWidth = heightTex.width;
		textureHeight = heightTex.height;

        allPixels = heightTex.GetPixels();
        for (int i = 0; i < allPixels.Length; i++)
        {
            allPixels[i].a = 0;
        }

        Debug.Log("SetPixels");
        heightTex.SetPixels(allPixels);

        heightTex.Apply();

//		Loom.RunAsync(SetTexture);
    }
	
	void OnDestroy()
	{
//		TilesGenerator.onCreateTiles -= OnCreateTiles;
		TouchController.onTouch -= OnTouch;
        TouchController.onBeginTouch -= OnBeginTouch;
        TouchController.onEndTouch -= OnEndTouch;
	}
				

	public void SetArea(float area)
	{
		touchAreaMultiplicator = area;
	}

	public void SetForce(float force)
	{
//		forceMultiplicator = force;
	}

//	void OnCreateTiles(BaseTile[] allTiles)
//	{
//		this.allTiles = allTiles;
//	}

    void OnBeginTouch()
    {
        touchActive = true;
    }

	void OnTouch(Vector3 viewportPosition)
	{
		touchPosition = viewportPosition;
		Loom.RunAsync(SetTextureForce);

//		SetTextureForce(viewportPosition);

//		foreach (BaseTile tile in allTiles)
//		{
//			float force = GetForce(tile.viewportPosition, viewportPosition);
//			tile.SetForce(force);
//		}
	}

    void OnEndTouch()
    {
        touchActive = false;
		touchPosition = new Vector2(-1, -1);
    }

    void Update()
    {
//        if (!touchActive)
        {
            Fading();
        }

		SetTexture();
    }

    void Fading()
    {
        for (int i = 0; i < allPixels.Length; i++)
        {
            allPixels[i].a = Mathf.Clamp01(allPixels[i].a - Time.deltaTime * fading);
        }
    }

    void SetTextureForce()
    {
		if (touchPosition == new Vector2(-1, -1))
		{
			return;
		}

        for (int i = 0; i < allPixels.Length; i++)
        {
			float force = GetForce(ConvertArrayIndexToViewportPosition(i, textureWidth, textureHeight), touchPosition);
            allPixels[i].a = Mathf.Clamp(allPixels[i].a + force, 0, 1);
        }
    }

	void SetTexture()
	{
		heightTex.SetPixels(allPixels);
		heightTex.Apply();
	}

    Vector2 ConvertArrayIndexToViewportPosition(int index, int width, int height)
    {
        Vector2 position = Vector2.zero;

        position.y = index / width;
        position.x = index % width;

        position.x = width - position.x;

        position.x = position.x / width;
        position.y = position.y / height;


        return position;
    }

	float GetForce(Vector2 tilePos, Vector2 touchPos)
	{
		float distance = Vector2.Distance(tilePos, touchPos) / touchAreaMultiplicator;
		if (distance == 0)
		{
            return 1;
		}

        float force = Mathf.Clamp01(Mathf.Pow(1 / distance, magicNumber));
//		float force = Mathf.Clamp(forceMultiplicator / distance, 0.0f, forceMultiplicator);

//        if (force < 0.2f)
//        {
//            force = 0;
//        }

		return force;
	}

}
