Shader "Hidden/CRT"
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
            float noiseSize;
            float noiseAmount;
            float interlaceStrenght;
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
            float random(half2 uv)
			{
				return frac(sin(dot(uv, half2(15.1511, 42.5225))) * 12341.51611 * sin(time * 0.03));
			}
            float noise(half2 uv)
			{
				half2 i = floor(uv);
				half2 f = frac(uv);
				float a = random(i);
				float b = random(i + half2(1., 0.));
				float c = random(i + half2(0, 1.));
				float d = random(i + half2(1., 1.));
				half2 u = smoothstep(0., 1., f);
				return lerp(a, b, u.x) + (c - a) * u.y * (1. - u.x) + (d - b) * u.x * u.y;
			}
            sampler2D _MainTex;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                //Interlace WIP
                col.x = tex2D(_MainTex, i.uv + float2(interlaceStrenght/1000*random(time),0)).x;
                col.y = tex2D(_MainTex, i.uv - float2(interlaceStrenght/1000*random(time),0)).y;
                col.z = tex2D(_MainTex, i.uv + float2(interlaceStrenght/1000*random(time),0)).z;
                ///

                col-=mul(sin(scanlineCount*i.uv.y+time*scanlineSpeed),scanlineIntensity/100);
                float vignette=mul(16.0,i.uv.x*i.uv.y)*(1.0-i.uv.x)*(1.0-i.uv.y);
                col=mul(pow(vignette,vignetteStrenght),col);

                
                return lerp(col,fixed(noise(i.uv * noiseSize)), noiseAmount/100);
            }
            ENDCG
        }
    }
}
