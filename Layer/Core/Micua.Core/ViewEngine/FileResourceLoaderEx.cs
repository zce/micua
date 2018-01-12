// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileResourceLoaderEx.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   The file resource loader ex.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.ViewEngine
{
    using System;
    using System.IO;

    using NVelocity.Runtime.Resource.Loader;

    /// <summary>
    /// The file resource loader ex.
    /// </summary>
    public class FileResourceLoaderEx : FileResourceLoader
    {
        private Stream FindTemplate(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                return new BufferedStream(file.OpenRead());
            }
            catch (Exception exception)
            {
                this.runtimeServices.Debug(string.Format("FileResourceLoader : {0}", exception.Message));
                return null;
            }
        }


        public override long GetLastModified(NVelocity.Runtime.Resource.Resource resource)
        {
            if (File.Exists(resource.Name))
            {
                var file = new FileInfo(resource.Name);
                return file.LastWriteTime.Ticks;
            }

            return base.GetLastModified(resource);
        }

        public override Stream GetResourceStream(string templateName)
        {
            if (templateName.Equals("VM_global_library.vm"))
            {
                return null;
            }
            if (File.Exists(templateName))
            {
                return this.FindTemplate(templateName);
            }
            return base.GetResourceStream(templateName);
        }
        public override bool IsSourceModified(NVelocity.Runtime.Resource.Resource resource)
        {
            if (File.Exists(resource.Name))
            {
                var file = new FileInfo(resource.Name);
                return (!file.Exists || (file.LastWriteTime.Ticks != resource.LastModified));
            }

            return base.IsSourceModified(resource);
        }
    }
}