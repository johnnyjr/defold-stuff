varying mediump vec4 position;
varying mediump vec2 var_texcoord0;

uniform mediump sampler2D DIFFUSE_TEXTURE;
uniform lowp vec4 tint;
uniform lowp vec4 offset;

void main()
{
    // Pre-multiply alpha since all runtime textures already are
    lowp vec4 tint_pm = vec4(tint.xyz * tint.w, tint.w);
    gl_FragColor = texture2D(DIFFUSE_TEXTURE, var_texcoord0.xy+vec2(offset.xy)) * tint_pm;
}
