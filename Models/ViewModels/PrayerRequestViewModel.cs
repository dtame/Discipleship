using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WandaWebAdmin.Models.ViewModels
{
    public class PrayerRequestViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(From)
                    && !string.IsNullOrWhiteSpace(Subject)
                    && !string.IsNullOrWhiteSpace(Body);
            }
        }
    }
}
