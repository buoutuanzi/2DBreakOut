Shader "Unlit/BlockPicShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AreaWidth("图片显示区域占ViewPort的大小，从左往右", float) = 0.7
        _AreaHeight ("图片显示区域占ViewPort的大小，从上往下", float) = 0.5
        _OffsetX("X轴偏移的ViewPort坐标", float) = 0.3
        _OffsetY("Y轴偏移的ViewPort坐标", float) = 0.3
        _CrackTex("Crack_Texture", 2D) = "white" {}
        _CrackScale("CrackScale", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv_crack : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                float2 uv_crack : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CrackTex;
            float4 _CrackTex_ST;
            fixed _AreaWidth;
            fixed _AreaHeight;
            fixed _OffsetX;
            fixed _OffsetY;
            fixed _CrackScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.uv_crack = v.uv_crack;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                half2 uv;
                half2 pos = i.screenPos.xy / i.screenPos.w;
                // 原点向x轴正方向偏移，uv要反向偏移
                uv.x = pos.x / _AreaWidth - _OffsetX;
                uv.y = (pos.y - (1 - _AreaHeight)) / _AreaHeight - _OffsetY;
                // sample the texture
                fixed4 col = tex2D(_MainTex, uv);
                fixed crack_col = tex2D(_CrackTex, i.uv_crack).r;
                crack_col = lerp(1, crack_col, _CrackScale);
                return col * crack_col;
            }
            ENDCG
        }
    }
}
