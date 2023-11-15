using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForTracking : MonoBehaviour
{

    private bool playerDetected;

    [Header("BoxParameters")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;
    public float detectionDelay;
    public LayerMask detectorLayerMask;

    [Header("ColorParameters")]
    public Color IdleColor = Color.green;
    public Color DetectedColor = Color.red;
    public bool show = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
