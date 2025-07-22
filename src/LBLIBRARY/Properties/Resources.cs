namespace LBLIBRARY.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class Resources
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                resourceMan ??= new System.Resources.ResourceManager("LBLIBRARY.Properties.Resources", typeof(Resources).Assembly);
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => 
                resourceCulture;
            set => 
                resourceCulture = value;
        }

        internal static Bitmap Default =>
            (Bitmap) ResourceManager.GetObject("Default", resourceCulture);

        internal static Bitmap English =>
            (Bitmap) ResourceManager.GetObject("English", resourceCulture);

        internal static Bitmap Russian =>
            (Bitmap) ResourceManager.GetObject("Russian", resourceCulture);

        internal static Bitmap silver =>
            (Bitmap) ResourceManager.GetObject("silver", resourceCulture);
    }
}

