## Defold parser

This parser will read and write some of the Defold file formats, allowing you to write specialized tools to streamline your workflow.

#Usage:

```
var tilemap = DefoldHelper.Read<TileMap>(<path to tilemap>);
tilemap.Layers.RemoveAt(0);
DefoldHelper.Save(tilemap.Serialize(), "<new path>");
```

