using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Media.Media3D;
using AttendanceTracking.DB;

namespace AttendanceTracking.ClassHelper
{
    public class EFClass
    {
        public static AttendanceTrackingEntities1 context { get; } = new AttendanceTrackingEntities1();
   
        public static bool Change = false;
        public static int IdChange;
        public static int IdAuth;
    }
}
