namespace Micua.Core.Plugin
{
    using System;
    using System.Collections;
    using System.IO;

    internal interface IBuildManager
    {
        Stream CreateCachedFile(string fileName);
        bool FileExists(string virtualPath);
        Type GetCompiledType(string virtualPath);
        ICollection GetReferencedAssemblies();
        Stream ReadCachedFile(string fileName);
    }
}
