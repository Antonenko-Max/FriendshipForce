using Sd.Crm.Backend.Model.SquadModels;
using Sd.Crm.Backend.Services.Google;

namespace Sd.Crm.Backend.Controllers.Responses
{
    public class SquadResponse : Squad
    {
        public List<TrainingDateInfo> TrainingDateInfo { get; set; }
    }
}
