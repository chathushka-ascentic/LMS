using System;
using System.Collections.Generic;
using System.Linq;

namespace Plug.Models
{
    public class EnrollmentModel : ModelBase
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public CourseModel CourseModel { get; set; }

        public Guid StudentId { get; set; }

        public int EnrollModulesCount { get; set; }

        public int CompletedEnrollModulesCount { get; set; }

        public virtual List<EnrollModuleModel> EnrollModules { get; set; }

        public string LastSeen
        {
            get
            {
                if (this.EnrollModules == null ||
                    !this.EnrollModules.Any())
                {
                    return string.Empty;
                }

                var lastseen = DateTime.MinValue;

                this.EnrollModules.ForEach(e =>
                {
                    if (lastseen < e.LastVisited)
                    {
                        lastseen = e.LastVisited;
                    }
                });

                if (lastseen == DateTime.MinValue)
                {
                    return string.Empty;
                }

                return lastseen.ToString("g");
            }
        }

        public EnrollModuleModel LastActiveModule
        {
            get
            {
                if (this.EnrollModules == null ||
                       !this.EnrollModules.Any())
                {
                    return null;
                }

                var lastseen = DateTime.MinValue;
                var lastActive = new EnrollModuleModel();

                this.EnrollModules.ForEach(e =>
                {
                    if (lastseen < e.LastVisited)
                    {
                        lastseen = e.LastVisited;
                        lastActive = e;
                    }
                });

                if (lastseen == DateTime.MinValue)
                {
                    return null;
                }

                return lastActive;
            }
        }
    }
}
