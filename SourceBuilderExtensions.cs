namespace StaticWebAssetToolingLibrary;
internal static class SourceBuilderExtensions
{
    extension (SourceCodeStringBuilder builder)
    {
        
        public void WriteAssetClass(Action<ICodeBlock> action)
        {
            builder.WriteLine("#nullable enable")
                    .WriteLine(w =>
                    {
                        w.Write("namespace ")
                         .Write($"{GlobalConstants.ProjectName}.Assets")
                         .Write(";");
                    })
                    .WriteLine(w =>
                    {
                        w.Write($"public static class {GlobalConstants.GlobalName}");
                    })
                    .WriteCodeBlock(action.Invoke);
        }
    }
    
}