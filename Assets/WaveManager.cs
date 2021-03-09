using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{   
    public float water_level;
    private float noise = 0.0f;
    private Vector2 uv;

    // Should match WaterShader amplitude for best result.
    public float WaveAmplitude = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    public float getLevel(Vector2 position) {

        // Get offset for wave.
        Unity_TilingAndOffset_float(position, new Vector2(0.3f, 0.3f), Shader.GetGlobalVector("_Time")*0.1f, out uv);

        //Debug.Log("Current time " + Shader.GetGlobalVector("_Time")*0.1f);
        
        // Calculate wave height.
        noise = getGradientNoise(uv, WaveAmplitude);

        return noise;
    }

    // Mimics the Tiling and Offset node used to calculate wave offset for WaterShader.
    void Unity_TilingAndOffset_float(Vector2 UV, Vector2 Tiling, Vector2 Offset, out Vector2 Out)
    {   
        Out = UV * Tiling + Offset;
    }

    
    // Mimics the GradientNoise node used in calculating waves for WaterShader PBR.
    Vector2 gradientNoisedir(Vector2 p)
    {
        p = new Vector2(p.x % 289, p.y % 289);
        float x = (34 * p.x + 1) * p.x % 289 + p.y;
        x = (34 * x + 1) * x % 289;
        x = ((x / 41)%1) * 2 - 1;
        return (new  Vector2(x - Mathf.Floor(x + 0.5f), Mathf.Abs(x) - 0.5f)).normalized;
    }
    
    float gradientNoise(Vector2 p)
    {
        Vector2 ip = new Vector2(Mathf.Floor(p.x), Mathf.Floor(p.y));
        Vector2 fp = new Vector2(p.x%1, p.y%1);
        float d00 = Vector3.Dot(gradientNoisedir(ip), fp);
        float d01 = Vector3.Dot(gradientNoisedir(ip + new Vector2(0, 1)), fp - new Vector2(0, 1));
        float d10 = Vector3.Dot(gradientNoisedir(ip + new Vector2(1, 0)), fp - new Vector2(1, 0));
        float d11 = Vector3.Dot(gradientNoisedir(ip + new Vector2(1, 1)), fp - new Vector2(1, 1));
        fp = fp * fp * fp * (fp * (fp * 6 - new Vector2(15,15)) + new Vector2(10, 10));
        return Mathf.Lerp(Mathf.Lerp(d00, d01, fp.y), Mathf.Lerp(d10, d11, fp.y), fp.x);
    }
    
    float getGradientNoise(Vector2 uv, float WaveAmplitude)
    {
        return gradientNoise(uv * WaveAmplitude) + 0.5f; //relevant to the transfer
    }

}
