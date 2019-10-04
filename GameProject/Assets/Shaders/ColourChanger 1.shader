﻿Shader "Custom/ColourChanger"
{
    Properties
    {
        _MainTex("Sprite", 2D) = "white" {}
        _Palette("Palette", 2D) = "white" {}


        _TexCol1("Colour 1", Color) = (0,0,0,1)
        _TexCol2("Colour 2", Color) = (0,0,0,1)
        _TexCol3("Colour 3", Color) = (0,0,0,1)

		[MaterialToggle] _UseTrans("4th Colour is Transparent?", float) = 0

		_TexCol4("Colour 4", Color) = (0,0,0,1)
    }
    SubShader
    {
        Pass
        {

			CGPROGRAM

			// Defining Functions
			#pragma vertex vertexFunction
			#pragma fragment fragmentFunction
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"



        sampler2D _MainTex;
        sampler2D _Palette;



		UNITY_INSTANCING_BUFFER_START(Props)
			//UNITY_DEFINE_INSTANCED_PROP(sampler2D, _Palette)
			UNITY_DEFINE_INSTANCED_PROP(float4, _TexCol1)
			UNITY_DEFINE_INSTANCED_PROP(float4, _TexCol2)
			UNITY_DEFINE_INSTANCED_PROP(float4, _TexCol3)
			UNITY_DEFINE_INSTANCED_PROP(float4, _TexCol4)
			UNITY_DEFINE_INSTANCED_PROP(float, _UseTrans)
		UNITY_INSTANCING_BUFFER_END(Props)

		// Vertex
        struct Appdata
        {
            float4 vertex : POSITION;		// Position x,y,z,w
            float2 uv_MainTex : TEXCOORD0;	// x,y
			UNITY_VERTEX_INPUT_INSTANCE_ID
        };


		// Vertex to Fragment Values
        struct V2F
        {
            float4 vertex : SV_POSITION;	// Position x,y,z,w (SV_ dx(9,10,11) platforms thingy)
            float2 uv_MainTex : TEXCOORD0;	// x,y
			UNITY_VERTEX_INPUT_INSTANCE_ID
        };


		// Build the object (fragment)
        V2F vertexFunction(Appdata v)
        {
            V2F o;

			UNITY_SETUP_INSTANCE_ID(v);
			UNITY_TRANSFER_INSTANCE_ID(v, o);

            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv_MainTex = v.uv_MainTex;

            return o;
        }


		// Actually does the colour changing stuff
		// Goes through all the pixels one by one and coluring it all in
        float4 fragmentFunction(V2F IN) : SV_TARGET
        {
			UNITY_SETUP_INSTANCE_ID(IN);

            float4 c = tex2D(_MainTex, IN.uv_MainTex);


			//half4 Pal1 = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props, _Palette), float2(0, 1));
			//half4 Pal2 = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props, _Palette), float2(1, 1));
			//half4 Pal3 = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props, _Palette), float2(0, 0));
			//half4 Pal4 = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props, _Palette), float2(1, 0));

			half4 Pal1 = tex2D(_Palette, float2(0, 1));
			half4 Pal2 = tex2D(_Palette, float2(1, 1));
			half4 Pal3 = tex2D(_Palette, float2(0, 0));
			half4 Pal4 = tex2D(_Palette, float2(1, 0));

			float4 _TestCol1 = UNITY_ACCESS_INSTANCED_PROP(Props, _TexCol1);
			float4 _TestCol2 = UNITY_ACCESS_INSTANCED_PROP(Props, _TexCol2);
			float4 _TestCol3 = UNITY_ACCESS_INSTANCED_PROP(Props, _TexCol3);
			float4 _TestCol4 = UNITY_ACCESS_INSTANCED_PROP(Props, _TexCol4);

			if 
			(
				c.r >= _TestCol1.r - 0.001 && c.r <= _TestCol1.r + 0.001 &&
				c.g >= _TestCol1.g - 0.001 && c.g <= _TestCol1.g + 0.001 &&
				c.b >= _TestCol1.b - 0.001 && c.b <= _TestCol1.b + 0.001 &&
				c.a >= _TestCol1.a - 0.001 && c.a <= _TestCol1.a + 0.001
			)
			{
				return Pal1;
            }

			if 
			(
				c.r >= _TestCol2.r - 0.001 && c.r <= _TestCol2.r + 0.001 &&
				c.g >= _TestCol2.g - 0.001 && c.g <= _TestCol2.g + 0.001 &&
				c.b >= _TestCol2.b - 0.001 && c.b <= _TestCol2.b + 0.001 &&
				c.a >= _TestCol2.a - 0.001 && c.a <= _TestCol2.a + 0.001
			)
			{
				return Pal2;
			}

			if 
			(
				c.r >= _TestCol3.r - 0.001 && c.r <= _TestCol3.r + 0.001 &&
				c.g >= _TestCol3.g - 0.001 && c.g <= _TestCol3.g + 0.001 &&
				c.b >= _TestCol3.b - 0.001 && c.b <= _TestCol3.b + 0.001 &&
				c.a >= _TestCol3.a - 0.001 && c.a <= _TestCol3.a + 0.001
			)
			{
				return Pal3;
			}

			if 
			(
				c.r >= _TestCol4.r - 0.001 && c.r <= _TestCol4.r + 0.001 &&
				c.g >= _TestCol4.g - 0.001 && c.g <= _TestCol4.g + 0.001 &&
				c.b >= _TestCol4.b - 0.001 && c.b <= _TestCol4.b + 0.001 &&
				c.a >= _TestCol4.a - 0.001 && c.a <= _TestCol4.a + 0.001
			)
			{
				if (UNITY_ACCESS_INSTANCED_PROP(Props, _UseTrans) > 0)
				{
					// essentially gets rid of the pixels entirely if they are meant to be transparent
					discard;
				}
				else
				{
					// If there is no transparency on this sprite then the 4th colour will be used instead
					return Pal4;
				}
			}

            return c;
        }


            ENDCG
        }
    }
}