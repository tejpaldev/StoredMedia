using System;

namespace StoreyedMedia.Model
{
    public class CommonBase
    {
        //set up standard null values for once

        public static DateTime DateTimeNullValue = DateTime.MinValue;
        public static Guid GuidNullValue = Guid.Empty;
        public static int IntNullValue = int.MinValue;
        public static int NonExistantOrdinal = -1;
        public static float FloatNullValue = float.MinValue;
        public static decimal DecimalNullValue = decimal.MinValue;
        public static string StringNullValue = null;
        public static string LoggedInUser="TestAdmin";
        public static int LoggedInUserId = 2;
        public static string LoggedInUser1 = "Robert Reader";
        public static int LoggedInUserId1 = 3;
        public static string LoggedInUser2 = "Vanessa Toll";
        public static int LoggedInUserId2 = 4;

    }

     
}
