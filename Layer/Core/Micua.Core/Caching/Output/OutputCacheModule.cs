//namespace System.Web.Caching
//{
//    using System.Text;
//    using System.IO;
//    using System.Threading;
//    using System.Collections;
//    using System.Globalization;
//    using System.Security.Cryptography;
//    using System.Web;
//    using System.Web.Caching;
//    using System.Web.Util;
//    using System.Collections.Specialized;
//    using System.Web.Configuration;
//    using System.Web.Management;
//    using System.Web.Hosting;
//    using System.Diagnostics;

//    /*
//     * Holds header and param names that this cached item varies by.
//     */
//    internal class CachedVary
//    {
//        internal readonly string[] _contentEncodings;
//        internal readonly string[] _headers;
//        internal readonly string[] _params;
//        internal readonly string _varyByCustom;
//        internal readonly bool _varyByAllParams;

//        internal CachedVary(string[] contentEncodings, string[] headers, string[] parameters, bool varyByAllParams, string varyByCustom)
//        {
//            _contentEncodings = contentEncodings;
//            _headers = headers;
//            _params = parameters;
//            _varyByAllParams = varyByAllParams;
//            _varyByCustom = varyByCustom;
//        }

//        public override bool Equals(Object obj)
//        {
//            if (!(obj is CachedVary))
//            {
//                return false;
//            }

//            CachedVary cv = (CachedVary)obj;

//            return _varyByAllParams == cv._varyByAllParams
//                && _varyByCustom == cv._varyByCustom
//                && StringUtil.StringArrayEquals(_contentEncodings, cv._contentEncodings)
//                && StringUtil.StringArrayEquals(_headers, cv._headers)
//                && StringUtil.StringArrayEquals(_params, cv._params);
//        }

//        public override int GetHashCode()
//        {
//            HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
//            hashCodeCombiner.AddObject(_varyByAllParams);

//            // Cast _varyByCustom to an object, since the HashCodeCombiner.AddObject(string)
//            // overload uses StringUtil.GetStringHashCode().  We want to use String.GetHashCode()
//            // in this method, since we do not require a stable hash code across architectures.
//            hashCodeCombiner.AddObject((object)_varyByCustom);

//            hashCodeCombiner.AddArray(_contentEncodings);
//            hashCodeCombiner.AddArray(_headers);
//            hashCodeCombiner.AddArray(_params);
//            return hashCodeCombiner.CombinedHash32;
//        }
//    }

//    /*
//     * Holds the cached response.
//     */
//    internal class CachedRawResponse
//    {
//        /*
//         * Fields to store an actual response.
//         */
//        internal readonly HttpRawResponse _rawResponse;
//        internal readonly HttpCachePolicySettings _settings;
//        internal readonly String _kernelCacheUrl;

//        internal CachedRawResponse(
//                  HttpRawResponse rawResponse,
//                  HttpCachePolicySettings settings,
//                  String kernelCacheUrl)
//        {
//            _rawResponse = rawResponse;
//            _settings = settings;
//            _kernelCacheUrl = kernelCacheUrl;
//        }
//    }

//    class OutputCacheItemRemoved
//    {
//        internal OutputCacheItemRemoved() { }

//        internal void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
//        {
//            Debug.Trace("OutputCacheItemRemoved", "reason=" + reason + "; key=" + key);
//            Interlocked.Decrement(ref OutputCacheModule.s_cEntries);
//            PerfCounters.DecrementCounter(AppPerfCounter.OUTPUT_CACHE_ENTRIES);
//            PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_TURNOVER_RATE);

//            CachedRawResponse cachedRawResponse = value as CachedRawResponse;
//            if (cachedRawResponse != null)
//            {
//                String kernelCacheUrl = cachedRawResponse._kernelCacheUrl;
//                // if it is kernel cached, the url will be non-null.
//                // if the entry was re-inserted, don't remove kernel entry since it will be updated
//                if (kernelCacheUrl != null && HttpRuntime.CacheInternal[key] == null)
//                {
//                    // invalidate kernel cache entry
//                    if (HttpRuntime.UseIntegratedPipeline)
//                    {
//                        UnsafeIISMethods.MgdFlushKernelCache(kernelCacheUrl);
//                    }
//                    else
//                    {
//                        UnsafeNativeMethods.InvalidateKernelCache(kernelCacheUrl);
//                    }
//                }
//            }
//        }
//    }

//    //
//    //  OutputCacheModule real implementation for premium SKUs
//    //

//    sealed class OutputCacheModule : IHttpModule
//    {
//        const int MAX_POST_KEY_LENGTH = 15000;
//        const string NULL_VARYBY_VALUE = "+n+";
//        const string ERROR_VARYBY_VALUE = "+e+";
//        internal const string TAG_OUTPUTCACHE = "OutputCache";
//        const string OUTPUTCACHE_KEYPREFIX_POST = CacheInternal.PrefixOutputCache + "1";
//        const string OUTPUTCACHE_KEYPREFIX_GET = CacheInternal.PrefixOutputCache + "2";
//        const string IDENTITY = "identity";
//        const string ASTERISK = "*";

//        static internal readonly char[] s_fieldSeparators;
//        static CacheItemRemovedCallback s_cacheItemRemovedCallback;

//        static internal int s_cEntries;

//        string _key;
//        bool _recordedCacheMiss;

//        static OutputCacheModule()
//        {
//            s_fieldSeparators = new char[] { ',', ' ' };
//            s_cacheItemRemovedCallback = new CacheItemRemovedCallback((new OutputCacheItemRemoved()).CacheItemRemovedCallback);
//        }

//        internal OutputCacheModule()
//        {
//        }

//        internal static string CreateOutputCachedItemKey(
//                string path,
//                HttpVerb verb,
//                HttpContext context,
//                CachedVary cachedVary)
//        {

//            StringBuilder sb;
//            int i, j, n;
//            string name, value;
//            string[] a;
//            byte[] buf, hash;
//            HttpRequest request;
//            NameValueCollection col;
//            int contentLength;
//            bool getAllParams;

//            if (verb == HttpVerb.POST)
//            {
//                sb = new StringBuilder(OUTPUTCACHE_KEYPREFIX_POST, path.Length + OUTPUTCACHE_KEYPREFIX_POST.Length);
//            }
//            else
//            {
//                sb = new StringBuilder(OUTPUTCACHE_KEYPREFIX_GET, path.Length + OUTPUTCACHE_KEYPREFIX_GET.Length);
//            }

//            sb.Append(CultureInfo.InvariantCulture.TextInfo.ToLower(path));

//            /* key for cached vary item has additional information */
//            if (cachedVary != null)
//            {
//                request = context.Request;

//                /* params part */
//                for (j = 0; j <= 2; j++)
//                {
//                    a = null;
//                    col = null;
//                    getAllParams = false;

//                    switch (j)
//                    {
//                        case 0:
//                            sb.Append("H");
//                            a = cachedVary._headers;
//                            if (a != null)
//                            {
//                                col = request.ServerVariables;
//                            }

//                            break;

//                        case 1:
//                            Debug.Assert(cachedVary._params == null || !cachedVary._varyByAllParams, "cachedVary._params == null || !cachedVary._varyByAllParams");

//                            sb.Append("Q");
//                            a = cachedVary._params;
//                            if (request.HasQueryString && (a != null || cachedVary._varyByAllParams))
//                            {
//                                col = request.QueryString;
//                                getAllParams = cachedVary._varyByAllParams;
//                            }

//                            break;

//                        case 2:
//                        default:
//                            Debug.Assert(cachedVary._params == null || !cachedVary._varyByAllParams, "cachedVary._params == null || !cachedVary._varyByAllParams");

//                            sb.Append("F");
//                            if (verb == HttpVerb.POST)
//                            {
//                                a = cachedVary._params;
//                                if (request.HasForm && (a != null || cachedVary._varyByAllParams))
//                                {
//                                    col = request.Form;
//                                    getAllParams = cachedVary._varyByAllParams;
//                                }
//                            }

//                            break;
//                    }

//                    Debug.Assert(a == null || !getAllParams, "a == null || !getAllParams");

//                    /* handle all params case (VaryByParams[*] = true) */
//                    if (getAllParams && col.Count > 0)
//                    {
//                        a = col.AllKeys;
//                        for (i = a.Length - 1; i >= 0; i--)
//                        {
//                            if (a[i] != null)
//                                a[i] = CultureInfo.InvariantCulture.TextInfo.ToLower(a[i]);
//                        }

//                        Array.Sort(a, InvariantComparer.Default);
//                    }

//                    if (a != null)
//                    {
//                        for (i = 0, n = a.Length; i < n; i++)
//                        {
//                            name = a[i];
//                            if (col == null)
//                            {
//                                value = NULL_VARYBY_VALUE;
//                            }
//                            else
//                            {
//                                value = col[name];
//                                if (value == null)
//                                {
//                                    value = NULL_VARYBY_VALUE;
//                                }
//                            }

//                            sb.Append("N");
//                            sb.Append(name);
//                            sb.Append("V");
//                            sb.Append(value);
//                        }
//                    }
//                }

//                /* custom string part */
//                sb.Append("C");
//                if (cachedVary._varyByCustom != null)
//                {
//                    sb.Append("N");
//                    sb.Append(cachedVary._varyByCustom);
//                    sb.Append("V");

//                    try
//                    {
//                        value = context.ApplicationInstance.GetVaryByCustomString(
//                                context, cachedVary._varyByCustom);
//                        if (value == null)
//                        {
//                            value = NULL_VARYBY_VALUE;
//                        }
//                    }
//                    catch (Exception e)
//                    {
//                        value = ERROR_VARYBY_VALUE;
//                        HttpApplicationFactory.RaiseError(e);
//                    }

//                    sb.Append(value);
//                }

//                /*
//                 * if VaryByParms=*, and method is not a form, then
//                 * use a cryptographically strong hash of the data as
//                 * part of the key.
//                 */
//                sb.Append("D");
//                if (verb == HttpVerb.POST &&
//                        cachedVary._varyByAllParams &&
//                        request.Form.Count == 0)
//                {

//                    contentLength = request.ContentLength;
//                    if (contentLength > MAX_POST_KEY_LENGTH || contentLength < 0)
//                    {
//                        return null;
//                    }

//                    if (contentLength > 0)
//                    {
//                        buf = ((HttpInputStream)request.InputStream).GetAsByteArray();
//                        if (buf == null)
//                        {
//                            return null;
//                        }

//                        // Hash with machine key
//                        hash = MachineKeySection.HashData(buf, null, 0, buf.Length);
//                        value = Convert.ToBase64String(hash);
//                        sb.Append(value);
//                    }
//                }

//                /*
//                 * VaryByContentEncoding
//                 */
//                sb.Append("E");
//                string[] contentEncodings = cachedVary._contentEncodings;
//                if (contentEncodings != null)
//                {
//                    string coding = context.Response.GetHttpHeaderContentEncoding();
//                    if (coding != null)
//                    {
//                        for (int k = 0; k < contentEncodings.Length; k++)
//                        {
//                            if (contentEncodings[k] == coding)
//                            {
//                                sb.Append(coding);
//                                break;
//                            }
//                        }
//                    }
//                }

//                // The key must end in "E", or the VaryByContentEncoding feature will break. Unfortunately,
//                // there was no good way to encapsulate the logic within this routine.  See the code in
//                // OnEnter where we append the result of GetAcceptableEncoding to the key.
//            }

//            return sb.ToString();
//        }

//        /*
//         * Return a key to lookup a cached response. The key contains
//         * the path and optionally, vary parameters, vary headers, custom strings,
//         * and form posted data.
//         */
//        string CreateOutputCachedItemKey(HttpContext context, CachedVary cachedVary)
//        {
//            return CreateOutputCachedItemKey(context.Request.Path, context.Request.HttpVerb, context, cachedVary);
//        }

//        /*
//         * GetAcceptableEncoding finds an acceptable coding for the given
//         * Accept-Encoding header (see RFC 2616)
//         * returns either i) an acceptable index in contentEncodings, ii) -1 if the identity is acceptable, or iii) -2 if nothing is acceptable
//         */
//        static int GetAcceptableEncoding(string[] contentEncodings, int startIndex, string acceptEncoding)
//        {
//            // The format of Accept-Encoding is ( 1#( codings [ ";" "q" "=" qvalue ] ) | "*" )
//            if (String.IsNullOrEmpty(acceptEncoding))
//            {
//                return -1; // use "identity"
//            }

//            // is there only one token?
//            int tokenEnd = acceptEncoding.IndexOf(',');
//            if (tokenEnd == -1)
//            {
//                string acceptEncodingWithoutWeight = acceptEncoding;
//                // WOS 1984913: is there a weight?
//                tokenEnd = acceptEncoding.IndexOf(';');
//                if (tokenEnd > -1)
//                {
//                    // remove weight
//                    int space = acceptEncoding.IndexOf(' ');
//                    if (space > -1 && space < tokenEnd)
//                    {
//                        tokenEnd = space;
//                    }
//                    acceptEncodingWithoutWeight = acceptEncoding.Substring(0, tokenEnd);
//                    if (ParseWeight(acceptEncoding, tokenEnd) == 0)
//                    {
//                        // WOS 1985352 & WOS 1985353: weight is 0, use "identity" only if it is acceptable
//                        bool identityIsAcceptable = acceptEncodingWithoutWeight != IDENTITY && acceptEncodingWithoutWeight != ASTERISK;
//                        return (identityIsAcceptable) ? -1 : -2;
//                    }
//                }
//                // WOS 1985353: is this the special "*" symbol?
//                if (acceptEncodingWithoutWeight == ASTERISK)
//                {
//                    // just return the index of the first entry in the list, since it is acceptable
//                    return 0;
//                }
//                for (int i = startIndex; i < contentEncodings.Length; i++)
//                {
//                    if (contentEncodings[i] == acceptEncodingWithoutWeight)
//                    {
//                        return i; // found
//                    }
//                }
//                return -1; // not found, use "identity"
//            }

//            // there are multiple tokens
//            int bestCodingIndex = -1;
//            double bestCodingWeight = 0;
//            for (int i = startIndex; i < contentEncodings.Length; i++)
//            {
//                string coding = contentEncodings[i];
//                // get weight of current coding
//                double weight = GetAcceptableEncodingHelper(coding, acceptEncoding);
//                // if it is 1, use it
//                if (weight == 1)
//                {
//                    return i;
//                }
//                // if it is the best so far, remember it
//                if (weight > bestCodingWeight)
//                {
//                    bestCodingIndex = i;
//                    bestCodingWeight = weight;
//                }
//            }
//            // WOS 1985352: use "identity" only if it is acceptable
//            if (bestCodingIndex == -1 && !IsIdentityAcceptable(acceptEncoding))
//            {
//                bestCodingIndex = -2;
//            }
//            return bestCodingIndex; // coding index with highest weight, possibly -1 or -2
//        }

//        // Get the weight of the specified coding from the Accept-Encoding header.
//        // 1 means use this coding.  0 means don't use this coding.  A number between
//        // 1 and 0 must be compared with other codings.  -1 means the coding was not found
//        static double GetAcceptableEncodingHelper(string coding, string acceptEncoding)
//        {
//            double weight = -1;
//            int startSearchIndex = 0;
//            int codingLength = coding.Length;
//            int acceptEncodingLength = acceptEncoding.Length;
//            int maxSearchIndex = acceptEncodingLength - codingLength;
//            while (startSearchIndex < maxSearchIndex)
//            {
//                int indexStart = acceptEncoding.IndexOf(coding, startSearchIndex, StringComparison.Ordinal);

//                if (indexStart == -1)
//                {
//                    break; // not found
//                }

//                // if index is in middle of string, previous char should be ' ' or ','
//                if (indexStart != 0)
//                {
//                    char previousChar = acceptEncoding[indexStart - 1];
//                    if (previousChar != ' ' && previousChar != ',')
//                    {
//                        startSearchIndex = indexStart + 1;
//                        continue; // move index forward and continue searching
//                    }
//                }

//                // the match starts on a token boundary, but it must also end
//                // on a token boundary ...

//                int indexNextChar = indexStart + codingLength;
//                char nextChar = '\0';
//                if (indexNextChar < acceptEncodingLength)
//                {
//                    nextChar = acceptEncoding[indexNextChar];
//                    while (nextChar == ' ' && ++indexNextChar < acceptEncodingLength)
//                    {
//                        nextChar = acceptEncoding[indexNextChar];
//                    }
//                    if (nextChar != ' ' && nextChar != ',' && nextChar != ';')
//                    {
//                        startSearchIndex = indexStart + 1;
//                        continue; // move index forward and continue searching
//                    }
//                }
//                weight = (nextChar == ';') ? ParseWeight(acceptEncoding, indexNextChar) : 1;
//                break; // found
//            }
//            return weight;
//        }

//        // Gets the weight of the encoding beginning at startIndex.
//        // If Accept-Encoding header is formatted incorrectly, return 1 to short-circuit search.
//        static double ParseWeight(string acceptEncoding, int startIndex)
//        {
//            double weight = 1;
//            int tokenEnd = acceptEncoding.IndexOf(',', startIndex);
//            if (tokenEnd == -1)
//            {
//                tokenEnd = acceptEncoding.Length;
//            }
//            int qIndex = acceptEncoding.IndexOf('q', startIndex);
//            if (qIndex > -1 && qIndex < tokenEnd)
//            {
//                int equalsIndex = acceptEncoding.IndexOf('=', qIndex);
//                if (equalsIndex > -1 && equalsIndex < tokenEnd)
//                {
//                    string s = acceptEncoding.Substring(equalsIndex + 1, tokenEnd - (equalsIndex + 1));
//                    double d;
//                    if (Double.TryParse(s, NumberStyles.Float & ~NumberStyles.AllowLeadingSign & ~NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out d))
//                    {
//                        weight = (d >= 0 && d <= 1) ? d : 1; // if format is invalid, short-circut search by returning weight of 1
//                    }
//                }
//            }
//            return weight;
//        }

//        static bool IsIdentityAcceptable(string acceptEncoding)
//        {
//            bool result = true;
//            double identityWeight = GetAcceptableEncodingHelper(IDENTITY, acceptEncoding);
//            if (identityWeight == 0
//                || (identityWeight <= 0 && GetAcceptableEncodingHelper(ASTERISK, acceptEncoding) == 0))
//            {
//                result = false;
//            }
//            return result;
//        }

//        static bool IsAcceptableEncoding(string contentEncoding, string acceptEncoding)
//        {
//            if (String.IsNullOrEmpty(contentEncoding))
//            {
//                // if Content-Encoding is not set treat it as the identity
//                contentEncoding = IDENTITY;
//            }
//            if (String.IsNullOrEmpty(acceptEncoding))
//            {
//                // only the identity is acceptable if Accept-Encoding is not set
//                return (contentEncoding == IDENTITY);
//            }
//            double weight = GetAcceptableEncodingHelper(contentEncoding, acceptEncoding);
//            if (weight == 0
//                || (weight <= 0 && GetAcceptableEncodingHelper(ASTERISK, acceptEncoding) == 0))
//            {
//                return false;
//            }
//            return true;
//        }

//        /*
//         * Record a cache miss to the perf counters.
//         */
//        void RecordCacheMiss()
//        {
//            if (!_recordedCacheMiss)
//            {
//                PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_RATIO_BASE);
//                PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_MISSES);
//                _recordedCacheMiss = true;
//            }
//        }


//        /// <devdoc>
//        ///    <para>Initializes the output cache for an application.</para>
//        /// </devdoc>
//        void IHttpModule.Init(HttpApplication app)
//        {
//            OutputCacheSection cacheConfig = RuntimeConfig.GetAppConfig().OutputCache;
//            if (cacheConfig.EnableOutputCache)
//            {
//                app.ResolveRequestCache += new EventHandler(this.OnEnter);
//                app.UpdateRequestCache += new EventHandler(this.OnLeave);
//            }
//        }


//        /// <devdoc>
//        ///    <para>Disposes of items from the output cache.</para>
//        /// </devdoc>
//        void IHttpModule.Dispose()
//        {
//        }

//        /*
//         * Try to find this request in the cache. If so, return it. Otherwise,
//         * store the cache key for use on Leave.
//         */

//        /// <devdoc>
//        /// <para>Raises the <see langword="Enter">
//        /// event, which searches the output cache for an item to satisfy the HTTP request. </see></para>
//        /// </devdoc>
//        internal /*public*/ void OnEnter(Object source, EventArgs eventArgs)
//        {
//            HttpApplication app;
//            HttpContext context;
//            string key;
//            HttpRequest request;
//            HttpResponse response;
//            Object item;
//            CachedRawResponse cachedRawResponse;
//            HttpCachePolicySettings settings;
//            int i, n;
//            bool sendBody;
//            HttpValidationStatus validationStatus, validationStatusFinal;
//            ValidationCallbackInfo callbackInfo;
//            string ifModifiedSinceHeader;
//            DateTime utcIfModifiedSince;
//            string etag;
//            string[] etags;
//            int send304;
//            string cacheControl;
//            string[] cacheDirectives = null;
//            string pragma;
//            string[] pragmaDirectives = null;
//            string directive;
//            int maxage;
//            int minfresh;
//            int age;
//            int fresh;
//            bool hasValidationPolicy;
//            CachedVary cachedVary;
//            CacheInternal cacheInternal;
//            HttpRawResponse rawResponse;
//            CachedPathData cachedPathData;

//            Debug.Trace("OutputCacheModuleEnter", "Beginning OutputCacheModule::Enter");

//            _key = null;
//            _recordedCacheMiss = false;

//            app = (HttpApplication)source;
//            context = app.Context;
//            cachedPathData = context.GetFilePathData();

//            // If we have no entries in memory, then skip further checks.
//            if (s_cEntries <= 0)
//            {
//                Debug.Trace("OutputCacheModuleEnter", "Output cache miss, no entries in output Cache" +
//                            "\nReturning from OutputCacheModule::Enter");
//                return;
//            }

//            request = context.Request;
//            response = context.Response;

//            /*
//             * Check if the request can be resolved for this method.
//             */
//            switch (request.HttpVerb)
//            {
//                case HttpVerb.HEAD:
//                case HttpVerb.GET:
//                case HttpVerb.POST:
//                    break;

//                default:
//                    Debug.Trace("OutputCacheModuleEnter", "Output cache miss, Http method not GET, POST, or HEAD" +
//                                "\nReturning from OutputCacheModule::Enter");

//                    return;
//            }

//            /*
//             * Create a lookup key. Remember the key for use inside Leave()
//             */
//            _key = key = CreateOutputCachedItemKey(context, null);
//            Debug.Assert(_key != null, "_key != null");

//            /*
//             *  Lookup the cache vary for this key.
//             */
//            cacheInternal = HttpRuntime.CacheInternal;
//            item = cacheInternal.Get(key);
//            if (item == null)
//            {
//                Debug.Trace("OutputCacheModuleEnter", "Output cache miss, item not found.\n\tkey=" + key +
//                            "\nReturning from OutputCacheModule::Enter");
//                return;
//            }

//            // 'item' may be one of the following:
//            //  - a CachedVary object (if the object varies by something)
//            //  - a "no vary" CachedRawResponse object (i.e. it doesn't vary on anything)

//            // Let's assume it's a CacheVary and see what happens.
//            cachedVary = item as CachedVary;

//            // If we have one, create a new cache key for it (this is a must)
//            if (cachedVary != null)
//            {
//                /*
//                 * This cached output has a Vary policy. Create a new key based
//                 * on the vary headers in cachedRawResponse and try again.
//                 *
//                 * Skip this step if it's a VaryByNone vary policy.
//                 */


//                key = CreateOutputCachedItemKey(context, cachedVary);
//                if (key == null)
//                {
//                    Debug.Trace("OutputCacheModuleEnter", "Output cache miss, key could not be created for vary-by item." +
//                                "\nReturning from OutputCacheModule::Enter");

//                    return;
//                }

//                if (cachedVary._contentEncodings == null)
//                {
//                    // With the new key, look up the in-memory key.
//                    // At this point, we've exhausted the lookups in memory for this item.
//                    item = cacheInternal.Get(key);
//                }
//                else
//                {
//#if DBG
//                    Debug.Assert(key[key.Length-1] == 'E', "key[key.Length-1] == 'E'");
//#endif
//                    item = null;
//                    bool identityIsAcceptable = true;
//                    string acceptEncoding = context.WorkerRequest.GetKnownRequestHeader(HttpWorkerRequest.HeaderAcceptEncoding);
//                    if (acceptEncoding != null)
//                    {
//                        string[] contentEncodings = cachedVary._contentEncodings;
//                        int startIndex = 0;
//                        bool done = false;
//                        while (!done)
//                        {
//                            done = true;
//                            int index = GetAcceptableEncoding(contentEncodings, startIndex, acceptEncoding);
//                            if (index > -1)
//                            {
//#if DBG
//                                Debug.Trace("OutputCacheModuleEnter", "VaryByContentEncoding key=" + key + contentEncodings[index]);
//#endif
//                                identityIsAcceptable = false; // the client Accept-Encoding header contains an encoding that's in the VaryByContentEncoding list
//                                item = cacheInternal.Get(key + contentEncodings[index]);
//                                if (item == null)
//                                {
//                                    startIndex = index + 1;
//                                    if (startIndex < contentEncodings.Length)
//                                    {
//                                        done = false;
//                                    }
//                                }
//                            }
//                            else if (index == -2)
//                            {
//                                // the identity has a weight of 0 and is not acceptable
//                                identityIsAcceptable = false;
//                            }
//                        }
//                    }

//                    // the identity should not be used if the client Accept-Encoding contains an entry in the VaryByContentEncoding list or "identity" is not acceptable
//                    if (item == null && identityIsAcceptable)
//                    {
//#if DBG
//                        Debug.Trace("OutputCacheModuleEnter", "VaryByContentEncoding key=" + key);
//#endif
//                        item = cacheInternal.Get(key);
//                    }
//                }

//                if (item == null)
//                {
//                    Debug.Trace("OutputCacheModuleEnter", "Output cache miss, item not found.\n\tkey=" + key +
//                                "\nReturning from OutputCacheModule::Enter");
//                    return;
//                }

//            }

//            // From this point on, we have an entry to work with.

//            Debug.Assert(item is CachedRawResponse, "item is CachedRawResponse");
//            cachedRawResponse = (CachedRawResponse)item;
//            settings = cachedRawResponse._settings;
//            if (cachedVary == null && !settings.IgnoreParams)
//            {
//                /*
//                 * This cached output has no vary policy, so make sure it doesn't have a query string or form post.
//                 */
//                if (request.HttpVerb == HttpVerb.POST)
//                {
//                    Debug.Trace("OutputCacheModuleEnter", "Output cache item found but method is POST and no VaryByParam specified." +
//                                "\n\tkey=" + key +
//                                "\nReturning from OutputCacheModule::Enter");
//                    RecordCacheMiss();
//                    return;
//                }

//                if (request.HasQueryString)
//                {
//                    Debug.Trace("OutputCacheModuleEnter", "Output cache item found but contains a querystring and no VaryByParam specified." +
//                                "\n\tkey=" + key +
//                                "\nReturning from OutputCacheModule::Enter");
//                    RecordCacheMiss();
//                    return;
//                }
//            }

//            hasValidationPolicy = settings.HasValidationPolicy();

//            /*
//             * Determine whether the client can accept a cached copy, and
//             * get values of other cache control directives.
//             *
//             * We do this after lookup so we don't have to break down the headers
//             * if the item is not found. Cracking the headers is expensive.
//             */
//            if (!hasValidationPolicy)
//            {
//                cacheControl = request.Headers["Cache-Control"];
//                if (cacheControl != null)
//                {
//                    cacheDirectives = cacheControl.Split(s_fieldSeparators);
//                    for (i = 0; i < cacheDirectives.Length; i++)
//                    {
//                        directive = cacheDirectives[i];
//                        if (directive == "no-cache" || directive == "no-store")
//                        {
//                            Debug.Trace("OutputCacheModuleEnter",
//                                        "Skipping lookup because of Cache-Control: no-cache or no-store directive." +
//                                        "\nReturning from OutputCacheModule::Enter");

//                            RecordCacheMiss();
//                            return;
//                        }

//                        if (StringUtil.StringStartsWith(directive, "max-age="))
//                        {
//                            try
//                            {
//                                maxage = Convert.ToInt32(directive.Substring(8), CultureInfo.InvariantCulture);
//                            }
//                            catch
//                            {
//                                maxage = -1;
//                            }

//                            if (maxage >= 0)
//                            {
//                                age = (int)((context.UtcTimestamp.Ticks - settings.UtcTimestampCreated.Ticks) / TimeSpan.TicksPerSecond);
//                                if (age >= maxage)
//                                {
//                                    Debug.Trace("OutputCacheModuleEnter",
//                                                "Not returning found item due to Cache-Control: max-age directive." +
//                                                "\nReturning from OutputCacheModule::Enter");

//                                    RecordCacheMiss();
//                                    return;
//                                }
//                            }
//                        }
//                        else if (StringUtil.StringStartsWith(directive, "min-fresh="))
//                        {
//                            try
//                            {
//                                minfresh = Convert.ToInt32(directive.Substring(10), CultureInfo.InvariantCulture);
//                            }
//                            catch
//                            {
//                                minfresh = -1;
//                            }

//                            if (minfresh >= 0 && settings.IsExpiresSet && !settings.SlidingExpiration)
//                            {
//                                fresh = (int)((settings.UtcExpires.Ticks - context.UtcTimestamp.Ticks) / TimeSpan.TicksPerSecond);
//                                if (fresh < minfresh)
//                                {
//                                    Debug.Trace("OutputCacheModuleEnter",
//                                                "Not returning found item due to Cache-Control: min-fresh directive." +
//                                                "\nReturning from OutputCacheModule::Enter");

//                                    RecordCacheMiss();
//                                    return;
//                                }
//                            }
//                        }
//                    }
//                }

//                pragma = request.Headers["Pragma"];
//                if (pragma != null)
//                {
//                    pragmaDirectives = pragma.Split(s_fieldSeparators);
//                    for (i = 0; i < pragmaDirectives.Length; i++)
//                    {
//                        if (pragmaDirectives[i] == "no-cache")
//                        {
//                            Debug.Trace("OutputCacheModuleEnter",
//                                        "Skipping lookup because of Pragma: no-cache directive." +
//                                        "\nReturning from OutputCacheModule::Enter");

//                            RecordCacheMiss();
//                            return;
//                        }
//                    }
//                }
//            }
//            else if (settings.ValidationCallbackInfo != null)
//            {
//                /*
//                 * Check if the item is still valid.
//                 */
//                validationStatus = HttpValidationStatus.Valid;
//                validationStatusFinal = validationStatus;
//                for (i = 0, n = settings.ValidationCallbackInfo.Length; i < n; i++)
//                {
//                    callbackInfo = settings.ValidationCallbackInfo[i];
//                    try
//                    {
//                        callbackInfo.handler(context, callbackInfo.data, ref validationStatus);
//                    }
//                    catch (Exception e)
//                    {
//                        validationStatus = HttpValidationStatus.Invalid;
//                        HttpApplicationFactory.RaiseError(e);
//                    }

//                    switch (validationStatus)
//                    {
//                        case HttpValidationStatus.Invalid:
//                            Debug.Trace("OutputCacheModuleEnter", "Output cache item found but callback invalidated it." +
//                                        "\n\tkey=" + key +
//                                        "\nReturning from OutputCacheModule::Enter");

//                            cacheInternal.Remove(key);
//                            RecordCacheMiss();
//                            return;

//                        case HttpValidationStatus.IgnoreThisRequest:
//                            validationStatusFinal = HttpValidationStatus.IgnoreThisRequest;
//                            break;

//                        case HttpValidationStatus.Valid:
//                            break;

//                        default:
//                            Debug.Trace("OutputCacheModuleEnter", "Invalid validation status, ignoring it, status=" + validationStatus +
//                                        "\n\tkey=" + key);

//                            validationStatus = validationStatusFinal;
//                            break;
//                    }

//                }

//                if (validationStatusFinal == HttpValidationStatus.IgnoreThisRequest)
//                {
//                    Debug.Trace("OutputCacheModuleEnter", "Output cache item found but callback status is IgnoreThisRequest." +
//                                "\n\tkey=" + key +
//                                "\nReturning from OutputCacheModule::Enter");


//                    RecordCacheMiss();
//                    return;
//                }

//                Debug.Assert(validationStatusFinal == HttpValidationStatus.Valid,
//                             "validationStatusFinal == HttpValidationStatus.Valid");
//            }

//            rawResponse = cachedRawResponse._rawResponse;

//            // WOS 1985154 ensure Content-Encoding is acceptable
//            if (cachedVary == null || cachedVary._contentEncodings == null)
//            {
//                string acceptEncoding = request.Headers["Accept-Encoding"];
//                string contentEncoding = null;
//                ArrayList headers = rawResponse.Headers;
//                if (headers != null)
//                {
//                    foreach (HttpResponseHeader h in headers)
//                    {
//                        if (h.Name == "Content-Encoding")
//                        {
//                            contentEncoding = h.Value;
//                            break;
//                        }
//                    }
//                }
//                if (!IsAcceptableEncoding(contentEncoding, acceptEncoding))
//                {
//                    RecordCacheMiss();
//                    return;
//                }
//            }


//            /*
//             * Try to satisfy a conditional request. The cached response
//             * must satisfy all conditions that are present.
//             *
//             * We can only satisfy a conditional request if the response
//             * is buffered and has no substitution blocks.
//             *
//             * N.B. RFC 2616 says conditional requests only occur
//             * with the GET method, but we try to satisfy other
//             * verbs (HEAD, POST) as well.
//             */
//            send304 = -1;

//            if (!rawResponse.HasSubstBlocks)
//            {
//                /* Check "If-Modified-Since" header */
//                ifModifiedSinceHeader = request.IfModifiedSince;
//                if (ifModifiedSinceHeader != null)
//                {
//                    send304 = 0;
//                    try
//                    {
//                        utcIfModifiedSince = HttpDate.UtcParse(ifModifiedSinceHeader);
//                        if (settings.IsLastModifiedSet &&
//                                settings.UtcLastModified <= utcIfModifiedSince &&
//                                utcIfModifiedSince <= context.UtcTimestamp)
//                        {

//                            send304 = 1;
//                        }
//                    }
//                    catch
//                    {
//                        Debug.Trace("OutputCacheModuleEnter", "Ignore If-Modified-Since header, invalid format: " + ifModifiedSinceHeader);
//                    }
//                }

//                /* Check "If-None-Match" header */
//                if (send304 != 0)
//                {
//                    etag = request.IfNoneMatch;
//                    if (etag != null)
//                    {
//                        send304 = 0;
//                        etags = etag.Split(s_fieldSeparators);
//                        for (i = 0, n = etags.Length; i < n; i++)
//                        {
//                            if (i == 0 && etags[i].Equals(ASTERISK))
//                            {
//                                send304 = 1;
//                                break;
//                            }

//                            if (etags[i].Equals(settings.ETag))
//                            {
//                                send304 = 1;
//                                break;
//                            }
//                        }
//                    }
//                }
//            }

//            if (send304 == 1)
//            {
//                /*
//                 * Send 304 Not Modified
//                 */
//                Debug.Trace("OutputCacheModuleEnter", "Output cache hit & conditional request satisfied, status=304." +
//                            "\n\tkey=" + key +
//                            "\nReturning from OutputCacheModule::Enter");

//                response.ClearAll();
//                response.StatusCode = 304;
//            }
//            else
//            {
//                /*
//                 * Send the full response.
//                 */
//#if DBG
//                if (send304 == -1) {
//                    Debug.Trace("OutputCacheModuleEnter", "Output cache hit.\n\tkey=" + key +
//                                "\nReturning from OutputCacheModule::Enter");
  
//                }
//                else {
//                    Debug.Trace("OutputCacheModuleEnter", "Output cache hit but conditional request not satisfied.\n\tkey=" + key +
//                                "\nReturning from OutputCacheModule::Enter");
//                }
//#endif

//                sendBody = (request.HttpVerb != HttpVerb.HEAD);

//                // Check and see if the cachedRawResponse is from the disk
//                // If so, we must clone the HttpRawResponse before sending it

//                // UseSnapshot calls ClearAll
//                response.UseSnapshot(rawResponse, sendBody);
//            }

//            response.Cache.ResetFromHttpCachePolicySettings(settings, context.UtcTimestamp);

//            // re-insert entry in kernel cache if necessary
//            string originalCacheUrl = cachedRawResponse._kernelCacheUrl;
//            if (originalCacheUrl != null)
//            {
//                response.SetupKernelCaching(originalCacheUrl);
//            }

//            PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_RATIO_BASE);
//            PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_HITS);
//            _key = null;
//            _recordedCacheMiss = false;

//            app.CompleteRequest();
//        }


//        /*
//         * If the item is cacheable, add it to the cache.
//         */

//        /// <devdoc>
//        /// <para>Raises the <see langword="Leave"> event, which causes any cacheable items to
//        ///    be put into the output cache.</see></para>
//        /// </devdoc>
//        internal /*public*/ void OnLeave(Object source, EventArgs eventArgs)
//        {
//            HttpApplication app;
//            HttpContext context;
//            bool cacheable;
//            CachedVary cachedVary;
//            CachedRawResponse cachedRawResponse;
//            HttpCachePolicy cache;
//            HttpCachePolicySettings settings;
//            HttpRawResponse httpRawResponse;
//            string keyRawResponse;
//            string[] varyByContentEncodings;
//            string[] varyByHeaders;
//            string[] varyByParams;
//            bool varyByAllParams;
//            HttpRequest request;
//            HttpResponse response;
//            int i, n;
//            bool cacheAuthorizedPage;
//            CacheInternal cacheInternal;

//            Debug.Trace("OutputCacheModuleLeave", "Beginning OutputCacheModule::Leave");

//            app = (HttpApplication)source;
//            context = app.Context;
//            request = context.Request;
//            response = context.Response;
//            cache = null;

//#if DBG
//            string  reason = null;
//#endif
//            /*
//             * Determine whether the response is cacheable.
//             */
//            cacheable = false;
//            do
//            {
//                if (!response.HasCachePolicy)
//                {
//#if DBG
//                    reason = "CachePolicy not created, not modified from non-caching default.";
//#endif
//                    break;
//                }

//                cache = response.Cache;
//                if (!cache.IsModified())
//                {
//#if DBG
//                    reason = "CachePolicy created, but not modified from non-caching default.";
//#endif
//                    break;
//                }

//                if (response.StatusCode != 200)
//                {
//#if DBG
//                    reason = "response.StatusCode != 200.";
//#endif
//                    break;
//                }

//                if (request.HttpVerb != HttpVerb.GET && request.HttpVerb != HttpVerb.POST)
//                {
//#if DBG
//                    reason = "the cache can only cache responses to GET and POST.";
//#endif
//                    break;
//                }

//                if (!response.IsBuffered())
//                {
//#if DBG
//                    reason = "the response is not buffered.";
//#endif
//                    break;
//                }

//                /*
//                 * Change a response with HttpCacheability.Public to HttpCacheability.Private
//                 * if it requires authorization, and allow it to be cached.
//                 *
//                 * Note that setting Cacheability to ServerAndPrivate would accomplish
//                 * the same thing without needing the "cacheAuthorizedPage" variable,
//                 * but in RTM we did not have ServerAndPrivate, and setting that value
//                 * would change the behavior.
//                 */
//                cacheAuthorizedPage = false;
//                if (cache.GetCacheability() == HttpCacheability.Public &&
//                        context.RequestRequiresAuthorization())
//                {

//                    cache.SetCacheability(HttpCacheability.Private);
//                    cacheAuthorizedPage = true;
//                }

//                if (cache.GetCacheability() != HttpCacheability.Public &&
//                        cache.GetCacheability() != HttpCacheability.ServerAndPrivate &&
//                        cache.GetCacheability() != HttpCacheability.ServerAndNoCache &&
//                        !cacheAuthorizedPage)
//                {
//#if DBG
//                    reason = "CachePolicy.Cacheability is not Public, ServerAndPrivate, or ServerAndNoCache.";
//#endif
//                    break;
//                }

//                if (cache.GetNoServerCaching())
//                {
//#if DBG
//                    reason = "CachePolicy.NoServerCaching is set.";
//#endif
//                    break;
//                }

//                if (!cache.HasExpirationPolicy() && !cache.HasValidationPolicy())
//                {
//#if DBG
//                    reason = "CachePolicy has no expiration policy or validation policy.";
//#endif
//                    break;
//                }

//                if (cache.VaryByHeaders.GetVaryByUnspecifiedParameters())
//                {
//#if DBG
//                    reason = "CachePolicy.Vary.VaryByUnspecifiedParameters was called.";
//#endif
//                    break;
//                }

//                if (!cache.VaryByParams.AcceptsParams() && (request.HttpVerb == HttpVerb.POST || request.HasQueryString))
//                {
//#if DBG
//                    reason = "the cache cannot cache responses to POSTs or GETs with query strings unless Cache.VaryByParams is modified.";
//#endif
//                    break;
//                }

//                if (cache.VaryByContentEncodings.IsModified() && !cache.VaryByContentEncodings.IsCacheableEncoding(context.Response.GetHttpHeaderContentEncoding()))
//                {
//#if DBG
//                    reason = "the cache cannot cache encoded responses that are not listed in the VaryByContentEncodings collection.";
//#endif
//                    break;
//                }

//                cacheable = true;
//            } while (false);

//            /*
//             * Add response to cache.
//             */
//            if (!cacheable)
//            {
//#if DBG
//                Debug.Assert(reason != null, "reason != null");
//                Debug.Trace("OutputCacheModuleLeave", "Item is not output cacheable because " + reason +
//                            "\n\tUrl=" + request.Path +
//                            "\nReturning from OutputCacheModule::Leave");
//#endif

//                return;
//            }

//            RecordCacheMiss();

//            settings = cache.GetCurrentSettings(response);

//            varyByContentEncodings = settings.VaryByContentEncodings;

//            varyByHeaders = settings.VaryByHeaders;
//            if (settings.IgnoreParams)
//            {
//                varyByParams = null;
//            }
//            else
//            {
//                varyByParams = settings.VaryByParams;
//            }

//            cacheInternal = HttpRuntime.CacheInternal;

//            /* Create the key if it was not created in OnEnter */
//            if (_key == null)
//            {
//                _key = CreateOutputCachedItemKey(context, null);
//                Debug.Assert(_key != null, "_key != null");
//            }


//            if (varyByContentEncodings == null && varyByHeaders == null && varyByParams == null && settings.VaryByCustom == null)
//            {
//                /*
//                 * This is not a varyBy item.
//                 */
//                keyRawResponse = _key;
//                cachedVary = null;
//            }
//            else
//            {
//                /*
//                 * There is a vary in the cache policy. We handle this
//                 * by adding another item to the cache which contains
//                 * a list of the vary headers. A request for the item
//                 * without the vary headers in the key will return this
//                 * item. From the headers another key can be constructed
//                 * to lookup the item with the raw response.
//                 */
//                if (varyByHeaders != null)
//                {
//                    for (i = 0, n = varyByHeaders.Length; i < n; i++)
//                    {
//                        varyByHeaders[i] = "HTTP_" + CultureInfo.InvariantCulture.TextInfo.ToUpper(
//                                varyByHeaders[i].Replace('-', '_'));
//                    }
//                }

//                varyByAllParams = false;
//                if (varyByParams != null)
//                {
//                    varyByAllParams = (varyByParams.Length == 1 && varyByParams[0] == ASTERISK);
//                    if (varyByAllParams)
//                    {
//                        varyByParams = null;
//                    }
//                    else
//                    {
//                        for (i = 0, n = varyByParams.Length; i < n; i++)
//                        {
//                            varyByParams[i] = CultureInfo.InvariantCulture.TextInfo.ToLower(varyByParams[i]);
//                        }
//                    }
//                }

//                cachedVary = new CachedVary(varyByContentEncodings, varyByHeaders, varyByParams, varyByAllParams, settings.VaryByCustom);
//                keyRawResponse = CreateOutputCachedItemKey(context, cachedVary);
//                if (keyRawResponse == null)
//                {
//                    Debug.Trace("OutputCacheModuleLeave", "Couldn't add non-cacheable post.\n\tkey=" + _key);
//                    return;
//                }

//                // it is possible that the user code calculating custom vary-by
//                // string would Flush making the response non-cacheable. Check fo it here.
//                if (!response.IsBuffered())
//                {
//                    Debug.Trace("OutputCacheModuleLeave", "Response.Flush() inside GetVaryByCustomString\n\tkey=" + _key);
//                    return;
//                }
//            }

//            // Create the response object to be sent on cache hits.
//            httpRawResponse = response.GetSnapshot();
//            string kernelCacheUrl = response.SetupKernelCaching(null);
//            cachedRawResponse = new CachedRawResponse(httpRawResponse, settings, kernelCacheUrl);
//            InsertResponse(response, context, keyRawResponse, settings, cachedVary, cachedRawResponse);
//            _key = null;
//        }

//        internal bool InsertResponse(HttpResponse response,
//                                     HttpContext context,
//                                     string keyRawResponse,
//                                     HttpCachePolicySettings settings,
//                                     CachedVary cachedVary,
//                                     CachedRawResponse memoryRawResponse)
//        {
//            CacheInternal cacheInternal;
//            CachedVary cachedVaryInCache;
//            CacheDependency dependencyVary;
//            CacheDependency dependency;
//            DateTime utcExpires;
//            TimeSpan slidingDelta;
//            DateTime utcTimestamp;

//            cacheInternal = HttpRuntime.CacheInternal;

//            /* Determine the size of the sliding expiration.*/
//            if (settings.UtcTimestampCreated != DateTime.MinValue)
//            {
//                utcTimestamp = settings.UtcTimestampCreated;
//            }
//            else
//            {
//                utcTimestamp = context.UtcTimestamp;
//            }
//            utcExpires = DateTime.MaxValue;
//            if (settings.SlidingExpiration)
//            {
//                slidingDelta = settings.SlidingDelta;
//                utcExpires = Cache.NoAbsoluteExpiration;
//            }
//            else
//            {
//                slidingDelta = Cache.NoSlidingExpiration;
//                if (settings.IsMaxAgeSet)
//                {
//                    utcExpires = utcTimestamp + settings.MaxAge;
//                }
//                else if (settings.IsExpiresSet)
//                {
//                    utcExpires = settings.UtcExpires;
//                }
//            }

//            // Check and ensure that item hasn't expired:
//            if (utcExpires < DateTime.UtcNow)
//            {
//                return false;
//            }

//            if (cachedVary == null)
//            {
//                dependencyVary = null;
//            }
//            else
//            {
//                /*
//                 * Add the CachedVary item so that a request will know
//                 * which headers are needed to issue another request.
//                 *
//                 * Use the Add method so that we guarantee we only use
//                 * a single CachedVary and don't overwrite existing ones.
//                 */
//                cachedVaryInCache = (CachedVary)cacheInternal.UtcAdd(
//                        _key, cachedVary, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration,
//                        CacheItemPriority.Default, null);

//                if (cachedVaryInCache != null)
//                {
//                    if (cachedVary.Equals(cachedVaryInCache))
//                    {
//                        cachedVary = cachedVaryInCache;
//                    }
//                    else
//                    {
//                        Debug.Trace("OutputCacheModuleLeave", "CachedVary definition changed, new CachedVary inserted into cache.\n\tkey=" + _key);
//                        cacheInternal.UtcInsert(_key, cachedVary);
//                    }
//                }
//#if DBG
//                else {
//                    Debug.Trace("OutputCacheModuleLeave", "Added CachedVary to cache.\n\tkey=" + _key);
//                }
//#endif
//                dependencyVary = new CacheDependency(0, null, new string[1] { _key });

//            }

//            Debug.Trace("OutputCacheModuleLeave", "Adding response to cache.\n\tkey=" + keyRawResponse +
//                        "\nReturning from OutputCacheModule::Leave");

//            // Create the dependency
//            dependency = response.CreateCacheDependencyForResponse(dependencyVary);

//            // Increment the count before the insert so that another thread that
//            // is in OnEnter will not skip the lookup while there is an entry in the cache.
//            Interlocked.Increment(ref s_cEntries);

//            // Now insert into the cache.
//            try
//            {
//                cacheInternal.UtcInsert(
//                    keyRawResponse, memoryRawResponse, dependency, utcExpires,
//                    slidingDelta, CacheItemPriority.Normal, s_cacheItemRemovedCallback);

//                PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_ENTRIES);
//                PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_TURNOVER_RATE);
//            }
//            catch
//            {
//                Interlocked.Decrement(ref s_cEntries);
//                throw;
//            }

//            return true;
//        }
//    }
//}