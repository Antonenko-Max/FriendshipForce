using System.Globalization;
using System.Text.RegularExpressions;

namespace Sd.Crm.Backend.Services.Google
{
    public static class LeadExtensions
    {
        public static IEnumerable<Model.LeadModels.Lead> ToLeadCollection(this IList<IList<object>> list, Dictionary<string, int> leadMapping)
        {
            var result = new List<Model.LeadModels.Lead>();

            var year = DateTime.Now.Year;
            var regex = new Regex("^[0-9]{1,2}\\.[0-9]{1,2}$");
            DateTime? today = null;

            foreach (var row in list)
            {
                if (row.Any() && regex.IsMatch(row.First().ToString()))
                {
                    today = DateTime.ParseExact(string.Concat(row.First(), '.', year), "dd.MM.yyyy", null, DateTimeStyles.None);
                    continue;
                }

                if (today == null) continue;

                if (row.Count > 6)
                {
                    var lead = new Model.LeadModels.Lead(Guid.NewGuid());
                    lead.MotherName = row[leadMapping["name"]].ToString() ?? "no name";
                    lead.Phone = row[leadMapping["phone"]].ToString() ?? "no phone";
                    lead.Region = new Model.Region() { Id = Guid.NewGuid(), Name = row[leadMapping["region"]].ToString() };
                    lead.City = new Model.City() { Id = Guid.NewGuid(), Name = row[leadMapping["city"]].ToString() };
                    lead.ChildAge = int.TryParse(row[leadMapping["childAge"]].ToString(), out int childAge) ? childAge : null;
                    lead.Date = (DateTime)today;

                    result.Add(lead);
                }
            }

            return result;
        }
    }
}
