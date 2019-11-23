/*
	You shouldn't be here.....
	If something throws an error that stops you working then let me know...


	Colour Changer Shader
	-=-=-=-=-=-=-=-=-=-=-=-

	Made by: Jonathan Carter
	Last Edited By: Jonathan Carter
	Date Edited Last: 11/11/19 - Coded to work with UI...

	Edit History:
	- 03/11/19 - Added useage of Perm Palette instead of the stores colours in the shader
	- 03/11/19 - Fixed problem where palette's revert when you select the object in the inspector
	- 27/10/19 - Added fields to store colour options
	- 12/10/19 - Added option for transparency on all selected colours, not jsut the 4th colour.
								 Also removed some old commented code that isn't going to be used anymore.
	- 6/10/19 - To add this comment bit in (nothing else was changed)

	This script makes the colour changing happen, note that there is no intellisense on this script which makes mistakes easy

	DO NOT touch this at all, any minor typo's will break the shader!!!!

*/

Shader "Custom/ColourChanger"
{
	Properties
	{
		[HideInInspector] _MainTex("Sprite", 2D) = "white" {}

		[HideInInspector]_TexCol1("Colour 1", Color) = (185, 185, 185, 1)
		[HideInInspector]_TexCol2("Colour 2", Color) = (111, 111, 111, 1)
		[HideInInspector]_TexCol3("Colour 3", Color) = (55, 55, 55, 1)
		[HideInInspector]_TexCol4("Colour 4", Color) = (0,0,0,1)

		[HideInInspector]_PalCol1("Palette 1", Color) = (0,0,0,1)
		[HideInInspector]_PalCol2("Palette 2", Color) = (0,0,0,1)
		[HideInInspector]_PalCol3("Palette 3", Color) = (0,0,0,1)
		[HideInInspector]_PalCol4("Palette 4", Color) = (0,0,0,1)

		[HideInInspector]_StoreTrans1("StoreTrans1", Color) = (0,0,0,1)
		[HideInInspector]_StoreTrans2("StoreTrans2", Color) = (0,0,0,1)
		[HideInInspector]_StoreTrans3("StoreTrans3", Color) = (0,0,0,1)
		[HideInInspector]_StoreTrans4("StoreTrans4", Color) = (0,0,0,1)
		[HideInInspector][MaterialToggle]_IsInstance("IsInstance", Float) = 0
		[HideInInspector][MaterialToggle]_UseTrans("UseTrans", Float) = 1
		[HideInInspector]_PaletteSelected("PaletteSelected", Float) = 1
	}
		SubShader
		{
			Cull Off

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

		// Defines the GPU instanced variables - still not sure this does anythig helpful in a 2D game but I've kept it in for now
		UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(float4, _TexCol1)
			UNITY_DEFINE_INSTANCED_PROP(float4, _TexCol2)
			UNITY_DEFINE_INSTANCED_PROP(float4, _TexCol3)
			UNITY_DEFINE_INSTANCED_PROP(float4, _TexCol4)
			UNITY_DEFINE_INSTANCED_PROP(float4, _PalCol1)
			UNITY_DEFINE_INSTANCED_PROP(float4, _PalCol2)
			UNITY_DEFINE_INSTANCED_PROP(float4, _PalCol3)
			UNITY_DEFINE_INSTANCED_PROP(float4, _PalCol4)
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

			float4 _TestCol1 = UNITY_ACCESS_INSTANCED_PROP(Props, _TexCol1);
			float4 _TestCol2 = UNITY_ACCESS_INSTANCED_PROP(Props, _TexCol2);
			float4 _TestCol3 = UNITY_ACCESS_INSTANCED_PROP(Props, _TexCol3);
			float4 _TestCol4 = UNITY_ACCESS_INSTANCED_PROP(Props, _TexCol4);

			float4 Pal1 = UNITY_ACCESS_INSTANCED_PROP(Props, _PalCol1);
			float4 Pal2 = UNITY_ACCESS_INSTANCED_PROP(Props, _PalCol2);
			float4 Pal3 = UNITY_ACCESS_INSTANCED_PROP(Props, _PalCol3);
			float4 Pal4 = UNITY_ACCESS_INSTANCED_PROP(Props, _PalCol4);

			if
			(
				c.r >= _TestCol1.r - 0.001 && c.r <= _TestCol1.r + 0.001 &&
				c.g >= _TestCol1.g - 0.001 && c.g <= _TestCol1.g + 0.001 &&
				c.b >= _TestCol1.b - 0.001 && c.b <= _TestCol1.b + 0.001 &&
				c.a >= _TestCol1.a - 0.001 && c.a <= _TestCol1.a + 0.001
			)
			{
				if (UNITY_ACCESS_INSTANCED_PROP(Props, _PalCol1.a) == 0)
				{
					// essentially gets rid of the pixels entirely if they are meant to be transparent
					discard;
				}
				else
				{
					// If there is no transparency on this sprite then the 4th colour will be used instead
					return Pal1;
				}
			}

			if
			(
				c.r >= _TestCol2.r - 0.001 && c.r <= _TestCol2.r + 0.001 &&
				c.g >= _TestCol2.g - 0.001 && c.g <= _TestCol2.g + 0.001 &&
				c.b >= _TestCol2.b - 0.001 && c.b <= _TestCol2.b + 0.001 &&
				c.a >= _TestCol2.a - 0.001 && c.a <= _TestCol2.a + 0.001
			)
			{
				if (UNITY_ACCESS_INSTANCED_PROP(Props, _PalCol2.a) == 0)
				{
					// essentially gets rid of the pixels entirely if they are meant to be transparent
					discard;
				}
				else
				{
					// If there is no transparency on this sprite then the 4th colour will be used instead
					return Pal2;
				}
			}

			if
			(
				c.r >= _TestCol3.r - 0.001 && c.r <= _TestCol3.r + 0.001 &&
				c.g >= _TestCol3.g - 0.001 && c.g <= _TestCol3.g + 0.001 &&
				c.b >= _TestCol3.b - 0.001 && c.b <= _TestCol3.b + 0.001 &&
				c.a >= _TestCol3.a - 0.001 && c.a <= _TestCol3.a + 0.001
			)
			{
				if (UNITY_ACCESS_INSTANCED_PROP(Props, _PalCol3.a) == 0)
				{
					// essentially gets rid of the pixels entirely if they are meant to be transparent
					discard;
				}
				else
				{
					// If there is no transparency on this sprite then the 4th colour will be used instead
					return Pal3;
				}
			}

			if
			(
				c.r >= _TestCol4.r - 0.001 && c.r <= _TestCol4.r + 0.001 &&
				c.g >= _TestCol4.g - 0.001 && c.g <= _TestCol4.g + 0.001 &&
				c.b >= _TestCol4.b - 0.001 && c.b <= _TestCol4.b + 0.001 &&
				c.a >= _TestCol4.a - 0.001 && c.a <= _TestCol4.a + 0.001
			)
			{
				if (UNITY_ACCESS_INSTANCED_PROP(Props, _PalCol4.a) == 0)
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

			CustomEditor "ShaderEditorGUI"
}