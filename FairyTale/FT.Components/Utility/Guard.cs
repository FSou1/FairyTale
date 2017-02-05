using System;

namespace FT.Components.Utility {
    public static partial class Guard {
        public static void ArgumentNotNull(object argumentValue, string argumentName) {
            if (argumentValue == null) {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}