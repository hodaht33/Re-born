Shader "Custom/hgfedsh"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _CubeMap("CubeMap", cube) = "" {}
        _BumpMap("BumpMap", 2D) = "bump"
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf Lambert 

            sampler2D _MainTex;
            sampler2D _BumpMap;
            samplerCUBE _CubeMap;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_BumpMap;
                float3 worldRefl;
                INTERNAL_DATA
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                //o.Albedo = c.rgb;            //! 반사에는 물리적으로 그림자 영향받지 않으므로 Albedo 주석
                o.Alpha = c.a;

                float3 fNormal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
                o.Normal = fNormal;

                float4 fCube = texCUBE(_CubeMap, WorldReflectionVector(IN, o.Normal));
                o.Emission = fCube.rgb;
            }

            ENDCG
        }
            FallBack "Diffuse"
}