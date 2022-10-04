using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools_Build.OilInit {
    public struct OilSettings {
        public bool UseConstructorArguments;
        public bool UseObjectInitialization;
        public bool Minify;

        public static OilSettings GetSettings(bool useConstructorArguments = true, bool useObjectInitialization = true, bool minify = true) {
            return new OilSettings() {
                UseConstructorArguments=useConstructorArguments,
                UseObjectInitialization=useObjectInitialization,
                Minify=minify
            };
        }
    }
}
