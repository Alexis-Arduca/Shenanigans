Shader "Custom/2DSoftShadowGaussian5x5"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _ShadowColor ("Shadow Color", Color) = (0,0,0,0.5)
        _ShadowOffset ("Shadow Offset", Vector) = (0.05, -0.05, 0, 0)
        _BlurAmount ("Blur Amount", Range(0, 10)) = 2.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        // Shadow Pass: Apply a 5x5 Gaussian blur to the sprite texture for the shadow.
        Pass
        {
            Name "Shadow"
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vertShadow
            #pragma fragment fragShadow
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ShadowColor;
            float4 _ShadowOffset;
            float _BlurAmount;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;
            };

            // Vertex shader: Offset the sprite vertices for the shadow.
            v2f vertShadow (appdata v)
            {
                v2f o;
                float4 pos = UnityObjectToClipPos(v.vertex);
                pos.xy += _ShadowOffset.xy * pos.w;
                o.vertex = pos;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            // Fragment shader: 5x5 Gaussian blur.
            fixed4 fragShadow (v2f i) : SV_Target
            {
                // Base UV offset scale (tweak if needed depending on your texture's resolution)
                float2 uvOffset = _BlurAmount * 0.001;
                fixed4 col = fixed4(0,0,0,0);

                // 5x5 kernel with weights approximating a Gaussian distribution:
                // Kernel weights (rows): 
                //   Row -2: [1, 4, 6, 4, 1]
                //   Row -1: [4,16,24,16,4]
                //   Row  0: [6,24,36,24,6]
                //   Row  1: [4,16,24,16,4]
                //   Row  2: [1, 4, 6, 4, 1]
                // Total weight = 256.
                col += tex2D(_MainTex, i.uv + uvOffset * float2(-2, -2)) * (1.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2(-1, -2)) * (4.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 0, -2)) * (6.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 1, -2)) * (4.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 2, -2)) * (1.0/256.0);

                col += tex2D(_MainTex, i.uv + uvOffset * float2(-2, -1)) * (4.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2(-1, -1)) * (16.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 0, -1)) * (24.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 1, -1)) * (16.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 2, -1)) * (4.0/256.0);

                col += tex2D(_MainTex, i.uv + uvOffset * float2(-2,  0)) * (6.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2(-1,  0)) * (24.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 0,  0)) * (36.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 1,  0)) * (24.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 2,  0)) * (6.0/256.0);

                col += tex2D(_MainTex, i.uv + uvOffset * float2(-2,  1)) * (4.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2(-1,  1)) * (16.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 0,  1)) * (24.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 1,  1)) * (16.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 2,  1)) * (4.0/256.0);

                col += tex2D(_MainTex, i.uv + uvOffset * float2(-2,  2)) * (1.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2(-1,  2)) * (4.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 0,  2)) * (6.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 1,  2)) * (4.0/256.0);
                col += tex2D(_MainTex, i.uv + uvOffset * float2( 2,  2)) * (1.0/256.0);

                // Apply the shadow tint and opacity.
                return col * _ShadowColor;
            }
            ENDCG
        }

        // Sprite Pass: Render the sprite normally on top of the shadow.
        Pass
        {
            Name "Sprite"
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vertSprite
            #pragma fragment fragSprite
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;
            };

            v2f vertSprite (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 fragSprite (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
    FallBack "Sprites/Default"
}
