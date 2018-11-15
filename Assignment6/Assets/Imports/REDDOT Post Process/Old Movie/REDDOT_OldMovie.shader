// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


    Shader "Hidden/REDDOT oldMovie 1" {
    Properties {
        _MainTex ("Input", RECT) = "white" {}
    }
        SubShader {
            Pass {
                ZTest Always Cull Off ZWrite Off
                Fog { Mode off }

        CGPROGRAM
       
        #pragma vertex vert
        #pragma fragment frag
        #pragma fragmentoption ARB_precision_hint_fastest
        #pragma multi_compile MODE1 MODE2 MODE3
        
#if defined (MODE1)
#define BAW
#define GRAIN
#endif

#if defined (MODE2)
#define BAW
#define VL
#define GRAIN
#endif        

#if defined (MODE3)
#define BAW
#define VL
#define HL
#define GRAIN
#endif       
                
        #pragma target 3.0
     
        #include "UnityCG.cginc"
     
        uniform sampler2D _MainTex;
        uniform sampler2D _NoiseTex;
        uniform float4 _MainTex_TexelSize;

half2 uv;
fixed rand_1;
fixed rand_2;
fixed rand_3;
fixed rand_4;
fixed rand_5;
fixed rand_6;
fixed rand_7;
fixed rand_8;
fixed rand_9;
float _timeScale;
float _grainIntensity;
fixed4 _colorTint;
fixed _bawIntensity;
float _filmDirtSize;

float rand(half2 co){
    return frac(sin(dot(co.xy ,half2(12.9898,78.233))) * 43758.5453);
}

float rand(float c){
	return rand(half2(c,1.0));
}


float randomVLine(float seed)
{
	float b = 0.01 * rand_1;
	float a = rand(seed+1.0);
	float c = rand_4 - 0.5;
	float mu = rand_7;
	
	float l = 1.0;
	
	if ( mu > 0.2)
		l = pow(  abs(a * uv.x + b * uv.y + c ), 1.0/8.0 );
	else
		l = 2.0 - pow( abs(a * uv.x + b * uv.y + c), 1.0/8.0 );				
	
	return lerp(0.5, 1.0, l);
}

float randomHLine(float seed)
{
	float x = rand_1;
	float y = rand_3;
	float s = 0.01 * rand(seed+3.0) * _filmDirtSize;
	
	half2 p = half2(x,y) - uv;
	p.x *= _ScreenParams.x/_ScreenParams.y;
	float a = atan2(p.y,p.x);
	float v = 1.0;
	float ss = s*s * (sin(6.2831*a*x)*0.1 + 1.0);
	
	if ( dot(p,p) < ss ) v = 0.2;
	else
		v = pow(dot(p,p) - ss, 1.0/16.0);
	
	return lerp(0.3 + 0.2 * (1.0 - (s / 0.02)), 1.0, v);
}

    
struct v2f_img2 {
	float4 pos : SV_POSITION;
	half2 uv : TEXCOORD0;
	half2 uv2 : TEXCOORD1;
	half2 uv2flip : TEXCOORD2;
	
};     
     
v2f_img2 vert( appdata_img v )
{
	v2f_img2 o;
	o.pos = UnityObjectToClipPos (v.vertex);
	o.uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
	#if SHADER_API_D3D9
	if (_MainTex_TexelSize.y < 0)
		o.uv.y = 1-o.uv.y;
	#endif		
	return o;
}
   
half4 frag (v2f_img2 i) : COLOR {
		uv = i.uv;
		
		float t = float(int(_Time.y * 15 * _timeScale));
		
		rand_1 = rand(t);
		rand_2 = rand(t+23.0);
		rand_3 = rand(t+1.0);
		rand_4 = rand(t+2.0);
		rand_5 = rand(t+0.5);
		rand_6 = rand(t+8.0);
		rand_7 = rand(t+3.0);		
		rand_8 = rand(t+7.0);
		rand_9 = rand(t+18.0);
		
		half2 suv = uv + 0.002 * half2( rand_1, rand_2);
		
		fixed3 image = tex2D( _MainTex, half2(suv.x, suv.y) ).xyz;
		
		#ifdef BAW
			float luma = dot( fixed3(0.2126, 0.7152, 0.0722), image );
			fixed3 oldImage = lerp(image,luma * fixed3(0.7, 0.7, 0.7),_bawIntensity);
		#else
			fixed3 oldImage = image;
		#endif
		
		float vI = 16.0 * (uv.x * (1.0-uv.x) * uv.y * (1.0-uv.y));
		vI *= lerp( 0.7, 1.0, rand_5);
		
		vI += 1.0 + 0.4 * rand_6;
		
		vI *= pow(16.0 * uv.x * (1.0-uv.x) * uv.y * (1.0-uv.y), 0.4);
		
		#ifdef VL
		int l = int(8.0 * rand(t+7.0));
		
		if ( 0 < l ) vI *= randomVLine( t+6.0+17.* 0.0);
		if ( 1 < l ) vI *= randomVLine( t+6.0+17.* 1.0);
		if ( 2 < l ) vI *= randomVLine( t+6.0+17.* 2.0);		
		if ( 3 < l ) vI *= randomVLine( t+6.0+17.* 3.0);
		if ( 4 < l ) vI *= randomVLine( t+6.0+17.* 4.0);
		//if ( 5 < l ) vI *= randomVLine( t+6.0+17.* 5.0);
		//if ( 6 < l ) vI *= randomVLine( t+6.0+17.* 6.0);
		//if ( 7 < l ) vI *= randomVLine( t+6.0+17.* 7.0);
		
		#endif
		
		#ifdef HL
		int s = int( max(8.0 * rand(t+18.0) -2.0, 0.0 ));

		if ( 0 < s ) vI *= randomHLine( t+6.0+19.* 0.0);
		if ( 1 < s ) vI *= randomHLine( t+6.0+19.* 1.0);
		if ( 2 < s ) vI *= randomHLine( t+6.0+19.* 2.0);
		if ( 3 < s ) vI *= randomHLine( t+6.0+19.* 3.0);
		if ( 4 < s ) vI *= randomHLine( t+6.0+19.* 4.0);
		if ( 5 < s ) vI *= randomHLine( t+6.0+19.* 5.0);
		
		#endif
	
		fixed4 color;
        color.xyz = oldImage * vI * _colorTint;
		
		#ifdef GRAIN
        color.xyz *= lerp(fixed3(1.0,1.0,1.0),(1.0+(rand(uv+t*.01)-.2)*.15),_grainIntensity);		
        #endif	
        
        return color;	

}   
   
   ENDCG
   }
       
            
  }
}