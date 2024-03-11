Shader "Unlit/LightShader"
{
    Properties
    {
        _ObjectColor("Object Color", Color) = (1, 1, 1, 1)
        _LColor("Light Color", Color) = (1, 1, 1, 1)
        _Ambient("Ambient", Range(0, 3)) = 0
        _Diffuse("Diffuse", Range(0, 4)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 500

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _ObjectColor;
            float4 _LColor;
            float _Ambient;
            float _Diffuse;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 wPos : TEXCOORD0;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.wPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 ambient = _LColor * _Ambient;
                float4 diffuse = _LColor * dot(i.normal, _WorldSpaceLightPos0) * _Diffuse;
                fixed4 col = _ObjectColor * (ambient + diffuse); 
                return col;
            }
            ENDCG
        }
    }
}
