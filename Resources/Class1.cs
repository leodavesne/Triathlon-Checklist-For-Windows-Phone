using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriathlonChecklist.Resources
{
    public class InternationalizationHelper
    {
        public InternationalizationHelper()
        {
        }

        private static AppResources localizedResources = new AppResources();

        public AppResources AppResources
        {
            get { return localizedResources; }
        }
    }
}
