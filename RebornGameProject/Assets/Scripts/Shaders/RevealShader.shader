// 참고 : https://www.youtube.com/watch?v=b4utgRuIekk

// 보기만 했던 것 : https://answers.unity.com/questions/381183/two-textures-on-one-surface.html

Shader "Custom/HiddenTexture"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0

		_LightDirection("Light Direction", Vector) = (0,0,1,0)
		_LightPosition("Light Position", Vector) = (0,0,0,0)
		_LightAngle("Light Angle", Range(0, 180)) = 45
		_LightRange("Light Range", Float) = 30
		_LightEnabled("Light Enabled", Float) = 0
		_StrengthScaler("Strength", Float) = 15
	}
		SubShader
		{
			//RenderType Opaque에서 변경, Queue는 추가
			Tags { "RenderType" = "Transparent" "Queue" = "Geometry"}
			LOD 200

		CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			// alpha추가
			#pragma surface surf Standard fullforwardshadows alpha:fade

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;

			struct Input
			{
				float2 uv_MainTex;
				float3 worldPos;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;
			float4 _LightPosition;
			float4 _LightDirection;
			float _LightAngle;
			float _LightRange;
			float _LightEnabled;
			float _StrengthScaler;

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				if ( _LightEnabled == 1 )
				{
					float3 direction = normalize(_LightPosition - IN.worldPos);
					// 거리에 따라 보이는 정도 조절
					float distance = clamp(1 - (abs(IN.worldPos - _LightPosition) / _LightRange), 0, 1);
					float scale = dot(direction, _LightDirection);	// 내적을 통해 드러날 크기
					float strength = scale - cos(_LightAngle * (3.14 / 360.0));	// 알파 값을 조절하기 위한 것(반지름)
					strength = min(max(strength * _StrengthScaler, 0), 1);	// 0 ~ 1 사이로 조절

					// Albedo comes from a texture tinted by color
					fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
					o.Albedo = c.rgb;	// 색깔
					o.Emission = c.rgb * c.a * strength;	// 강도?
					// Metallic and smoothness come from slider variables
					o.Metallic = _Metallic;
					o.Smoothness = _Glossiness;
					o.Alpha = min(strength * c.a, distance * c.a);	// 원래의 알파값과 strength를 곱
				}
				else
				{
					//fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
					//o.Albedo = c.rgb;
					o.Alpha = 0;
				}
			}
			ENDCG
		}
			FallBack "Diffuse"
}
