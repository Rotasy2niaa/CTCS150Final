Shader "Unlit/FrameShader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Alpha ("Alpha", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct a2v
            {
                float4 vertexOS : POSITION;
            };

            struct v2f
            {
                float4 vertexCS : SV_POSITION;
            };

            float4 _Color;
            float _Alpha;

            v2f vert (a2v v)
            {
                v2f o;
                o.vertexCS = UnityObjectToClipPos(v.vertexOS);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                return float4(_Color.xyz, _Alpha);
            }
            ENDCG
        }
    }
}
