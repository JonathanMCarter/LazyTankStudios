﻿Shader "Hidden/CRT"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            float scanlineCount;
            float scanlineIntensity;
            float vignetteStrenght;
            float time;
            float scanlineSpeed;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float scanline = mul(sin(scanlineCount*i.uv.y+time*scanlineSpeed),scanlineIntensity);
                col-=scanline;

                float vignette=mul(16.0,i.uv.x*i.uv.y)*(1.0-i.uv.x)*(1.0-i.uv.y);

                col=mul(pow(vignette,vignetteStrenght),col);

                return col;
            }
            ENDCG
        }
    }
}
