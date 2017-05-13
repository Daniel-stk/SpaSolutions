using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InformationModels
{
    public class Workspace
    {
        public int WorkspaceId { get; set; }
        public int WorkspaceName { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
