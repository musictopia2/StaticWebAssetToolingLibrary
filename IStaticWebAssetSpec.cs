namespace StaticWebAssetToolingLibrary;
public interface IStaticWebAssetSpec
{
    string GetGlobalName { get; }


    // Allowed extensions (e.g. [".png"])
    BasicList<string> ExtensionsAllowed { get; }
}