Shader "Hidden/shader"
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
			sampler2D _CameraDepthTexture;

			uniform float u_max;
			uniform float u_min;

			float _Size;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);


			float depth = Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);



			if (depth >0.9) {


				// ブラー
				col = float4(0.0, 0.0, 0.0, 1.0);
				// ぼかし量
				float2 pixelSize = float2(1.0 / 256.0, 1.0 / 256.0);
				col += tex2D(_MainTex, i.uv + pixelSize * float2(-3, 0))*0.053;
				col += tex2D(_MainTex, i.uv + pixelSize * float2(-2, 0))*0.123;
				col += tex2D(_MainTex, i.uv + pixelSize * float2(-1, 0))*0.203;
				col += tex2D(_MainTex, i.uv)*0.240;
				col += tex2D(_MainTex, i.uv + pixelSize * float2(1, 0))*0.203;
				col += tex2D(_MainTex, i.uv + pixelSize * float2(2, 0))*0.123;
				col += tex2D(_MainTex, i.uv + pixelSize * float2(3, 0))*0.053;

			}
				return col;
			}
			ENDCG
		}
	}
}
