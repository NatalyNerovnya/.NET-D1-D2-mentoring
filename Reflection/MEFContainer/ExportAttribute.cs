using System;

namespace MEFContainer
{
    public class ExportAttribute : Attribute
    {
        public Type  ExportType{ get; set; }

        public ExportAttribute()
        {
        }

        public ExportAttribute (Type type)
        {
            ExportType = type;
        }
    }
}
