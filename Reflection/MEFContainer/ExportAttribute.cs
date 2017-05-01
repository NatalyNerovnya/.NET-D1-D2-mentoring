using System;

namespace MEFContainer
{
    public class ExportAttribute : Attribute
    {
        Type  ExportType{ get; set; }

        public ExportAttribute()
        {
        }

        public ExportAttribute (Type type)
        {
            ExportType = type;
        }
    }
}
