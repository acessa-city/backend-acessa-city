using System;

namespace AcessaCity.Business.Models.Constants
{
    public static class StatusReport
    {
        public static Guid InAnalysis = Guid.Parse("48cf5f0f-40c9-4a79-9627-6fd22018f72c");
        public static Guid Denied = Guid.Parse("52ccae2e-af86-4fcc-82ea-9234088dbedf");
        public static Guid Approved = Guid.Parse("96afa0df-8ad9-4a44-a726-70582b7bd010");
        public static Guid InProgress = Guid.Parse("c37d9588-1875-44dd-8cf1-6781de7533c3");
        public static Guid Finished = Guid.Parse("ee6dda1a-51e2-4041-9d21-7f5c8f2e94b0");
    }
}