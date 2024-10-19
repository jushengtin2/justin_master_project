Shader "Custom/RearViewWithOutline"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {} // Render Texture
        _OutlineColor ("Outline Color", Color) = (0,0,0,1) // 边框颜色
        _OutlineWidth ("Outline Width", Range(0.001, 0.1)) = 0.01 // 边框宽度
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                
                // 计算边框
                float border = _OutlineWidth;
                if (uv.x < border || uv.x > 1.0 - border || uv.y < border || uv.y > 1.0 - border)
                {
                    return _OutlineColor; // 边框颜色
                }

                // 渲染后视镜的Render Texture
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
