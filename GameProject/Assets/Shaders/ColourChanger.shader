Shader "Custom/ColourChanger"
{
    Properties
    {
        _MainTex("Sprite", 2D) = "white" {}
        _Palette("Palette", 2D) = "white" {}


        _TexCol1("Color 1", Color) = (0,0,0,1)
        _TexCol2("Color 2", Color) = (0,0,0,1)
        _TexCol3("Color 3", Color) = (0,0,0,1)
        _TexCol4("Color 4", Color) = (0,0,0,1)

    }
    SubShader
    {
			// No culling or depth
			Cull Off ZWrite Off ZTest Always

        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Transparent+1"
        }

        Pass
        {
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma multi_compile DUMMY PIXELSNAP_ON




        sampler2D _MainTex;
		sampler2D _Palette;


        float4 _TexCol1;
        float4 _TexCol2;
        float4 _TexCol3;
        float4 _TexCol4;



        struct Vertex
        {
            float4 vertex : POSITION;
            float2 uv_MainTex : TEXCOORD0;
        };

        struct Fragment
        {
            float4 vertex : POSITION;
            float2 uv_MainTex : TEXCOORD0;
        };


        Fragment vert(Vertex v)
        {
            Fragment o;

            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv_MainTex = v.uv_MainTex;

            return o;
        }




		// Actually does the colour changing stuff
        float4 frag(Fragment IN) : COLOR
        {

            half4 c = tex2D(_MainTex, IN.uv_MainTex);

			half4 Pal1 = tex2D(_Palette, float2(0,1));
			half4 Pal2 = tex2D(_Palette, float2(1,1));
			half4 Pal3 = tex2D(_Palette, float2(0,0));
			half4 Pal4 = tex2D(_Palette, float2(1,0));

			if (
				c.r >= _TexCol1.r - 0.001 && c.r <= _TexCol1.r + 0.001 &&
				c.g >= _TexCol1.g - 0.001 && c.g <= _TexCol1.g + 0.001 &&
				c.b >= _TexCol1.b - 0.001 && c.b <= _TexCol1.b + 0.001 &&
				c.a >= _TexCol1.a - 0.001 && c.a <= _TexCol1.a + 0.001
				)
			{
				return Pal1;
            }

			if (
				c.r >= _TexCol2.r - 0.001 && c.r <= _TexCol2.r + 0.001 &&
				c.g >= _TexCol2.g - 0.001 && c.g <= _TexCol2.g + 0.001 &&
				c.b >= _TexCol2.b - 0.001 && c.b <= _TexCol2.b + 0.001 &&
				c.a >= _TexCol2.a - 0.001 && c.a <= _TexCol2.a + 0.001
				)
			{
				return Pal2;
			}

			if (
				c.r >= _TexCol3.r - 0.001 && c.r <= _TexCol3.r + 0.001 &&
				c.g >= _TexCol3.g - 0.001 && c.g <= _TexCol3.g + 0.001 &&
				c.b >= _TexCol3.b - 0.001 && c.b <= _TexCol3.b + 0.001 &&
				c.a >= _TexCol3.a - 0.001 && c.a <= _TexCol3.a + 0.001
				)
			{
				return Pal3;
			}

			if (
				c.r >= _TexCol4.r - 0.001 && c.r <= _TexCol4.r + 0.001 &&
				c.g >= _TexCol4.g - 0.001 && c.g <= _TexCol4.g + 0.001 &&
				c.b >= _TexCol4.b - 0.001 && c.b <= _TexCol4.b + 0.001 &&
				c.a >= _TexCol4.a - 0.001 && c.a <= _TexCol4.a + 0.001
				)
			{
				return Pal4;
			}

            return c;
        }


            ENDCG
        }
    }
}