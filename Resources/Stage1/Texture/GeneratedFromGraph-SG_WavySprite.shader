Shader "SG_WavySprite"
    {
        Properties
        {
            _DistortSpeed("DistortSpeed", Float) = 1
            [NoScaleOffset]_DistortNormal("DistortNormal", 2D) = "white" {}
            _DistortSize("DistortSize", Vector) = (0.1, 0.1, 0, 0)
            [NoScaleOffset]_MainTex("MainTex", 2D) = "white" {}
            _DistortPosition("DistortPosition", Vector) = (0, 0, 0, 0)
            _SpriteTilling("SpriteTilling", Vector) = (0, 0, 0, 0)
            _SpriteOffset("SpriteOffset", Vector) = (0, 0, 0, 0)
            [HDR]_Emisson("Emisson", Color) = (1, 1, 1, 0)
            _Opacity("Opacity", Float) = 0
            [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
            [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
            [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
        }
        SubShader
        {
            Tags
            {
                "RenderPipeline"="UniversalPipeline"
                "RenderType"="Transparent"
                "UniversalMaterialType" = "Lit"
                "Queue"="Transparent"
                "ShaderGraphShader"="true"
                "ShaderGraphTargetId"="UniversalSpriteLitSubTarget"
            }
            Pass
            {
                Name "Sprite Lit"
                Tags
                {
                    "LightMode" = "Universal2D"
                }
            
            // Render State
            Cull Off
                Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
                ZTest LEqual
                ZWrite Off
            
            // Debug
            // <None>
            
            // --------------------------------------------------
            // Copyright Nyan$nail
            // Pass
            
            HLSLPROGRAM
            
            // Pragmas
            #pragma target 2.0
                #pragma exclude_renderers d3d11_9x
                #pragma vertex vert
                #pragma fragment frag
            
            // Keywords
            #pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_0
                #pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_1
                #pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_2
                #pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_3
                #pragma multi_compile_fragment _ DEBUG_DISPLAY
            // GraphKeywords: <None>
            
            // Defines
            
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
            #define VARYINGS_NEED_SCREENPOSITION
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_SPRITELIT
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
            
            
            // custom interpolator pre-include
            /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
            
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            
            // --------------------------------------------------
            // Structs and Packing
            
            // custom interpolators pre packing
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
            
            struct Attributes
                {
                     float3 positionOS : POSITION;
                     float3 normalOS : NORMAL;
                     float4 tangentOS : TANGENT;
                     float4 uv0 : TEXCOORD0;
                     float4 color : COLOR;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : INSTANCEID_SEMANTIC;
                    #endif
                };
                struct Varyings
                {
                     float4 positionCS : SV_POSITION;
                     float3 positionWS;
                     float4 texCoord0;
                     float4 color;
                     float4 screenPosition;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
                struct SurfaceDescriptionInputs
                {
                     float3 ObjectSpacePosition;
                     float4 uv0;
                     float3 TimeParameters;
                };
                struct VertexDescriptionInputs
                {
                     float3 ObjectSpaceNormal;
                     float3 ObjectSpaceTangent;
                     float3 ObjectSpacePosition;
                };
                struct PackedVaryings
                {
                     float4 positionCS : SV_POSITION;
                     float3 interp0 : INTERP0;
                     float4 interp1 : INTERP1;
                     float4 interp2 : INTERP2;
                     float4 interp3 : INTERP3;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
            
            PackedVaryings PackVaryings (Varyings input)
                {
                    PackedVaryings output;
                    ZERO_INITIALIZE(PackedVaryings, output);
                    output.positionCS = input.positionCS;
                    output.interp0.xyz =  input.positionWS;
                    output.interp1.xyzw =  input.texCoord0;
                    output.interp2.xyzw =  input.color;
                    output.interp3.xyzw =  input.screenPosition;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
                Varyings UnpackVaryings (PackedVaryings input)
                {
                    Varyings output;
                    output.positionCS = input.positionCS;
                    output.positionWS = input.interp0.xyz;
                    output.texCoord0 = input.interp1.xyzw;
                    output.color = input.interp2.xyzw;
                    output.screenPosition = input.interp3.xyzw;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
            
            // --------------------------------------------------
            // Graph
            
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
                float _DistortSpeed;
                float4 _DistortNormal_TexelSize;
                float2 _DistortSize;
                float4 _MainTex_TexelSize;
                float2 _DistortPosition;
                float2 _SpriteTilling;
                float2 _SpriteOffset;
                float4 _Emisson;
                float _Opacity;
                CBUFFER_END
                
                // Object and Global properties
                SAMPLER(SamplerState_Linear_Repeat);
                TEXTURE2D(_DistortNormal);
                SAMPLER(sampler_DistortNormal);
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
            
            // Graph Includes
            // GraphIncludes: <None>
            
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
            
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
            
            // Graph Functions
            
                void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Multiply_float_float(float A, float B, out float Out)
                {
                    Out = A * B;
                }
                
                void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
                {
                    Out = UV * Tiling + Offset;
                }
                
                void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Add_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A + B;
                }
                
                void Unity_Blend_Overlay_float4(float4 Base, float4 Blend, out float4 Out, float Opacity)
                {
                    float4 result1 = 1.0 - 2.0 * (1.0 - Base) * (1.0 - Blend);
                    float4 result2 = 2.0 * Base * Blend;
                    float4 zeroOrOne = step(Base, 0.5);
                    Out = result2 * zeroOrOne + (1 - zeroOrOne) * result1;
                    Out = lerp(Base, Out, Opacity);
                }
            
            // Custom interpolators pre vertex
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
            
            // Graph Vertex
            struct VertexDescription
                {
                    float3 Position;
                    float3 Normal;
                    float3 Tangent;
                };
                
                VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                {
                    VertexDescription description = (VertexDescription)0;
                    description.Position = IN.ObjectSpacePosition;
                    description.Normal = IN.ObjectSpaceNormal;
                    description.Tangent = IN.ObjectSpaceTangent;
                    return description;
                }
            
            // Custom interpolators, pre surface
            #ifdef FEATURES_GRAPH_VERTEX
            Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
            {
            return output;
            }
            #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
            #endif
            
            // Graph Pixel
            struct SurfaceDescription
                {
                    float3 BaseColor;
                    float Alpha;
                    float4 SpriteMask;
                };
                
                SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                {
                    SurfaceDescription surface = (SurfaceDescription)0;
                    UnityTexture2D _Property_0dee077884704c658b38d1e672c5a1c1_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    float4 _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_0dee077884704c658b38d1e672c5a1c1_Out_0.tex, _Property_0dee077884704c658b38d1e672c5a1c1_Out_0.samplerstate, _Property_0dee077884704c658b38d1e672c5a1c1_Out_0.GetTransformedUV(IN.uv0.xy));
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_R_4 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.r;
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_G_5 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.g;
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_B_6 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.b;
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_A_7 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.a;
                    float4 _Property_f0a0f083baf240e995ead5f1eaaf0ef1_Out_0 = IsGammaSpace() ? LinearToSRGB(_Emisson) : _Emisson;
                    float4 _Multiply_ffe62072630f424e85a61bef53400569_Out_2;
                    Unity_Multiply_float4_float4((_SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_R_4.xxxx), _Property_f0a0f083baf240e995ead5f1eaaf0ef1_Out_0, _Multiply_ffe62072630f424e85a61bef53400569_Out_2);
                    UnityTexture2D _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    UnityTexture2D _Property_d654e6607acb4a4faf2750efb69bc674_Out_0 = UnityBuildTexture2DStructNoScale(_DistortNormal);
                    float2 _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0 = _DistortSize;
                    float _Property_4e977330ff634999a1feeb748ad85871_Out_0 = _DistortSpeed;
                    float _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2;
                    Unity_Multiply_float_float(IN.TimeParameters.x, _Property_4e977330ff634999a1feeb748ad85871_Out_0, _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2);
                    float2 _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0, (_Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2.xx), _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3);
                    float4 _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d654e6607acb4a4faf2750efb69bc674_Out_0.tex, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.samplerstate, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.GetTransformedUV(_TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3));
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_R_4 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.r;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_G_5 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.g;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_B_6 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.b;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_A_7 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.a;
                    float3 _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2;
                    Unity_Multiply_float3_float3(IN.ObjectSpacePosition, (_SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.xyz), _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2);
                    float2 _Property_35c0b9826e4c40b4a98576290da7f863_Out_0 = _DistortPosition;
                    float2 _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2;
                    Unity_Multiply_float2_float2((_Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2.xy), _Property_35c0b9826e4c40b4a98576290da7f863_Out_0, _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2);
                    float2 _Add_6fbb0b737f214a2dad54497313500ffc_Out_2;
                    Unity_Add_float2((IN.ObjectSpacePosition.xy), _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2, _Add_6fbb0b737f214a2dad54497313500ffc_Out_2);
                    float2 _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0 = _SpriteTilling;
                    float2 _Property_74a313c063ea461daf6b79f48bcbe376_Out_0 = _SpriteOffset;
                    float2 _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0, _Property_74a313c063ea461daf6b79f48bcbe376_Out_0, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3);
                    float2 _Add_b99143d9640d4957888c0a14b766b6cc_Out_2;
                    Unity_Add_float2(_Add_6fbb0b737f214a2dad54497313500ffc_Out_2, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3, _Add_b99143d9640d4957888c0a14b766b6cc_Out_2);
                    float4 _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0 = SAMPLE_TEXTURE2D(_Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.tex, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.samplerstate, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.GetTransformedUV(_Add_b99143d9640d4957888c0a14b766b6cc_Out_2));
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_R_4 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.r;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_G_5 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.g;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_B_6 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.b;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.a;
                    float _Property_505f1cc01d044127a8c273551910e986_Out_0 = _Opacity;
                    float4 _Blend_3fcc86e24b3a4a77bdac7854f6ce396c_Out_2;
                    Unity_Blend_Overlay_float4(_Multiply_ffe62072630f424e85a61bef53400569_Out_2, _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0, _Blend_3fcc86e24b3a4a77bdac7854f6ce396c_Out_2, _Property_505f1cc01d044127a8c273551910e986_Out_0);
                    surface.BaseColor = (_Blend_3fcc86e24b3a4a77bdac7854f6ce396c_Out_2.xyz);
                    surface.Alpha = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7;
                    surface.SpriteMask = IsGammaSpace() ? float4(1, 1, 1, 1) : float4 (SRGBToLinear(float3(1, 1, 1)), 1);
                    return surface;
                }
            
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES Attributes
            #define VFX_SRP_VARYINGS Varyings
            #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
            #endif
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                {
                    VertexDescriptionInputs output;
                    ZERO_INITIALIZE(VertexDescriptionInputs, output);
                
                    output.ObjectSpaceNormal =                          input.normalOS;
                    output.ObjectSpaceTangent =                         input.tangentOS.xyz;
                    output.ObjectSpacePosition =                        input.positionOS;
                
                    return output;
                }
                
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
                
                #ifdef HAVE_VFX_MODIFICATION
                    // FragInputs from VFX come from two places: Interpolator or CBuffer.
                    /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
                
                #endif
                
                    
                
                
                
                
                
                    output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                
                    #if UNITY_UV_STARTS_AT_TOP
                    #else
                    #endif
                
                
                    output.uv0 = input.texCoord0;
                    output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                #else
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                #endif
                #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                
                        return output;
                }
                
            
            // --------------------------------------------------
            // Main
            
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteLitPass.hlsl"
            
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
            #ifdef HAVE_VFX_MODIFICATION
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
            #endif
            
            ENDHLSL
            }
            Pass
            {
                Name "Sprite Normal"
                Tags
                {
                    "LightMode" = "NormalsRendering"
                }
            
            // Render State
            Cull Off
                Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
                ZTest LEqual
                ZWrite Off
            
            // Debug
            // <None>
            
            // --------------------------------------------------
            // Pass
            
            HLSLPROGRAM
            
            // Pragmas
            #pragma target 2.0
                #pragma exclude_renderers d3d11_9x
                #pragma vertex vert
                #pragma fragment frag
            
            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>
            
            // Defines
            
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_SPRITENORMAL
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
            
            
            // custom interpolator pre-include
            /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
            
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            
            // --------------------------------------------------
            // Structs and Packing
            
            // custom interpolators pre packing
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
            
            struct Attributes
                {
                     float3 positionOS : POSITION;
                     float3 normalOS : NORMAL;
                     float4 tangentOS : TANGENT;
                     float4 uv0 : TEXCOORD0;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : INSTANCEID_SEMANTIC;
                    #endif
                };
                struct Varyings
                {
                     float4 positionCS : SV_POSITION;
                     float3 positionWS;
                     float3 normalWS;
                     float4 tangentWS;
                     float4 texCoord0;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
                struct SurfaceDescriptionInputs
                {
                     float3 TangentSpaceNormal;
                     float3 ObjectSpacePosition;
                     float4 uv0;
                     float3 TimeParameters;
                };
                struct VertexDescriptionInputs
                {
                     float3 ObjectSpaceNormal;
                     float3 ObjectSpaceTangent;
                     float3 ObjectSpacePosition;
                };
                struct PackedVaryings
                {
                     float4 positionCS : SV_POSITION;
                     float3 interp0 : INTERP0;
                     float3 interp1 : INTERP1;
                     float4 interp2 : INTERP2;
                     float4 interp3 : INTERP3;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
            
            PackedVaryings PackVaryings (Varyings input)
                {
                    PackedVaryings output;
                    ZERO_INITIALIZE(PackedVaryings, output);
                    output.positionCS = input.positionCS;
                    output.interp0.xyz =  input.positionWS;
                    output.interp1.xyz =  input.normalWS;
                    output.interp2.xyzw =  input.tangentWS;
                    output.interp3.xyzw =  input.texCoord0;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
                Varyings UnpackVaryings (PackedVaryings input)
                {
                    Varyings output;
                    output.positionCS = input.positionCS;
                    output.positionWS = input.interp0.xyz;
                    output.normalWS = input.interp1.xyz;
                    output.tangentWS = input.interp2.xyzw;
                    output.texCoord0 = input.interp3.xyzw;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
            
            // --------------------------------------------------
            // Graph
            
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
                float _DistortSpeed;
                float4 _DistortNormal_TexelSize;
                float2 _DistortSize;
                float4 _MainTex_TexelSize;
                float2 _DistortPosition;
                float2 _SpriteTilling;
                float2 _SpriteOffset;
                float4 _Emisson;
                float _Opacity;
                CBUFFER_END
                
                // Object and Global properties
                SAMPLER(SamplerState_Linear_Repeat);
                TEXTURE2D(_DistortNormal);
                SAMPLER(sampler_DistortNormal);
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
            
            // Graph Includes
            // GraphIncludes: <None>
            
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
            
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
            
            // Graph Functions
            
                void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Multiply_float_float(float A, float B, out float Out)
                {
                    Out = A * B;
                }
                
                void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
                {
                    Out = UV * Tiling + Offset;
                }
                
                void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Add_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A + B;
                }
                
                void Unity_Blend_Overlay_float4(float4 Base, float4 Blend, out float4 Out, float Opacity)
                {
                    float4 result1 = 1.0 - 2.0 * (1.0 - Base) * (1.0 - Blend);
                    float4 result2 = 2.0 * Base * Blend;
                    float4 zeroOrOne = step(Base, 0.5);
                    Out = result2 * zeroOrOne + (1 - zeroOrOne) * result1;
                    Out = lerp(Base, Out, Opacity);
                }
            
            // Custom interpolators pre vertex
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
            
            // Graph Vertex
            struct VertexDescription
                {
                    float3 Position;
                    float3 Normal;
                    float3 Tangent;
                };
                
                VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                {
                    VertexDescription description = (VertexDescription)0;
                    description.Position = IN.ObjectSpacePosition;
                    description.Normal = IN.ObjectSpaceNormal;
                    description.Tangent = IN.ObjectSpaceTangent;
                    return description;
                }
            
            // Custom interpolators, pre surface
            #ifdef FEATURES_GRAPH_VERTEX
            Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
            {
            return output;
            }
            #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
            #endif
            
            // Graph Pixel
            struct SurfaceDescription
                {
                    float3 BaseColor;
                    float Alpha;
                    float3 NormalTS;
                };
                
                SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                {
                    SurfaceDescription surface = (SurfaceDescription)0;
                    UnityTexture2D _Property_0dee077884704c658b38d1e672c5a1c1_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    float4 _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_0dee077884704c658b38d1e672c5a1c1_Out_0.tex, _Property_0dee077884704c658b38d1e672c5a1c1_Out_0.samplerstate, _Property_0dee077884704c658b38d1e672c5a1c1_Out_0.GetTransformedUV(IN.uv0.xy));
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_R_4 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.r;
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_G_5 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.g;
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_B_6 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.b;
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_A_7 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.a;
                    float4 _Property_f0a0f083baf240e995ead5f1eaaf0ef1_Out_0 = IsGammaSpace() ? LinearToSRGB(_Emisson) : _Emisson;
                    float4 _Multiply_ffe62072630f424e85a61bef53400569_Out_2;
                    Unity_Multiply_float4_float4((_SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_R_4.xxxx), _Property_f0a0f083baf240e995ead5f1eaaf0ef1_Out_0, _Multiply_ffe62072630f424e85a61bef53400569_Out_2);
                    UnityTexture2D _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    UnityTexture2D _Property_d654e6607acb4a4faf2750efb69bc674_Out_0 = UnityBuildTexture2DStructNoScale(_DistortNormal);
                    float2 _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0 = _DistortSize;
                    float _Property_4e977330ff634999a1feeb748ad85871_Out_0 = _DistortSpeed;
                    float _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2;
                    Unity_Multiply_float_float(IN.TimeParameters.x, _Property_4e977330ff634999a1feeb748ad85871_Out_0, _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2);
                    float2 _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0, (_Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2.xx), _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3);
                    float4 _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d654e6607acb4a4faf2750efb69bc674_Out_0.tex, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.samplerstate, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.GetTransformedUV(_TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3));
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_R_4 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.r;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_G_5 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.g;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_B_6 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.b;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_A_7 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.a;
                    float3 _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2;
                    Unity_Multiply_float3_float3(IN.ObjectSpacePosition, (_SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.xyz), _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2);
                    float2 _Property_35c0b9826e4c40b4a98576290da7f863_Out_0 = _DistortPosition;
                    float2 _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2;
                    Unity_Multiply_float2_float2((_Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2.xy), _Property_35c0b9826e4c40b4a98576290da7f863_Out_0, _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2);
                    float2 _Add_6fbb0b737f214a2dad54497313500ffc_Out_2;
                    Unity_Add_float2((IN.ObjectSpacePosition.xy), _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2, _Add_6fbb0b737f214a2dad54497313500ffc_Out_2);
                    float2 _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0 = _SpriteTilling;
                    float2 _Property_74a313c063ea461daf6b79f48bcbe376_Out_0 = _SpriteOffset;
                    float2 _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0, _Property_74a313c063ea461daf6b79f48bcbe376_Out_0, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3);
                    float2 _Add_b99143d9640d4957888c0a14b766b6cc_Out_2;
                    Unity_Add_float2(_Add_6fbb0b737f214a2dad54497313500ffc_Out_2, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3, _Add_b99143d9640d4957888c0a14b766b6cc_Out_2);
                    float4 _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0 = SAMPLE_TEXTURE2D(_Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.tex, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.samplerstate, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.GetTransformedUV(_Add_b99143d9640d4957888c0a14b766b6cc_Out_2));
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_R_4 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.r;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_G_5 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.g;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_B_6 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.b;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.a;
                    float _Property_505f1cc01d044127a8c273551910e986_Out_0 = _Opacity;
                    float4 _Blend_3fcc86e24b3a4a77bdac7854f6ce396c_Out_2;
                    Unity_Blend_Overlay_float4(_Multiply_ffe62072630f424e85a61bef53400569_Out_2, _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0, _Blend_3fcc86e24b3a4a77bdac7854f6ce396c_Out_2, _Property_505f1cc01d044127a8c273551910e986_Out_0);
                    surface.BaseColor = (_Blend_3fcc86e24b3a4a77bdac7854f6ce396c_Out_2.xyz);
                    surface.Alpha = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7;
                    surface.NormalTS = IN.TangentSpaceNormal;
                    return surface;
                }
            
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES Attributes
            #define VFX_SRP_VARYINGS Varyings
            #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
            #endif
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                {
                    VertexDescriptionInputs output;
                    ZERO_INITIALIZE(VertexDescriptionInputs, output);
                
                    output.ObjectSpaceNormal =                          input.normalOS;
                    output.ObjectSpaceTangent =                         input.tangentOS.xyz;
                    output.ObjectSpacePosition =                        input.positionOS;
                
                    return output;
                }
                
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
                
                #ifdef HAVE_VFX_MODIFICATION
                    // FragInputs from VFX come from two places: Interpolator or CBuffer.
                    /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
                
                #endif
                
                    
                
                
                
                    output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);
                
                
                    output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                
                    #if UNITY_UV_STARTS_AT_TOP
                    #else
                    #endif
                
                
                    output.uv0 = input.texCoord0;
                    output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                #else
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                #endif
                #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                
                        return output;
                }
                
            
            // --------------------------------------------------
            // Main
            
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteNormalPass.hlsl"
            
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
            #ifdef HAVE_VFX_MODIFICATION
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
            #endif
            
            ENDHLSL
            }
            Pass
            {
                Name "SceneSelectionPass"
                Tags
                {
                    "LightMode" = "SceneSelectionPass"
                }
            
            // Render State
            Cull Off
            
            // Debug
            // <None>
            
            // --------------------------------------------------
            // Pass
            
            HLSLPROGRAM
            
            // Pragmas
            #pragma target 2.0
                #pragma exclude_renderers d3d11_9x
                #pragma vertex vert
                #pragma fragment frag
            
            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>
            
            // Defines
            
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_DEPTHONLY
                #define SCENESELECTIONPASS 1
                
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
            
            
            // custom interpolator pre-include
            /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
            
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            
            // --------------------------------------------------
            // Structs and Packing
            
            // custom interpolators pre packing
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
            
            struct Attributes
                {
                     float3 positionOS : POSITION;
                     float3 normalOS : NORMAL;
                     float4 tangentOS : TANGENT;
                     float4 uv0 : TEXCOORD0;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : INSTANCEID_SEMANTIC;
                    #endif
                };
                struct Varyings
                {
                     float4 positionCS : SV_POSITION;
                     float3 positionWS;
                     float4 texCoord0;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
                struct SurfaceDescriptionInputs
                {
                     float3 ObjectSpacePosition;
                     float4 uv0;
                     float3 TimeParameters;
                };
                struct VertexDescriptionInputs
                {
                     float3 ObjectSpaceNormal;
                     float3 ObjectSpaceTangent;
                     float3 ObjectSpacePosition;
                };
                struct PackedVaryings
                {
                     float4 positionCS : SV_POSITION;
                     float3 interp0 : INTERP0;
                     float4 interp1 : INTERP1;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
            
            PackedVaryings PackVaryings (Varyings input)
                {
                    PackedVaryings output;
                    ZERO_INITIALIZE(PackedVaryings, output);
                    output.positionCS = input.positionCS;
                    output.interp0.xyz =  input.positionWS;
                    output.interp1.xyzw =  input.texCoord0;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
                Varyings UnpackVaryings (PackedVaryings input)
                {
                    Varyings output;
                    output.positionCS = input.positionCS;
                    output.positionWS = input.interp0.xyz;
                    output.texCoord0 = input.interp1.xyzw;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
            
            // --------------------------------------------------
            // Graph
            
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
                float _DistortSpeed;
                float4 _DistortNormal_TexelSize;
                float2 _DistortSize;
                float4 _MainTex_TexelSize;
                float2 _DistortPosition;
                float2 _SpriteTilling;
                float2 _SpriteOffset;
                float4 _Emisson;
                float _Opacity;
                CBUFFER_END
                
                // Object and Global properties
                SAMPLER(SamplerState_Linear_Repeat);
                TEXTURE2D(_DistortNormal);
                SAMPLER(sampler_DistortNormal);
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
            
            // Graph Includes
            // GraphIncludes: <None>
            
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
            
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
            
            // Graph Functions
            
                void Unity_Multiply_float_float(float A, float B, out float Out)
                {
                    Out = A * B;
                }
                
                void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
                {
                    Out = UV * Tiling + Offset;
                }
                
                void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Add_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A + B;
                }
            
            // Custom interpolators pre vertex
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
            
            // Graph Vertex
            struct VertexDescription
                {
                    float3 Position;
                    float3 Normal;
                    float3 Tangent;
                };
                
                VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                {
                    VertexDescription description = (VertexDescription)0;
                    description.Position = IN.ObjectSpacePosition;
                    description.Normal = IN.ObjectSpaceNormal;
                    description.Tangent = IN.ObjectSpaceTangent;
                    return description;
                }
            
            // Custom interpolators, pre surface
            #ifdef FEATURES_GRAPH_VERTEX
            Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
            {
            return output;
            }
            #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
            #endif
            
            // Graph Pixel
            struct SurfaceDescription
                {
                    float Alpha;
                };
                
                SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                {
                    SurfaceDescription surface = (SurfaceDescription)0;
                    UnityTexture2D _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    UnityTexture2D _Property_d654e6607acb4a4faf2750efb69bc674_Out_0 = UnityBuildTexture2DStructNoScale(_DistortNormal);
                    float2 _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0 = _DistortSize;
                    float _Property_4e977330ff634999a1feeb748ad85871_Out_0 = _DistortSpeed;
                    float _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2;
                    Unity_Multiply_float_float(IN.TimeParameters.x, _Property_4e977330ff634999a1feeb748ad85871_Out_0, _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2);
                    float2 _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0, (_Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2.xx), _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3);
                    float4 _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d654e6607acb4a4faf2750efb69bc674_Out_0.tex, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.samplerstate, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.GetTransformedUV(_TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3));
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_R_4 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.r;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_G_5 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.g;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_B_6 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.b;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_A_7 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.a;
                    float3 _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2;
                    Unity_Multiply_float3_float3(IN.ObjectSpacePosition, (_SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.xyz), _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2);
                    float2 _Property_35c0b9826e4c40b4a98576290da7f863_Out_0 = _DistortPosition;
                    float2 _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2;
                    Unity_Multiply_float2_float2((_Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2.xy), _Property_35c0b9826e4c40b4a98576290da7f863_Out_0, _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2);
                    float2 _Add_6fbb0b737f214a2dad54497313500ffc_Out_2;
                    Unity_Add_float2((IN.ObjectSpacePosition.xy), _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2, _Add_6fbb0b737f214a2dad54497313500ffc_Out_2);
                    float2 _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0 = _SpriteTilling;
                    float2 _Property_74a313c063ea461daf6b79f48bcbe376_Out_0 = _SpriteOffset;
                    float2 _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0, _Property_74a313c063ea461daf6b79f48bcbe376_Out_0, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3);
                    float2 _Add_b99143d9640d4957888c0a14b766b6cc_Out_2;
                    Unity_Add_float2(_Add_6fbb0b737f214a2dad54497313500ffc_Out_2, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3, _Add_b99143d9640d4957888c0a14b766b6cc_Out_2);
                    float4 _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0 = SAMPLE_TEXTURE2D(_Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.tex, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.samplerstate, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.GetTransformedUV(_Add_b99143d9640d4957888c0a14b766b6cc_Out_2));
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_R_4 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.r;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_G_5 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.g;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_B_6 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.b;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.a;
                    surface.Alpha = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7;
                    return surface;
                }
            
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES Attributes
            #define VFX_SRP_VARYINGS Varyings
            #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
            #endif
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                {
                    VertexDescriptionInputs output;
                    ZERO_INITIALIZE(VertexDescriptionInputs, output);
                
                    output.ObjectSpaceNormal =                          input.normalOS;
                    output.ObjectSpaceTangent =                         input.tangentOS.xyz;
                    output.ObjectSpacePosition =                        input.positionOS;
                
                    return output;
                }
                
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
                
                #ifdef HAVE_VFX_MODIFICATION
                    // FragInputs from VFX come from two places: Interpolator or CBuffer.
                    /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
                
                #endif
                
                    
                
                
                
                
                
                    output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                
                    #if UNITY_UV_STARTS_AT_TOP
                    #else
                    #endif
                
                
                    output.uv0 = input.texCoord0;
                    output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                #else
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                #endif
                #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                
                        return output;
                }
                
            
            // --------------------------------------------------
            // Main
            
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
            
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
            #ifdef HAVE_VFX_MODIFICATION
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
            #endif
            
            ENDHLSL
            }
            Pass
            {
                Name "ScenePickingPass"
                Tags
                {
                    "LightMode" = "Picking"
                }
            
            // Render State
            Cull Off
            
            // Debug
            // <None>
            
            // --------------------------------------------------
            // Pass
            
            HLSLPROGRAM
            
            // Pragmas
            #pragma target 2.0
                #pragma exclude_renderers d3d11_9x
                #pragma vertex vert
                #pragma fragment frag
            
            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>
            
            // Defines
            
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_DEPTHONLY
                #define SCENEPICKINGPASS 1
                
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
            
            
            // custom interpolator pre-include
            /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
            
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            
            // --------------------------------------------------
            // Structs and Packing
            
            // custom interpolators pre packing
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
            
            struct Attributes
                {
                     float3 positionOS : POSITION;
                     float3 normalOS : NORMAL;
                     float4 tangentOS : TANGENT;
                     float4 uv0 : TEXCOORD0;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : INSTANCEID_SEMANTIC;
                    #endif
                };
                struct Varyings
                {
                     float4 positionCS : SV_POSITION;
                     float3 positionWS;
                     float4 texCoord0;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
                struct SurfaceDescriptionInputs
                {
                     float3 ObjectSpacePosition;
                     float4 uv0;
                     float3 TimeParameters;
                };
                struct VertexDescriptionInputs
                {
                     float3 ObjectSpaceNormal;
                     float3 ObjectSpaceTangent;
                     float3 ObjectSpacePosition;
                };
                struct PackedVaryings
                {
                     float4 positionCS : SV_POSITION;
                     float3 interp0 : INTERP0;
                     float4 interp1 : INTERP1;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
            
            PackedVaryings PackVaryings (Varyings input)
                {
                    PackedVaryings output;
                    ZERO_INITIALIZE(PackedVaryings, output);
                    output.positionCS = input.positionCS;
                    output.interp0.xyz =  input.positionWS;
                    output.interp1.xyzw =  input.texCoord0;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
                Varyings UnpackVaryings (PackedVaryings input)
                {
                    Varyings output;
                    output.positionCS = input.positionCS;
                    output.positionWS = input.interp0.xyz;
                    output.texCoord0 = input.interp1.xyzw;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
            
            // --------------------------------------------------
            // Graph
            
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
                float _DistortSpeed;
                float4 _DistortNormal_TexelSize;
                float2 _DistortSize;
                float4 _MainTex_TexelSize;
                float2 _DistortPosition;
                float2 _SpriteTilling;
                float2 _SpriteOffset;
                float4 _Emisson;
                float _Opacity;
                CBUFFER_END
                
                // Object and Global properties
                SAMPLER(SamplerState_Linear_Repeat);
                TEXTURE2D(_DistortNormal);
                SAMPLER(sampler_DistortNormal);
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
            
            // Graph Includes
            // GraphIncludes: <None>
            
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
            
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
            
            // Graph Functions
            
                void Unity_Multiply_float_float(float A, float B, out float Out)
                {
                    Out = A * B;
                }
                
                void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
                {
                    Out = UV * Tiling + Offset;
                }
                
                void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Add_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A + B;
                }
            
            // Custom interpolators pre vertex
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
            
            // Graph Vertex
            struct VertexDescription
                {
                    float3 Position;
                    float3 Normal;
                    float3 Tangent;
                };
                
                VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                {
                    VertexDescription description = (VertexDescription)0;
                    description.Position = IN.ObjectSpacePosition;
                    description.Normal = IN.ObjectSpaceNormal;
                    description.Tangent = IN.ObjectSpaceTangent;
                    return description;
                }
            
            // Custom interpolators, pre surface
            #ifdef FEATURES_GRAPH_VERTEX
            Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
            {
            return output;
            }
            #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
            #endif
            
            // Graph Pixel
            struct SurfaceDescription
                {
                    float Alpha;
                };
                
                SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                {
                    SurfaceDescription surface = (SurfaceDescription)0;
                    UnityTexture2D _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    UnityTexture2D _Property_d654e6607acb4a4faf2750efb69bc674_Out_0 = UnityBuildTexture2DStructNoScale(_DistortNormal);
                    float2 _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0 = _DistortSize;
                    float _Property_4e977330ff634999a1feeb748ad85871_Out_0 = _DistortSpeed;
                    float _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2;
                    Unity_Multiply_float_float(IN.TimeParameters.x, _Property_4e977330ff634999a1feeb748ad85871_Out_0, _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2);
                    float2 _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0, (_Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2.xx), _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3);
                    float4 _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d654e6607acb4a4faf2750efb69bc674_Out_0.tex, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.samplerstate, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.GetTransformedUV(_TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3));
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_R_4 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.r;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_G_5 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.g;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_B_6 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.b;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_A_7 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.a;
                    float3 _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2;
                    Unity_Multiply_float3_float3(IN.ObjectSpacePosition, (_SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.xyz), _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2);
                    float2 _Property_35c0b9826e4c40b4a98576290da7f863_Out_0 = _DistortPosition;
                    float2 _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2;
                    Unity_Multiply_float2_float2((_Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2.xy), _Property_35c0b9826e4c40b4a98576290da7f863_Out_0, _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2);
                    float2 _Add_6fbb0b737f214a2dad54497313500ffc_Out_2;
                    Unity_Add_float2((IN.ObjectSpacePosition.xy), _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2, _Add_6fbb0b737f214a2dad54497313500ffc_Out_2);
                    float2 _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0 = _SpriteTilling;
                    float2 _Property_74a313c063ea461daf6b79f48bcbe376_Out_0 = _SpriteOffset;
                    float2 _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0, _Property_74a313c063ea461daf6b79f48bcbe376_Out_0, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3);
                    float2 _Add_b99143d9640d4957888c0a14b766b6cc_Out_2;
                    Unity_Add_float2(_Add_6fbb0b737f214a2dad54497313500ffc_Out_2, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3, _Add_b99143d9640d4957888c0a14b766b6cc_Out_2);
                    float4 _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0 = SAMPLE_TEXTURE2D(_Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.tex, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.samplerstate, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.GetTransformedUV(_Add_b99143d9640d4957888c0a14b766b6cc_Out_2));
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_R_4 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.r;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_G_5 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.g;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_B_6 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.b;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.a;
                    surface.Alpha = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7;
                    return surface;
                }
            
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES Attributes
            #define VFX_SRP_VARYINGS Varyings
            #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
            #endif
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                {
                    VertexDescriptionInputs output;
                    ZERO_INITIALIZE(VertexDescriptionInputs, output);
                
                    output.ObjectSpaceNormal =                          input.normalOS;
                    output.ObjectSpaceTangent =                         input.tangentOS.xyz;
                    output.ObjectSpacePosition =                        input.positionOS;
                
                    return output;
                }
                
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
                
                #ifdef HAVE_VFX_MODIFICATION
                    // FragInputs from VFX come from two places: Interpolator or CBuffer.
                    /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
                
                #endif
                
                    
                
                
                
                
                
                    output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                
                    #if UNITY_UV_STARTS_AT_TOP
                    #else
                    #endif
                
                
                    output.uv0 = input.texCoord0;
                    output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                #else
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                #endif
                #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                
                        return output;
                }
                
            
            // --------------------------------------------------
            // Main
            
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
            
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
            #ifdef HAVE_VFX_MODIFICATION
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
            #endif
            
            ENDHLSL
            }
            Pass
            {
                Name "Sprite Forward"
                Tags
                {
                    "LightMode" = "UniversalForward"
                }
            
            // Render State
            Cull Off
                Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
                ZTest LEqual
                ZWrite Off
            
            // Debug
            // <None>
            
            // --------------------------------------------------
            // Pass
            
            HLSLPROGRAM
            
            // Pragmas
            #pragma target 2.0
                #pragma exclude_renderers d3d11_9x
                #pragma vertex vert
                #pragma fragment frag
            
            // Keywords
            #pragma multi_compile_fragment _ DEBUG_DISPLAY
            // GraphKeywords: <None>
            
            // Defines
            
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_SPRITEFORWARD
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
            
            
            // custom interpolator pre-include
            /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
            
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            
            // --------------------------------------------------
            // Structs and Packing
            
            // custom interpolators pre packing
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
            
            struct Attributes
                {
                     float3 positionOS : POSITION;
                     float3 normalOS : NORMAL;
                     float4 tangentOS : TANGENT;
                     float4 uv0 : TEXCOORD0;
                     float4 color : COLOR;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : INSTANCEID_SEMANTIC;
                    #endif
                };
                struct Varyings
                {
                     float4 positionCS : SV_POSITION;
                     float3 positionWS;
                     float4 texCoord0;
                     float4 color;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
                struct SurfaceDescriptionInputs
                {
                     float3 TangentSpaceNormal;
                     float3 ObjectSpacePosition;
                     float4 uv0;
                     float3 TimeParameters;
                };
                struct VertexDescriptionInputs
                {
                     float3 ObjectSpaceNormal;
                     float3 ObjectSpaceTangent;
                     float3 ObjectSpacePosition;
                };
                struct PackedVaryings
                {
                     float4 positionCS : SV_POSITION;
                     float3 interp0 : INTERP0;
                     float4 interp1 : INTERP1;
                     float4 interp2 : INTERP2;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
            
            PackedVaryings PackVaryings (Varyings input)
                {
                    PackedVaryings output;
                    ZERO_INITIALIZE(PackedVaryings, output);
                    output.positionCS = input.positionCS;
                    output.interp0.xyz =  input.positionWS;
                    output.interp1.xyzw =  input.texCoord0;
                    output.interp2.xyzw =  input.color;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
                Varyings UnpackVaryings (PackedVaryings input)
                {
                    Varyings output;
                    output.positionCS = input.positionCS;
                    output.positionWS = input.interp0.xyz;
                    output.texCoord0 = input.interp1.xyzw;
                    output.color = input.interp2.xyzw;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }
                
            
            // --------------------------------------------------
            // Graph
            
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
                float _DistortSpeed;
                float4 _DistortNormal_TexelSize;
                float2 _DistortSize;
                float4 _MainTex_TexelSize;
                float2 _DistortPosition;
                float2 _SpriteTilling;
                float2 _SpriteOffset;
                float4 _Emisson;
                float _Opacity;
                CBUFFER_END
                
                // Object and Global properties
                SAMPLER(SamplerState_Linear_Repeat);
                TEXTURE2D(_DistortNormal);
                SAMPLER(sampler_DistortNormal);
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
            
            // Graph Includes
            // GraphIncludes: <None>
            
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
            
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
            
            // Graph Functions
            
                void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Multiply_float_float(float A, float B, out float Out)
                {
                    Out = A * B;
                }
                
                void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
                {
                    Out = UV * Tiling + Offset;
                }
                
                void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A * B;
                }
                
                void Unity_Add_float2(float2 A, float2 B, out float2 Out)
                {
                    Out = A + B;
                }
                
                void Unity_Blend_Overlay_float4(float4 Base, float4 Blend, out float4 Out, float Opacity)
                {
                    float4 result1 = 1.0 - 2.0 * (1.0 - Base) * (1.0 - Blend);
                    float4 result2 = 2.0 * Base * Blend;
                    float4 zeroOrOne = step(Base, 0.5);
                    Out = result2 * zeroOrOne + (1 - zeroOrOne) * result1;
                    Out = lerp(Base, Out, Opacity);
                }
            
            // Custom interpolators pre vertex
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
            
            // Graph Vertex
            struct VertexDescription
                {
                    float3 Position;
                    float3 Normal;
                    float3 Tangent;
                };
                
                VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                {
                    VertexDescription description = (VertexDescription)0;
                    description.Position = IN.ObjectSpacePosition;
                    description.Normal = IN.ObjectSpaceNormal;
                    description.Tangent = IN.ObjectSpaceTangent;
                    return description;
                }
            
            // Custom interpolators, pre surface
            #ifdef FEATURES_GRAPH_VERTEX
            Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
            {
            return output;
            }
            #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
            #endif
            
            // Graph Pixel
            struct SurfaceDescription
                {
                    float3 BaseColor;
                    float Alpha;
                    float3 NormalTS;
                };
                
                SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                {
                    SurfaceDescription surface = (SurfaceDescription)0;
                    UnityTexture2D _Property_0dee077884704c658b38d1e672c5a1c1_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    float4 _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_0dee077884704c658b38d1e672c5a1c1_Out_0.tex, _Property_0dee077884704c658b38d1e672c5a1c1_Out_0.samplerstate, _Property_0dee077884704c658b38d1e672c5a1c1_Out_0.GetTransformedUV(IN.uv0.xy));
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_R_4 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.r;
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_G_5 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.g;
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_B_6 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.b;
                    float _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_A_7 = _SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_RGBA_0.a;
                    float4 _Property_f0a0f083baf240e995ead5f1eaaf0ef1_Out_0 = IsGammaSpace() ? LinearToSRGB(_Emisson) : _Emisson;
                    float4 _Multiply_ffe62072630f424e85a61bef53400569_Out_2;
                    Unity_Multiply_float4_float4((_SampleTexture2D_f8235b44a0d64f568f72dd0c63e2aab7_R_4.xxxx), _Property_f0a0f083baf240e995ead5f1eaaf0ef1_Out_0, _Multiply_ffe62072630f424e85a61bef53400569_Out_2);
                    UnityTexture2D _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
                    UnityTexture2D _Property_d654e6607acb4a4faf2750efb69bc674_Out_0 = UnityBuildTexture2DStructNoScale(_DistortNormal);
                    float2 _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0 = _DistortSize;
                    float _Property_4e977330ff634999a1feeb748ad85871_Out_0 = _DistortSpeed;
                    float _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2;
                    Unity_Multiply_float_float(IN.TimeParameters.x, _Property_4e977330ff634999a1feeb748ad85871_Out_0, _Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2);
                    float2 _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_2785e50d5b65427ab250ec2d21ae3cac_Out_0, (_Multiply_30136ffcac714ff993c96bc2cb99e8d4_Out_2.xx), _TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3);
                    float4 _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d654e6607acb4a4faf2750efb69bc674_Out_0.tex, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.samplerstate, _Property_d654e6607acb4a4faf2750efb69bc674_Out_0.GetTransformedUV(_TilingAndOffset_0a00de8eaf6d4f3b856c9ecb1008e2cf_Out_3));
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_R_4 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.r;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_G_5 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.g;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_B_6 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.b;
                    float _SampleTexture2D_7013d12708944d6898231dd8e23ef920_A_7 = _SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.a;
                    float3 _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2;
                    Unity_Multiply_float3_float3(IN.ObjectSpacePosition, (_SampleTexture2D_7013d12708944d6898231dd8e23ef920_RGBA_0.xyz), _Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2);
                    float2 _Property_35c0b9826e4c40b4a98576290da7f863_Out_0 = _DistortPosition;
                    float2 _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2;
                    Unity_Multiply_float2_float2((_Multiply_a2b270c315b9456ca8e2dcab22efc0f1_Out_2.xy), _Property_35c0b9826e4c40b4a98576290da7f863_Out_0, _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2);
                    float2 _Add_6fbb0b737f214a2dad54497313500ffc_Out_2;
                    Unity_Add_float2((IN.ObjectSpacePosition.xy), _Multiply_e3affccb30a44f199419efbc05db12cd_Out_2, _Add_6fbb0b737f214a2dad54497313500ffc_Out_2);
                    float2 _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0 = _SpriteTilling;
                    float2 _Property_74a313c063ea461daf6b79f48bcbe376_Out_0 = _SpriteOffset;
                    float2 _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3;
                    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_fd84e96f437c498fb37d0c3b2496c560_Out_0, _Property_74a313c063ea461daf6b79f48bcbe376_Out_0, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3);
                    float2 _Add_b99143d9640d4957888c0a14b766b6cc_Out_2;
                    Unity_Add_float2(_Add_6fbb0b737f214a2dad54497313500ffc_Out_2, _TilingAndOffset_5168b3ab44c04b6ab8fa3c49619c1535_Out_3, _Add_b99143d9640d4957888c0a14b766b6cc_Out_2);
                    float4 _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0 = SAMPLE_TEXTURE2D(_Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.tex, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.samplerstate, _Property_22e8e2e4ed314031945e1fab01ffbd6b_Out_0.GetTransformedUV(_Add_b99143d9640d4957888c0a14b766b6cc_Out_2));
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_R_4 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.r;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_G_5 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.g;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_B_6 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.b;
                    float _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7 = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0.a;
                    float _Property_505f1cc01d044127a8c273551910e986_Out_0 = _Opacity;
                    float4 _Blend_3fcc86e24b3a4a77bdac7854f6ce396c_Out_2;
                    Unity_Blend_Overlay_float4(_Multiply_ffe62072630f424e85a61bef53400569_Out_2, _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_RGBA_0, _Blend_3fcc86e24b3a4a77bdac7854f6ce396c_Out_2, _Property_505f1cc01d044127a8c273551910e986_Out_0);
                    surface.BaseColor = (_Blend_3fcc86e24b3a4a77bdac7854f6ce396c_Out_2.xyz);
                    surface.Alpha = _SampleTexture2D_de6c3a9b37d046e19f132f0cef159a0a_A_7;
                    surface.NormalTS = IN.TangentSpaceNormal;
                    return surface;
                }
            
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES Attributes
            #define VFX_SRP_VARYINGS Varyings
            #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
            #endif
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                {
                    VertexDescriptionInputs output;
                    ZERO_INITIALIZE(VertexDescriptionInputs, output);
                
                    output.ObjectSpaceNormal =                          input.normalOS;
                    output.ObjectSpaceTangent =                         input.tangentOS.xyz;
                    output.ObjectSpacePosition =                        input.positionOS;
                
                    return output;
                }
                
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
                
                #ifdef HAVE_VFX_MODIFICATION
                    // FragInputs from VFX come from two places: Interpolator or CBuffer.
                    /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
                
                #endif
                
                    
                
                
                
                    output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);
                
                
                    output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                
                    #if UNITY_UV_STARTS_AT_TOP
                    #else
                    #endif
                
                
                    output.uv0 = input.texCoord0;
                    output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                #else
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                #endif
                #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                
                        return output;
                }
                
            
            // --------------------------------------------------
            // Main
            
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteForwardPass.hlsl"
            
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
            #ifdef HAVE_VFX_MODIFICATION
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
            #endif
            
            ENDHLSL
            }
        }
        CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
        FallBack "Hidden/Shader Graph/FallbackError"
    }