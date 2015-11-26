using UnityEngine;
using UnityEditor;
using UnityEditor.iOS;
using System.Collections;
using System.IO;

#if ENABLE_IOS_ON_DEMAND_RESOURCES || ENABLE_IOS_APP_SLICING
public class BuildResources
{
    [InitializeOnLoadMethod]
    static void SetupResourcesBuild()
    {
        UnityEditor.iOS.BuildPipeline.collectResources += CollectResources;
    }

    static string GetPath(string relativePath)
    {
        string root = Path.Combine(AssetBundles.Utility.AssetBundlesOutputPath, 
                                   AssetBundles.Utility.GetPlatformName());
        return Path.Combine(root, relativePath);
    }
 
    static UnityEditor.iOS.Resource[] CollectResources()
    {
        string manifest = AssetBundles.Utility.GetPlatformName();
        return new Resource[]
        {
            new Resource(manifest, GetPath(manifest)).AddOnDemandResourceTags(manifest),
            new Resource("scene-bundle", GetPath("scene-bundle")).AddOnDemandResourceTags("scene-bundle"),
            new Resource("cube-bundle", GetPath("cube-bundle")).AddOnDemandResourceTags("cube-bundle"),
            new Resource("material-bundle", GetPath("material-bundle")).AddOnDemandResourceTags("material-bundle"),
            new Resource("variants/variant-scene", GetPath("variants/variant-scene")).AddOnDemandResourceTags("variants/variant-scene"),
            new Resource("variants/myassets").BindVariant(GetPath("variants/myassets.hd"), "hd")
                                             .BindVariant(GetPath("variants/myassets.sd"), "sd")
        };
    }
}
#endif
