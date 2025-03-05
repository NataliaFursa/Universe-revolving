Shader "Unlit/AddBlendColorTwoSides"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
        _AddBlend ("Additive - Alpha Blend", Range(0,1)) = 1
        _Power ("Power", Range(0,15)) = 1
         [Toggle(PARTICLES)] _Particles ("Use Particle texcoord01.z for Power, w for addblend, ", float) = 0

         	[Space(10)]
		[Header(Hardware settings)]
		[Enum(UnityEngine.Rendering.CullMode)] _CullMode ("Cull faces", Float) = 2
		
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        LOD 100
        zwrite off
        blend One OneMinusSrcAlpha
	Cull [_CullMode]
		
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           #pragma shader_feature PARTICLES 

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                #ifdef PARTICLES
                float4 uv : TEXCOORD0;
                #else
                float2 uv : TEXCOORD0;
                #endif
                fixed4 color : COLOR;
            };

            struct v2f
            {
                #ifdef PARTICLES
                float3 uv : TEXCOORD0;
                #else
                float2 uv : TEXCOORD0;
                #endif
                fixed4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed _AddBlend;
            fixed _Power;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
                #ifdef PARTICLES              
                _Power *= v.uv.z;
                o.uv.z = v.uv.w;
                #endif
                o.color = v.color * _Color * _Power;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {                
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                col.rgb *= col.a;
                #ifdef PARTICLES
                _AddBlend *= i.uv.z;               
                #endif
                col.a *= _AddBlend;       
                return col;
            }
            ENDCG
        }
    }
}
