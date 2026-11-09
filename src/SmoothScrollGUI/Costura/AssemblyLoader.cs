using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Costura;

[CompilerGenerated]
internal static class AssemblyLoader
{
    private static readonly object NullCacheLock = new object();
    private static readonly Dictionary<string, bool> NullCache = new Dictionary<string, bool>();
    private static readonly Dictionary<string, string> AssemblyNames = new Dictionary<string, string>();
    private static readonly Dictionary<string, string> SymbolNames = new Dictionary<string, string>();
    private static int _isAttached;

    static AssemblyLoader()
    {
        AssemblyNames.Add("microsoft.threading.tasks", "costura.microsoft.threading.tasks.dll.compressed");
        AssemblyNames.Add("microsoft.threading.tasks.extensions.desktop", "costura.microsoft.threading.tasks.extensions.desktop.dll.compressed");
        AssemblyNames.Add("microsoft.threading.tasks.extensions", "costura.microsoft.threading.tasks.extensions.dll.compressed");
        AssemblyNames.Add("newtonsoft.json", "costura.newtonsoft.json.dll.compressed");
        SymbolNames.Add("newtonsoft.json", "costura.newtonsoft.json.pdb.compressed");
        AssemblyNames.Add("winformanimation", "costura.winformanimation.dll.compressed");
    }

    public static void Attach()
    {
        if (Interlocked.Exchange(ref _isAttached, 1) == 1)
        {
            return;
        }

        AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
    }

    public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
    {
        lock (NullCacheLock)
        {
            if (NullCache.ContainsKey(e.Name))
            {
                return null;
            }
        }

        var assemblyName = new AssemblyName(e.Name);
        Assembly assembly = ReadExistingAssembly(assemblyName);

        if (assembly != null)
        {
            return assembly;
        }

        assembly = ReadFromEmbeddedResources(AssemblyNames, SymbolNames, assemblyName);

        if (assembly == null)
        {
            lock (NullCacheLock)
            {
                NullCache[e.Name] = true;
            }

            if (assemblyName.Flags == AssemblyNameFlags.Retargetable)
            {
                assembly = Assembly.Load(assemblyName);
            }
        }

        return assembly;
    }

    private static string CultureToString(CultureInfo culture)
    {
        return culture == null ? string.Empty : culture.Name;
    }

    private static Assembly ReadExistingAssembly(AssemblyName name)
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            AssemblyName candidate = assembly.GetName();

            if (string.Equals(candidate.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(CultureToString(candidate.CultureInfo), CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
            {
                return assembly;
            }
        }

        return null;
    }

    private static void CopyTo(Stream source, Stream destination)
    {
        var buffer = new byte[81920];
        int read;

        while ((read = source.Read(buffer, 0, buffer.Length)) != 0)
        {
            destination.Write(buffer, 0, read);
        }
    }

    private static Stream LoadStream(string fullName)
    {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();

        if (fullName.EndsWith(".compressed"))
        {
            using (Stream stream = executingAssembly.GetManifestResourceStream(fullName))
            {
                using (var source = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    var destination = new MemoryStream();
                    CopyTo(source, destination);
                    destination.Position = 0;
                    return destination;
                }
            }
        }

        return executingAssembly.GetManifestResourceStream(fullName);
    }

    private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
    {
        return resourceNames.TryGetValue(name, out string value)
            ? LoadStream(value)
            : null;
    }

    private static byte[] ReadStream(Stream stream)
    {
        var buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        return buffer;
    }

    private static Assembly ReadFromEmbeddedResources(
        Dictionary<string, string> assemblyNames,
        Dictionary<string, string> symbolNames,
        AssemblyName requestedAssemblyName)
    {
        string resourceName = requestedAssemblyName.Name.ToLowerInvariant();

        if (requestedAssemblyName.CultureInfo != null &&
            !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
        {
            resourceName = $"{requestedAssemblyName.CultureInfo.Name}.{resourceName}";
        }

        byte[] rawAssembly;

        using (Stream stream = LoadStream(assemblyNames, resourceName))
        {
            if (stream == null)
            {
                return null;
            }

            rawAssembly = ReadStream(stream);
        }

        using (Stream symbolStream = LoadStream(symbolNames, resourceName))
        {
            if (symbolStream != null)
            {
                byte[] rawSymbolStore = ReadStream(symbolStream);
                return Assembly.Load(rawAssembly, rawSymbolStore);
            }
        }

        return Assembly.Load(rawAssembly);
    }
}
