Shader "Sonke/AlphaCtrl" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB) Transparency (A)", 2D) = "" {}
}
SubShader {
  Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
  LOD 200

CGPROGRAM
#pragma surface surf Lambert alpha:fade

sampler2D _MainTex;
fixed4 _Color;

struct Input {
  float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
  fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
  o.Albedo = c.rgb;
  o.Alpha = c.a;
}
ENDCG
}
}
//SubShader { 
// Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
// Pass {
//  Name "FORWARD"
//  Tags { "LIGHTMODE"="ForwardBase" "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
//  Blend SrcAlpha OneMinusSrcAlpha
//Program "vp" {
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying lowp vec3 xlv_TEXCOORD2;
//varying lowp vec3 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_Scale;
//uniform highp vec4 unity_SHC;
//uniform highp vec4 unity_SHBr;
//uniform highp vec4 unity_SHBg;
//uniform highp vec4 unity_SHBb;
//uniform highp vec4 unity_SHAr;
//uniform highp vec4 unity_SHAg;
//uniform highp vec4 unity_SHAb;
//
//uniform highp mat4 _Object2World;
//uniform highp vec4 _MainTex_ST;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec3 _glesNormal;
//attribute vec4 _glesVertex;
//void main ()
//{
//  highp vec3 shlight;
//  lowp vec3 tmpvar_1;
//  lowp vec3 tmpvar_2;
//  mat3 tmpvar_3;
//  tmpvar_3[0] = _Object2World[0].xyz;
//  tmpvar_3[1] = _Object2World[1].xyz;
//  tmpvar_3[2] = _Object2World[2].xyz;
//  highp vec3 tmpvar_4;
//  tmpvar_4 = (tmpvar_3 * (normalize (_glesNormal) * unity_Scale.w));
//  tmpvar_1 = tmpvar_4;
//  highp vec4 tmpvar_5;
//  tmpvar_5.w = 1.0;
//  tmpvar_5.xyz = tmpvar_4;
//  mediump vec3 tmpvar_6;
//  mediump vec4 normal;
//  normal = tmpvar_5;
//  mediump vec3 x3;
//  highp float vC;
//  mediump vec3 x2;
//  mediump vec3 x1;
//  highp float tmpvar_7;
//  tmpvar_7 = dot (unity_SHAr, normal);
//  x1.x = tmpvar_7;
//  highp float tmpvar_8;
//  tmpvar_8 = dot (unity_SHAg, normal);
//  x1.y = tmpvar_8;
//  highp float tmpvar_9;
//  tmpvar_9 = dot (unity_SHAb, normal);
//  x1.z = tmpvar_9;
//  mediump vec4 tmpvar_10;
//  tmpvar_10 = (normal.xyzz * normal.yzzx);
//  highp float tmpvar_11;
//  tmpvar_11 = dot (unity_SHBr, tmpvar_10);
//  x2.x = tmpvar_11;
//  highp float tmpvar_12;
//  tmpvar_12 = dot (unity_SHBg, tmpvar_10);
//  x2.y = tmpvar_12;
//  highp float tmpvar_13;
//  tmpvar_13 = dot (unity_SHBb, tmpvar_10);
//  x2.z = tmpvar_13;
//  mediump float tmpvar_14;
//  tmpvar_14 = ((normal.x * normal.x) - (normal.y * normal.y));
//  vC = tmpvar_14;
//  highp vec3 tmpvar_15;
//  tmpvar_15 = (unity_SHC.xyz * vC);
//  x3 = tmpvar_15;
//  tmpvar_6 = ((x1 + x2) + x3);
//  shlight = tmpvar_6;
//  tmpvar_2 = shlight;
//  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = tmpvar_1;
//  xlv_TEXCOORD2 = tmpvar_2;
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying lowp vec3 xlv_TEXCOORD2;
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  mediump vec4 c_i0_i1;
//  c_i0_i1.xyz = tmpvar_1;
//  c_i0_i1.w = tmpvar_2;
//  c = c_i0_i1;
//  c.xyz = (c.xyz + (tmpvar_1 * xlv_TEXCOORD2));
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying highp vec2 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_LightmapST;
//
//uniform highp vec4 _MainTex_ST;
//attribute vec4 _glesMultiTexCoord1;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec4 _glesVertex;
//void main ()
//{
//  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying highp vec2 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D unity_Lightmap;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  c = vec4(0.0, 0.0, 0.0, 0.0);
//  c.xyz = (tmpvar_1 * (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD1).xyz));
//  c.w = tmpvar_2;
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying highp vec4 xlv_TEXCOORD3;
//varying lowp vec3 xlv_TEXCOORD2;
//varying lowp vec3 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_Scale;
//uniform highp vec4 unity_SHC;
//uniform highp vec4 unity_SHBr;
//uniform highp vec4 unity_SHBg;
//uniform highp vec4 unity_SHBb;
//uniform highp vec4 unity_SHAr;
//uniform highp vec4 unity_SHAg;
//uniform highp vec4 unity_SHAb;
//
//uniform highp vec4 _ProjectionParams;
//uniform highp mat4 _Object2World;
//uniform highp vec4 _MainTex_ST;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec3 _glesNormal;
//attribute vec4 _glesVertex;
//void main ()
//{
//  highp vec3 shlight;
//  lowp vec3 tmpvar_1;
//  lowp vec3 tmpvar_2;
//  highp vec4 tmpvar_3;
//  tmpvar_3 = (gl_ModelViewProjectionMatrix * _glesVertex);
//  mat3 tmpvar_4;
//  tmpvar_4[0] = _Object2World[0].xyz;
//  tmpvar_4[1] = _Object2World[1].xyz;
//  tmpvar_4[2] = _Object2World[2].xyz;
//  highp vec3 tmpvar_5;
//  tmpvar_5 = (tmpvar_4 * (normalize (_glesNormal) * unity_Scale.w));
//  tmpvar_1 = tmpvar_5;
//  highp vec4 tmpvar_6;
//  tmpvar_6.w = 1.0;
//  tmpvar_6.xyz = tmpvar_5;
//  mediump vec3 tmpvar_7;
//  mediump vec4 normal;
//  normal = tmpvar_6;
//  mediump vec3 x3;
//  highp float vC;
//  mediump vec3 x2;
//  mediump vec3 x1;
//  highp float tmpvar_8;
//  tmpvar_8 = dot (unity_SHAr, normal);
//  x1.x = tmpvar_8;
//  highp float tmpvar_9;
//  tmpvar_9 = dot (unity_SHAg, normal);
//  x1.y = tmpvar_9;
//  highp float tmpvar_10;
//  tmpvar_10 = dot (unity_SHAb, normal);
//  x1.z = tmpvar_10;
//  mediump vec4 tmpvar_11;
//  tmpvar_11 = (normal.xyzz * normal.yzzx);
//  highp float tmpvar_12;
//  tmpvar_12 = dot (unity_SHBr, tmpvar_11);
//  x2.x = tmpvar_12;
//  highp float tmpvar_13;
//  tmpvar_13 = dot (unity_SHBg, tmpvar_11);
//  x2.y = tmpvar_13;
//  highp float tmpvar_14;
//  tmpvar_14 = dot (unity_SHBb, tmpvar_11);
//  x2.z = tmpvar_14;
//  mediump float tmpvar_15;
//  tmpvar_15 = ((normal.x * normal.x) - (normal.y * normal.y));
//  vC = tmpvar_15;
//  highp vec3 tmpvar_16;
//  tmpvar_16 = (unity_SHC.xyz * vC);
//  x3 = tmpvar_16;
//  tmpvar_7 = ((x1 + x2) + x3);
//  shlight = tmpvar_7;
//  tmpvar_2 = shlight;
//  highp vec4 o_i0;
//  highp vec4 tmpvar_17;
//  tmpvar_17 = (tmpvar_3 * 0.5);
//  o_i0 = tmpvar_17;
//  highp vec2 tmpvar_18;
//  tmpvar_18.x = tmpvar_17.x;
//  tmpvar_18.y = (tmpvar_17.y * _ProjectionParams.x);
//  o_i0.xy = (tmpvar_18 + tmpvar_17.w);
//  o_i0.zw = tmpvar_3.zw;
//  gl_Position = tmpvar_3;
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = tmpvar_1;
//  xlv_TEXCOORD2 = tmpvar_2;
//  xlv_TEXCOORD3 = o_i0;
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying lowp vec3 xlv_TEXCOORD2;
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  mediump vec4 c_i0_i1;
//  c_i0_i1.xyz = tmpvar_1;
//  c_i0_i1.w = tmpvar_2;
//  c = c_i0_i1;
//  c.xyz = (c.xyz + (tmpvar_1 * xlv_TEXCOORD2));
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying highp vec4 xlv_TEXCOORD2;
//varying highp vec2 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_LightmapST;
//
//uniform highp vec4 _ProjectionParams;
//uniform highp vec4 _MainTex_ST;
//attribute vec4 _glesMultiTexCoord1;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec4 _glesVertex;
//void main ()
//{
//  highp vec4 tmpvar_1;
//  tmpvar_1 = (gl_ModelViewProjectionMatrix * _glesVertex);
//  highp vec4 o_i0;
//  highp vec4 tmpvar_2;
//  tmpvar_2 = (tmpvar_1 * 0.5);
//  o_i0 = tmpvar_2;
//  highp vec2 tmpvar_3;
//  tmpvar_3.x = tmpvar_2.x;
//  tmpvar_3.y = (tmpvar_2.y * _ProjectionParams.x);
//  o_i0.xy = (tmpvar_3 + tmpvar_2.w);
//  o_i0.zw = tmpvar_1.zw;
//  gl_Position = tmpvar_1;
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
//  xlv_TEXCOORD2 = o_i0;
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying highp vec4 xlv_TEXCOORD2;
//varying highp vec2 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D unity_Lightmap;
//uniform sampler2D _ShadowMapTexture;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  c = vec4(0.0, 0.0, 0.0, 0.0);
//  c.xyz = (tmpvar_1 * min ((2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD1).xyz), vec3((texture2DProj (_ShadowMapTexture, xlv_TEXCOORD2).x * 2.0))));
//  c.w = tmpvar_2;
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying lowp vec3 xlv_TEXCOORD2;
//varying lowp vec3 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_Scale;
//uniform highp vec4 unity_SHC;
//uniform highp vec4 unity_SHBr;
//uniform highp vec4 unity_SHBg;
//uniform highp vec4 unity_SHBb;
//uniform highp vec4 unity_SHAr;
//uniform highp vec4 unity_SHAg;
//uniform highp vec4 unity_SHAb;
//uniform highp vec4 unity_LightColor[4];
//uniform highp vec4 unity_4LightPosZ0;
//uniform highp vec4 unity_4LightPosY0;
//uniform highp vec4 unity_4LightPosX0;
//uniform highp vec4 unity_4LightAtten0;
//
//uniform highp mat4 _Object2World;
//uniform highp vec4 _MainTex_ST;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec3 _glesNormal;
//attribute vec4 _glesVertex;
//void main ()
//{
//  highp vec3 shlight;
//  lowp vec3 tmpvar_1;
//  lowp vec3 tmpvar_2;
//  mat3 tmpvar_3;
//  tmpvar_3[0] = _Object2World[0].xyz;
//  tmpvar_3[1] = _Object2World[1].xyz;
//  tmpvar_3[2] = _Object2World[2].xyz;
//  highp vec3 tmpvar_4;
//  tmpvar_4 = (tmpvar_3 * (normalize (_glesNormal) * unity_Scale.w));
//  tmpvar_1 = tmpvar_4;
//  highp vec4 tmpvar_5;
//  tmpvar_5.w = 1.0;
//  tmpvar_5.xyz = tmpvar_4;
//  mediump vec3 tmpvar_6;
//  mediump vec4 normal;
//  normal = tmpvar_5;
//  mediump vec3 x3;
//  highp float vC;
//  mediump vec3 x2;
//  mediump vec3 x1;
//  highp float tmpvar_7;
//  tmpvar_7 = dot (unity_SHAr, normal);
//  x1.x = tmpvar_7;
//  highp float tmpvar_8;
//  tmpvar_8 = dot (unity_SHAg, normal);
//  x1.y = tmpvar_8;
//  highp float tmpvar_9;
//  tmpvar_9 = dot (unity_SHAb, normal);
//  x1.z = tmpvar_9;
//  mediump vec4 tmpvar_10;
//  tmpvar_10 = (normal.xyzz * normal.yzzx);
//  highp float tmpvar_11;
//  tmpvar_11 = dot (unity_SHBr, tmpvar_10);
//  x2.x = tmpvar_11;
//  highp float tmpvar_12;
//  tmpvar_12 = dot (unity_SHBg, tmpvar_10);
//  x2.y = tmpvar_12;
//  highp float tmpvar_13;
//  tmpvar_13 = dot (unity_SHBb, tmpvar_10);
//  x2.z = tmpvar_13;
//  mediump float tmpvar_14;
//  tmpvar_14 = ((normal.x * normal.x) - (normal.y * normal.y));
//  vC = tmpvar_14;
//  highp vec3 tmpvar_15;
//  tmpvar_15 = (unity_SHC.xyz * vC);
//  x3 = tmpvar_15;
//  tmpvar_6 = ((x1 + x2) + x3);
//  shlight = tmpvar_6;
//  tmpvar_2 = shlight;
//  highp vec3 tmpvar_16;
//  tmpvar_16 = (_Object2World * _glesVertex).xyz;
//  highp vec4 tmpvar_17;
//  tmpvar_17 = (unity_4LightPosX0 - tmpvar_16.x);
//  highp vec4 tmpvar_18;
//  tmpvar_18 = (unity_4LightPosY0 - tmpvar_16.y);
//  highp vec4 tmpvar_19;
//  tmpvar_19 = (unity_4LightPosZ0 - tmpvar_16.z);
//  highp vec4 tmpvar_20;
//  tmpvar_20 = (((tmpvar_17 * tmpvar_17) + (tmpvar_18 * tmpvar_18)) + (tmpvar_19 * tmpvar_19));
//  highp vec4 tmpvar_21;
//  tmpvar_21 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_17 * tmpvar_4.x) + (tmpvar_18 * tmpvar_4.y)) + (tmpvar_19 * tmpvar_4.z)) * inversesqrt (tmpvar_20))) * (1.0/((1.0 + (tmpvar_20 * unity_4LightAtten0)))));
//  highp vec3 tmpvar_22;
//  tmpvar_22 = (tmpvar_2 + ((((unity_LightColor[0].xyz * tmpvar_21.x) + (unity_LightColor[1].xyz * tmpvar_21.y)) + (unity_LightColor[2].xyz * tmpvar_21.z)) + (unity_LightColor[3].xyz * tmpvar_21.w)));
//  tmpvar_2 = tmpvar_22;
//  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = tmpvar_1;
//  xlv_TEXCOORD2 = tmpvar_2;
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying lowp vec3 xlv_TEXCOORD2;
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  mediump vec4 c_i0_i1;
//  c_i0_i1.xyz = tmpvar_1;
//  c_i0_i1.w = tmpvar_2;
//  c = c_i0_i1;
//  c.xyz = (c.xyz + (tmpvar_1 * xlv_TEXCOORD2));
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying highp vec4 xlv_TEXCOORD3;
//varying lowp vec3 xlv_TEXCOORD2;
//varying lowp vec3 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_Scale;
//uniform highp vec4 unity_SHC;
//uniform highp vec4 unity_SHBr;
//uniform highp vec4 unity_SHBg;
//uniform highp vec4 unity_SHBb;
//uniform highp vec4 unity_SHAr;
//uniform highp vec4 unity_SHAg;
//uniform highp vec4 unity_SHAb;
//uniform highp vec4 unity_LightColor[4];
//uniform highp vec4 unity_4LightPosZ0;
//uniform highp vec4 unity_4LightPosY0;
//uniform highp vec4 unity_4LightPosX0;
//uniform highp vec4 unity_4LightAtten0;
//
//uniform highp vec4 _ProjectionParams;
//uniform highp mat4 _Object2World;
//uniform highp vec4 _MainTex_ST;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec3 _glesNormal;
//attribute vec4 _glesVertex;
//void main ()
//{
//  highp vec3 shlight;
//  lowp vec3 tmpvar_1;
//  lowp vec3 tmpvar_2;
//  highp vec4 tmpvar_3;
//  tmpvar_3 = (gl_ModelViewProjectionMatrix * _glesVertex);
//  mat3 tmpvar_4;
//  tmpvar_4[0] = _Object2World[0].xyz;
//  tmpvar_4[1] = _Object2World[1].xyz;
//  tmpvar_4[2] = _Object2World[2].xyz;
//  highp vec3 tmpvar_5;
//  tmpvar_5 = (tmpvar_4 * (normalize (_glesNormal) * unity_Scale.w));
//  tmpvar_1 = tmpvar_5;
//  highp vec4 tmpvar_6;
//  tmpvar_6.w = 1.0;
//  tmpvar_6.xyz = tmpvar_5;
//  mediump vec3 tmpvar_7;
//  mediump vec4 normal;
//  normal = tmpvar_6;
//  mediump vec3 x3;
//  highp float vC;
//  mediump vec3 x2;
//  mediump vec3 x1;
//  highp float tmpvar_8;
//  tmpvar_8 = dot (unity_SHAr, normal);
//  x1.x = tmpvar_8;
//  highp float tmpvar_9;
//  tmpvar_9 = dot (unity_SHAg, normal);
//  x1.y = tmpvar_9;
//  highp float tmpvar_10;
//  tmpvar_10 = dot (unity_SHAb, normal);
//  x1.z = tmpvar_10;
//  mediump vec4 tmpvar_11;
//  tmpvar_11 = (normal.xyzz * normal.yzzx);
//  highp float tmpvar_12;
//  tmpvar_12 = dot (unity_SHBr, tmpvar_11);
//  x2.x = tmpvar_12;
//  highp float tmpvar_13;
//  tmpvar_13 = dot (unity_SHBg, tmpvar_11);
//  x2.y = tmpvar_13;
//  highp float tmpvar_14;
//  tmpvar_14 = dot (unity_SHBb, tmpvar_11);
//  x2.z = tmpvar_14;
//  mediump float tmpvar_15;
//  tmpvar_15 = ((normal.x * normal.x) - (normal.y * normal.y));
//  vC = tmpvar_15;
//  highp vec3 tmpvar_16;
//  tmpvar_16 = (unity_SHC.xyz * vC);
//  x3 = tmpvar_16;
//  tmpvar_7 = ((x1 + x2) + x3);
//  shlight = tmpvar_7;
//  tmpvar_2 = shlight;
//  highp vec3 tmpvar_17;
//  tmpvar_17 = (_Object2World * _glesVertex).xyz;
//  highp vec4 tmpvar_18;
//  tmpvar_18 = (unity_4LightPosX0 - tmpvar_17.x);
//  highp vec4 tmpvar_19;
//  tmpvar_19 = (unity_4LightPosY0 - tmpvar_17.y);
//  highp vec4 tmpvar_20;
//  tmpvar_20 = (unity_4LightPosZ0 - tmpvar_17.z);
//  highp vec4 tmpvar_21;
//  tmpvar_21 = (((tmpvar_18 * tmpvar_18) + (tmpvar_19 * tmpvar_19)) + (tmpvar_20 * tmpvar_20));
//  highp vec4 tmpvar_22;
//  tmpvar_22 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_18 * tmpvar_5.x) + (tmpvar_19 * tmpvar_5.y)) + (tmpvar_20 * tmpvar_5.z)) * inversesqrt (tmpvar_21))) * (1.0/((1.0 + (tmpvar_21 * unity_4LightAtten0)))));
//  highp vec3 tmpvar_23;
//  tmpvar_23 = (tmpvar_2 + ((((unity_LightColor[0].xyz * tmpvar_22.x) + (unity_LightColor[1].xyz * tmpvar_22.y)) + (unity_LightColor[2].xyz * tmpvar_22.z)) + (unity_LightColor[3].xyz * tmpvar_22.w)));
//  tmpvar_2 = tmpvar_23;
//  highp vec4 o_i0;
//  highp vec4 tmpvar_24;
//  tmpvar_24 = (tmpvar_3 * 0.5);
//  o_i0 = tmpvar_24;
//  highp vec2 tmpvar_25;
//  tmpvar_25.x = tmpvar_24.x;
//  tmpvar_25.y = (tmpvar_24.y * _ProjectionParams.x);
//  o_i0.xy = (tmpvar_25 + tmpvar_24.w);
//  o_i0.zw = tmpvar_3.zw;
//  gl_Position = tmpvar_3;
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = tmpvar_1;
//  xlv_TEXCOORD2 = tmpvar_2;
//  xlv_TEXCOORD3 = o_i0;
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying lowp vec3 xlv_TEXCOORD2;
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  mediump vec4 c_i0_i1;
//  c_i0_i1.xyz = tmpvar_1;
//  c_i0_i1.w = tmpvar_2;
//  c = c_i0_i1;
//  c.xyz = (c.xyz + (tmpvar_1 * xlv_TEXCOORD2));
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//}
//Program "fp" {
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//"!!GLES"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//"!!GLES"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//"!!GLES"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//"!!GLES"
//}
//}
// }
// Pass {
//  Name "FORWARD"
//  Tags { "LIGHTMODE"="ForwardAdd" "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
//  ZWrite Off
//  Fog {
//   Color (0,0,0,0)
//  }
//  Blend One One
//Program "vp" {
//SubProgram "gles " {
//Keywords { "POINT" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying highp vec3 xlv_TEXCOORD3;
//varying mediump vec3 xlv_TEXCOORD2;
//varying lowp vec3 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_Scale;
//
//uniform highp vec4 _WorldSpaceLightPos0;
//uniform highp mat4 _Object2World;
//uniform highp vec4 _MainTex_ST;
//uniform highp mat4 _LightMatrix0;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec3 _glesNormal;
//attribute vec4 _glesVertex;
//void main ()
//{
//  lowp vec3 tmpvar_1;
//  mediump vec3 tmpvar_2;
//  mat3 tmpvar_3;
//  tmpvar_3[0] = _Object2World[0].xyz;
//  tmpvar_3[1] = _Object2World[1].xyz;
//  tmpvar_3[2] = _Object2World[2].xyz;
//  highp vec3 tmpvar_4;
//  tmpvar_4 = (tmpvar_3 * (normalize (_glesNormal) * unity_Scale.w));
//  tmpvar_1 = tmpvar_4;
//  highp vec3 tmpvar_5;
//  tmpvar_5 = (_WorldSpaceLightPos0.xyz - (_Object2World * _glesVertex).xyz);
//  tmpvar_2 = tmpvar_5;
//  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = tmpvar_1;
//  xlv_TEXCOORD2 = tmpvar_2;
//  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  mediump vec4 c_i0_i1;
//  c_i0_i1.xyz = tmpvar_1;
//  c_i0_i1.w = tmpvar_2;
//  c = c_i0_i1;
//  c.w = 0.0;
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying mediump vec3 xlv_TEXCOORD2;
//varying lowp vec3 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_Scale;
//
//uniform lowp vec4 _WorldSpaceLightPos0;
//uniform highp mat4 _Object2World;
//uniform highp vec4 _MainTex_ST;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec3 _glesNormal;
//attribute vec4 _glesVertex;
//void main ()
//{
//  lowp vec3 tmpvar_1;
//  mediump vec3 tmpvar_2;
//  mat3 tmpvar_3;
//  tmpvar_3[0] = _Object2World[0].xyz;
//  tmpvar_3[1] = _Object2World[1].xyz;
//  tmpvar_3[2] = _Object2World[2].xyz;
//  highp vec3 tmpvar_4;
//  tmpvar_4 = (tmpvar_3 * (normalize (_glesNormal) * unity_Scale.w));
//  tmpvar_1 = tmpvar_4;
//  highp vec3 tmpvar_5;
//  tmpvar_5 = _WorldSpaceLightPos0.xyz;
//  tmpvar_2 = tmpvar_5;
//  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = tmpvar_1;
//  xlv_TEXCOORD2 = tmpvar_2;
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  mediump vec4 c_i0_i1;
//  c_i0_i1.xyz = tmpvar_1;
//  c_i0_i1.w = tmpvar_2;
//  c = c_i0_i1;
//  c.w = 0.0;
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//SubProgram "gles " {
//Keywords { "SPOT" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying highp vec4 xlv_TEXCOORD3;
//varying mediump vec3 xlv_TEXCOORD2;
//varying lowp vec3 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_Scale;
//
//uniform highp vec4 _WorldSpaceLightPos0;
//uniform highp mat4 _Object2World;
//uniform highp vec4 _MainTex_ST;
//uniform highp mat4 _LightMatrix0;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec3 _glesNormal;
//attribute vec4 _glesVertex;
//void main ()
//{
//  lowp vec3 tmpvar_1;
//  mediump vec3 tmpvar_2;
//  mat3 tmpvar_3;
//  tmpvar_3[0] = _Object2World[0].xyz;
//  tmpvar_3[1] = _Object2World[1].xyz;
//  tmpvar_3[2] = _Object2World[2].xyz;
//  highp vec3 tmpvar_4;
//  tmpvar_4 = (tmpvar_3 * (normalize (_glesNormal) * unity_Scale.w));
//  tmpvar_1 = tmpvar_4;
//  highp vec3 tmpvar_5;
//  tmpvar_5 = (_WorldSpaceLightPos0.xyz - (_Object2World * _glesVertex).xyz);
//  tmpvar_2 = tmpvar_5;
//  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = tmpvar_1;
//  xlv_TEXCOORD2 = tmpvar_2;
//  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex));
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  mediump vec4 c_i0_i1;
//  c_i0_i1.xyz = tmpvar_1;
//  c_i0_i1.w = tmpvar_2;
//  c = c_i0_i1;
//  c.w = 0.0;
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//SubProgram "gles " {
//Keywords { "POINT_COOKIE" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying highp vec3 xlv_TEXCOORD3;
//varying mediump vec3 xlv_TEXCOORD2;
//varying lowp vec3 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_Scale;
//
//uniform highp vec4 _WorldSpaceLightPos0;
//uniform highp mat4 _Object2World;
//uniform highp vec4 _MainTex_ST;
//uniform highp mat4 _LightMatrix0;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec3 _glesNormal;
//attribute vec4 _glesVertex;
//void main ()
//{
//  lowp vec3 tmpvar_1;
//  mediump vec3 tmpvar_2;
//  mat3 tmpvar_3;
//  tmpvar_3[0] = _Object2World[0].xyz;
//  tmpvar_3[1] = _Object2World[1].xyz;
//  tmpvar_3[2] = _Object2World[2].xyz;
//  highp vec3 tmpvar_4;
//  tmpvar_4 = (tmpvar_3 * (normalize (_glesNormal) * unity_Scale.w));
//  tmpvar_1 = tmpvar_4;
//  highp vec3 tmpvar_5;
//  tmpvar_5 = (_WorldSpaceLightPos0.xyz - (_Object2World * _glesVertex).xyz);
//  tmpvar_2 = tmpvar_5;
//  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = tmpvar_1;
//  xlv_TEXCOORD2 = tmpvar_2;
//  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  mediump vec4 c_i0_i1;
//  c_i0_i1.xyz = tmpvar_1;
//  c_i0_i1.w = tmpvar_2;
//  c = c_i0_i1;
//  c.w = 0.0;
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL_COOKIE" }
//"!!GLES
//#define SHADER_API_GLES 1
//#define tex2D texture2D
//
//
//#ifdef VERTEX
//#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
//uniform mat4 glstate_matrix_mvp;
//
//varying highp vec2 xlv_TEXCOORD3;
//varying mediump vec3 xlv_TEXCOORD2;
//varying lowp vec3 xlv_TEXCOORD1;
//varying highp vec2 xlv_TEXCOORD0;
//uniform highp vec4 unity_Scale;
//
//uniform lowp vec4 _WorldSpaceLightPos0;
//uniform highp mat4 _Object2World;
//uniform highp vec4 _MainTex_ST;
//uniform highp mat4 _LightMatrix0;
//attribute vec4 _glesMultiTexCoord0;
//attribute vec3 _glesNormal;
//attribute vec4 _glesVertex;
//void main ()
//{
//  lowp vec3 tmpvar_1;
//  mediump vec3 tmpvar_2;
//  mat3 tmpvar_3;
//  tmpvar_3[0] = _Object2World[0].xyz;
//  tmpvar_3[1] = _Object2World[1].xyz;
//  tmpvar_3[2] = _Object2World[2].xyz;
//  highp vec3 tmpvar_4;
//  tmpvar_4 = (tmpvar_3 * (normalize (_glesNormal) * unity_Scale.w));
//  tmpvar_1 = tmpvar_4;
//  highp vec3 tmpvar_5;
//  tmpvar_5 = _WorldSpaceLightPos0.xyz;
//  tmpvar_2 = tmpvar_5;
//  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
//  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
//  xlv_TEXCOORD1 = tmpvar_1;
//  xlv_TEXCOORD2 = tmpvar_2;
//  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xy;
//}
//
//
//
//#endif
//#ifdef FRAGMENT
//
//varying highp vec2 xlv_TEXCOORD0;
//uniform sampler2D _MainTex;
//uniform highp vec4 _Color;
//void main ()
//{
//  lowp vec4 c;
//  lowp vec3 tmpvar_1;
//  lowp float tmpvar_2;
//  mediump vec4 c_i0;
//  lowp vec4 tmpvar_3;
//  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
//  c_i0 = tmpvar_3;
//  mediump vec3 tmpvar_4;
//  tmpvar_4 = c_i0.xyz;
//  tmpvar_1 = tmpvar_4;
//  highp float tmpvar_5;
//  tmpvar_5 = (_Color.w * c_i0.w);
//  tmpvar_2 = tmpvar_5;
//  mediump vec4 c_i0_i1;
//  c_i0_i1.xyz = tmpvar_1;
//  c_i0_i1.w = tmpvar_2;
//  c = c_i0_i1;
//  c.w = 0.0;
//  gl_FragData[0] = c;
//}
//
//
//
//#endif"
//}
//}
//Program "fp" {
//SubProgram "gles " {
//Keywords { "POINT" }
//"!!GLES"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL" }
//"!!GLES"
//}
//SubProgram "gles " {
//Keywords { "SPOT" }
//"!!GLES"
//}
//SubProgram "gles " {
//Keywords { "POINT_COOKIE" }
//"!!GLES"
//}
//SubProgram "gles " {
//Keywords { "DIRECTIONAL_COOKIE" }
//"!!GLES"
//}
//}
// }
//}
//}