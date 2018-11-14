Shader "TestShader"
{
	Properties
	{
		_MainTex ("Diffuse (RGB) Spec(A)", 2D) = "white" {}
        _RimColor ("Rim Color", Color) = (0.26, 0.19, 0.16, 0.0)
        _RimPower ("Rim Power", Range(0.5, 0.8)) = 3.0
        _SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
        _Shininess ("Shininess", Range(0.01, 1)) = 0.078125
	}
	SubShader
	{
	Tags { "RenderType"="Transparent" "Queue" = "Transparent" } // Transparetn when  doing alpha  blending

			CGPROGRAM
            
			#pragma surface surf SimpleSpecular alpha
            
            float _Shininess; 
            
			//struct appdata
			//{
			//	float4 vertex : POSITION;
			//	float2 uv : TEXCOORD0;
			//};

			//struct v2f
			//{
			//	float2 uv : TEXCOORD0;
			//	UNITY_FOG_COORDS(1)
			//	float4 vertex : SV_POSITION;
			//};
            
            half4 LightingSimpleSpecular(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
                half3 h = normalize (lightDir + viewDir);
                half diff = max(0, dot(s.Normal, lightDir));
                float nh = max(0, dot(s.Normal, h));
                float spec = pow(nh, 48.0);
                half4 c;
                c.rbg = (s.Albedo * _LightColor0.rbg * diff + _LightColor0.rbg * spec * s.Alpha * _Shininess * _SpecColor) * (atten*2);
                c.a = s.Alpha;
                return c;
            }
            
            struct Input {
                float2 uv_MainTex;
                float3 viewDir;
            };

			sampler2D _MainTex;
			float3 _RimColor;
            float _RimPower;
			
            void surf(Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
                o.Albedo = c.rbg;
                half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
                o.Emission = _RimColor.rgb + pow(rim, _RimPower);
                o.Alpha = c.a + rim;
            }
			//v2f vert (appdata v)
			//{
			//	v2f o;
			//	o.vertex = UnityObjectToClipPos(v.vertex);
			//	//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
   //             o.uv = v.uv;
			//	UNITY_TRANSFER_FOG(o,o.vertex);
			//	return o;
			//}
			
			//fixed4 frag (v2f i) : SV_Target
			//{
            
   //         return float4(i.uv.x, i.uv.y, 1, 1);
			//	// sample the texture
			////fixed4 col = tex2D(_MainTex, i.uv);
			//	// apply fog
			////UNITY_APPLY_FOG(i.fogCoord, col);
			//	//return col; 
			//}
			ENDCG
		}
        Fallback "Diffuse"
}
