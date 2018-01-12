namespace Micua.Core.Plugin
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Web.Compilation;
    
    internal sealed class BuildManagerWrapper : IBuildManager
    {
        Stream IBuildManager.CreateCachedFile(string fileName)
        {
            return BuildManager.CreateCachedFile(fileName);
        }
        
        bool IBuildManager.FileExists(string virtualPath)
        {
            return (BuildManager.GetObjectFactory(virtualPath, false) != null);
        }
        
        Type IBuildManager.GetCompiledType(string virtualPath)
        {
            return BuildManager.GetCompiledType(virtualPath);
        }
        
        ICollection IBuildManager.GetReferencedAssemblies()
        {
            return BuildManager.GetReferencedAssemblies();
        }
        
        Stream IBuildManager.ReadCachedFile(string fileName)
        {
            return BuildManager.ReadCachedFile(fileName);
        }
    }
}
