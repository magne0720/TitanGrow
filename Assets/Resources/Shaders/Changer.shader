Shader "Hidden/ShaderEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
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

		uniform float u_timer;


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
	//バーテクスシェーダ
	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	sampler2D _MainTex;

	//フラグメントシェーダ
	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col = tex2D(_MainTex, i.uv);
	// just invert the colors

	//画面真ん中からにするためのoffset
	//i.uv -= float2(u_offset_x + 0.5, u_offset_y + 0.5);
	i.uv -= float2(0.5, 0.5);
	//正円にするためのアス比の設定
	i.uv.x *= 16.0 / 9.0;

	//半径外を黒で塗る
	//if (distance(i.uv, float2(u_offsetXY.x, u_offsetXY.y)) > u_timer)
	if (distance(i.uv, float2(0,0)) > u_timer)
	{
		col.rgb = float3(0.0, 0.0, 0.0);
	}
	return col;
	}
		ENDCG
	}
	}
}
