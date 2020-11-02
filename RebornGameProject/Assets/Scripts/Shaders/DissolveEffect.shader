Shader "Unlit/Dissolve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_EdgeColor1 ("EdgeColor1", Color) = (1.0, 1.0, 1.0, 1.0)
		_EdgeColor2 ("EdgeColor2", Color) = (1.0, 1.0, 1.0, 1.0)
		_Level ("DissolutionLevel", Range(0.0, 1.0)) = 0.1
		_Edges ("EdgeWidth", Range (0.0, 1.0)) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog
			#pragma multi_compile DUMMY PIXELSNAP_ON

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
			sampler2D _NoiseTex;
			float4 _EdgeColor1;
			float4 _EdgeColor2;
			float _Level;
			float _Edges;
			float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);

				#ifdef PIXELSNAP_ON
				o.vertex = UnityPixelSnap(o.vertex);
				#endif

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
                // // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
				float cutout = tex2D(_NoiseTex, i.uv).r;
				fixed4 col = tex2D(_MainTex, i.uv);

				if (cutout < _Level)
				{
					discard;
				}

				if (cutout < col.a && cutout < _Level + _Edges)
				{
					col = lerp(_EdgeColor1, _EdgeColor2, (cutout - _Level) / _Edges);
				}

                return col;
            }
            ENDCG
        }
    }
}
