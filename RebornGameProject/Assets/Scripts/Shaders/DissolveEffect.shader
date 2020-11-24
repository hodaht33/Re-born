/// <summary>
/// 작성자 : 이성호
/// 기능 : 디졸브 효과
/// </summary>

Shader "Unlit/Dissolve"	// Unlit : 조명의 영향을 받지 않는 셰이더들을 의미
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}	// 2D 이미지
		_NoiseTex ("Texture", 2D) = "white" {}	// 2D 이미지
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0	// MaterialToggle(or Toggle) : bool값으로 설정하도록 해줌
		_EdgeColor1 ("EdgeColor1", Color) = (1.0, 1.0, 1.0, 1.0)	// 색상 값1
		_EdgeColor2 ("EdgeColor2", Color) = (1.0, 1.0, 1.0, 1.0)	// 색상 값2
		_Level ("DissolutionLevel", Range(0.0, 1.0)) = 0.1	// 보간되며 없어지는 정도(1에 가까울수록 보이지 않음)
		_Edges ("EdgeWidth", Range (0.0, 1.0)) = 0.1	// 색상 값에 영향을 받는 크기(1에 가까울수록 크게 영향을 미침)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		// Queue 태그 : 렌더링 순서 결정(렌더링 대기열 결정, 불투명 오브젝트를 그린 뒤 투명 오브젝트가 그려짐)
		// RenderType 태그 : 미리 정의된 몇 가지 그룹으로 분류 - 참고 : https://docs.unity3d.com/kr/530/Manual/SL-ShaderReplacement.html
        LOD 100

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha	// 알파값에 곱하여 영향을 미침(0이면 보이지 않는 것을 의미)
			/*	일반적인 블렌딩 유형들 - 참고 : https://docs.unity3d.com/kr/530/Manual/SL-Blend.html
				Blend SrcAlpha OneMinusSrcAlpha // Traditional transparency
				Blend One OneMinusSrcAlpha // Premultiplied transparency
				Blend One One // Additive
				Blend OneMinusDstColor One // Soft Additive
				Blend DstColor Zero // Multiplicative
				Blend DstColor SrcColor // 2x Multiplicative
			*/
			Cull Off	// 어느 면을 그리지 않을 지 결정 Culling : 그리지 않도록 함(Off : 지우는 면 없이 모든 면을 그림, Back : 후면을 그리지 않음, Front : 정면을 그리지 않음)
			Lighting Off	// 정점에 조명 값에 대한 영향을 끼칠 지 적용
			ZWrite Off	// 깊이 버퍼 설정 할 지에 대한 값
			Fog { Mode Off }	// 안개 관련 렌더링 비활성화?

            CGPROGRAM
            #pragma vertex vert	// vertex shader를 처리하는 함수는 vert함수라고 알림
            #pragma fragment frag	// fragment shader를 처리하는 함수는 frag함수라고 알림
            // make fog work
            //#pragma multi_compile_fog	// 안개 기능 활성화
			#pragma multi_compile DUMMY PIXELSNAP_ON	// 픽셀 스냅 기능 활성화

            #include "UnityCG.cginc"

			// appdata : Application to Vertex Shader Structure
			// 정점 데이터를 받을 구조체
            struct appdata	
            {
                float4 vertex : POSITION;	// 정점 위치 값 - POSITION : 정점 좌표 시맨틱
                float2 uv : TEXCOORD0;	// uv위치 값 - TEXCOORD0 : uv좌표 시맨틱(맨 끝은 숫자0임_주의)
            };

			// v2f : Vertex Shader to Fragment(Pixel) Shader Structure
			// frag데이터를 받을 구조체
            struct v2f	
            {
                float4 vertex : SV_POSITION;	// 정점 위치 값 - SV_POSITION : sv는 system value를 의미, 시스템적으로 추가 처리를 한 정점 좌표를 받는 시맨틱
                float2 uv : TEXCOORD0;	// uv위치 값 - 정점 데이터를 받는 uv와 동일한 시맨틱
                //UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;	// 샘플링 2D 이미지
			sampler2D _NoiseTex;	// 샘플링 2D 이미지
			float4 _EdgeColor1;	// 색상 값1
			float4 _EdgeColor2;	// 색상 값2
			float _Level;	// 레벨 값(보간하며 없어지도록 하기 위한 값)
			float _Edges;	// 정점 값(얼마나 없어지도록 할 지 정하기 위한 값)
			float4 _MainTex_ST;	// Unity의 Material에 대한 (Inspector창)텍스처의 "Tiling"과 "Offset" 값, 따라서 없으면 안되는 값
								// 그렇기 때문에 "o.uv = TRANSFORM_TEX(v.uv, _MainTex);" 코드를 쓰지 않고 
								// 그냥 "o.uv = v.uv"와 같이 대입하면 Unity 에디터에서 설정한 "Tiling"과 "Offset"값이 적용되지 않습니다.
								// 참고 : https://m.blog.naver.com/PostView.nhn?blogId=techshare&logNo=221328101250&categoryNo=1&proxyReferer=&proxyReferer=https:%2F%2Fwww.google.com%2F

            v2f vert (appdata v)	
            {
                v2f o;	
				o.vertex = UnityObjectToClipPos(v.vertex);	// 정점들의 정보를 가져옴
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);	// 메인 텍스처에서 uv좌표값을 얻어옴
                //UNITY_TRANSFER_FOG(o,o.vertex);

				#ifdef PIXELSNAP_ON
				o.vertex = UnityPixelSnap(o.vertex);
				#endif

                return o;
            }

            fixed4 frag (v2f i) : SV_Target	// SV_Target : SV_Target is the semantic used by DX10+ for fragment shader color output, COLOR is used by DX9 as the fragment output semantic
            {
                // // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
                // // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
				float cutout = tex2D(_NoiseTex, i.uv).r;	// 이미지 마스크에서 그리지 않을 uv위치의 red 색상값
				fixed4 col = tex2D(_MainTex, i.uv);	// 현재 메인 이미지의 uv좌표의 픽셀 색상값

				// cutout값이 _Level값보다 작은 uv위치의 픽셀은 그리지 않음
				if (cutout < _Level)
				{
					discard;
				}

				// 해당 uv위치의 알파 값이 마스크의 red색상 값과
				// _Level값과 _Edges값의 합보다 작으면 해당 uv위치의 색상 값을
				// EdgeColor1에서 EdgeColor2로 (cutout - _Level) / _Edges만큼 서서히 변화시킴
				if (cutout < col.a && cutout < _Level + _Edges)
				{
					col = lerp(_EdgeColor1, _EdgeColor2, (cutout - _Level) / _Edges);
				}

				// 최종 색상 값 반환
                return col;
            }
            ENDCG
        }
    }
}
