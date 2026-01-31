namespace StaticWebAssetToolingLibrary;
internal static class SourceBuilderExtensions
{
    extension (SourceCodeStringBuilder builder)
    {
        public void WriteSimpleTypeClass(Action<ICodeBlock> action, FileClass file)
        {
            builder.WriteLine("#nullable enable")
                    .WriteLine(w =>
                    {
                        w.Write("namespace ")
                        .Write($"{GlobalConstants.ProjectName}.Assets") //suggested assets.
                        .Write(";");
                    })
                    .WriteLine(w =>
                    {
                        w.Write($"internal class {file.ClassName.CapitalizeFirstLetter}");
                    })
                    .WriteCodeBlock(action.Invoke);
        }
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