using Sd.Crm.Backend.Controllers.Responses;
using Sd.Crm.Backend.Model;
using Sd.Crm.Backend.Model.SquadModels;
using System.Globalization;

namespace Sd.Crm.Backend.Services.Google
{
    public static class SquadExtensions
    {
        public static SquadResponse ToSquad(this IList<IList<object>> values, TableMapping mapping)
        {
            var squad = new SquadResponse() { Id = Guid.NewGuid() };
            var trainingMap = CalculateTrainingMap(values, mapping);
            var status = DiscipleStatusEnum.Active;
            for (int i = mapping.DiscipleListStartsFrom; i < values.Count; i++)
            {
                if (IsStandByZoneStarted(values[i]))
                {
                    status = DiscipleStatusEnum.StandBy;
                    continue;
                }
                if (!IsDiscipleRow(values[i], mapping)) continue;
                var disciple = new Disciple()
                {
                    Id = Guid.NewGuid(),
                    FirstName = values[i][mapping.ColumnMapping["name"]].ToString()!.Split(' ')[0],
                    LastName = GetLastName(values[i][mapping.ColumnMapping["name"]].ToString()),
                    DateOfBirth = GetDate(values[i][mapping.ColumnMapping["dateOfBirth"]].ToString()),
                    Sex = values[i][mapping.ColumnMapping["sex"]].ToString(),
                    Status = status,
                    Level = new DiscipleLevel(values[i][mapping.ColumnMapping["level"]].ToString() ?? "unknown"),
                    FirstTrainingDate = values[i][mapping.ColumnMapping["firstTrainingDate"]].ToString(),
                };
                disciple.Mother = MapMother(values[i], mapping);
                disciple.Father = MapFather(values[i], mapping);
                disciple.Trainings = MapTraining(values[i], mapping, trainingMap);
                squad.Disciples.Add(disciple);
            }

            
            squad.TrainingDateInfo = trainingMap.Where(t => t.hasValue).ToList();
            return squad;
        }

        private static bool IsStandByZoneStarted(IList<object> row)
        {
            return row.FirstOrDefault() != null && row.First().ToString()!.StartsWith("Бу");
        }

        private static DateTime? GetDate(string? date)
        {
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return null;
        }

        private static TrainingDateInfo[] CalculateTrainingMap(IList<IList<object>> values, TableMapping mapping)
        {
            var row = values[mapping.DiscipleListStartsFrom - 2];
            var result = new TrainingDateInfo[row.Count];
            var number = 1;
            var month = 9;

            for (int i = mapping.TrainingsStartsFrom; i < row.Count; i++)
            {
                if (int.TryParse(row[i].ToString(), out int day))
                {
                    result[i] = new TrainingDateInfo() { hasValue = true, Number = number, Month = month, Day = day};
                    number++;
                }
                else
                {
                    result[i] = new TrainingDateInfo() { hasValue = false };
                    if (number > 1) month++;
                    if (month > 12) month = 1;
                    number = 1;                    
                }
            }
            return result;
        }

        private static List<Training> MapTraining(IList<object> row, TableMapping mapping, TrainingDateInfo[] trainingMap)
        {
            var result = new List<Training>();
            var count = Math.Min(row.Count, trainingMap.Length);
            for (int i = mapping.TrainingsStartsFrom; i < count; i++)
            {
                if (trainingMap[i].hasValue)
                {
                    var year = trainingMap[i].Month > 6 ? mapping.StartingYear : mapping.StartingYear + 1;

                    result.Add(new Training()
                    {
                        Id = Guid.NewGuid(),
                        Number = trainingMap[i].Number,
                        Month = trainingMap[i].Month,
                        Date = GetDate(string.Concat(trainingMap[i].Day, ".", trainingMap[i].Month, ".", year)) ?? FindDateFallback(result, mapping.StartingYear),
                        Presence = CalculatePresence(row[i])
                    });
                }
            }

            return result;
        }

        private static DateTime FindDateFallback(List<Training> result, int year)
        {
            if (result.Any())
            {
                return result.Last().Date + TimeSpan.FromDays(7);
            }
            var sunday = Enumerable.Range(1, 7).First(d => (new DateTime(year, 09, d)).DayOfWeek == 0);
            return new DateTime(2024, 09, sunday);
        }

        private static PresenceEnum CalculatePresence(object cell)
        {
            if (string.IsNullOrWhiteSpace(cell.ToString())) return PresenceEnum.NotMarked;
            int.TryParse(cell.ToString(), out int presence);
            return presence == 1 ? PresenceEnum.Present : PresenceEnum.Absent;
        }

        private static Father? MapFather(IList<object> row, TableMapping mapping)
        {
            if (string.IsNullOrWhiteSpace(row[mapping.ColumnMapping["fatherName"]]?.ToString())) return null;

            return new Father()
            {
                Id = Guid.NewGuid(),
                Name = row[mapping.ColumnMapping["fatherName"]].ToString()!,
                Phone = row[mapping.ColumnMapping["fatherPhone"]]?.ToString(),
                Comment = row[mapping.ColumnMapping["fatherComment"]]?.ToString()
            };
        }

        private static Mother? MapMother(IList<object> row, TableMapping mapping)
        {
            if (string.IsNullOrWhiteSpace(row[mapping.ColumnMapping["motherName"]]?.ToString())) return null;

            return new Mother()
            {
                Id = Guid.NewGuid(),
                Name = row[mapping.ColumnMapping["motherName"]].ToString()!,
                Phone = row[mapping.ColumnMapping["motherPhone"]]?.ToString(),
                Comment = row[mapping.ColumnMapping["motherComment"]]?.ToString()
            };
        }

        private static bool IsDiscipleRow(IList<object> row, TableMapping mapping)
        {
            if (row.Count < 2) return false;

            bool isStartFromNumber = int.TryParse(row[0].ToString(), out _);

            int nameIndex = mapping.ColumnMapping["name"];

            bool hasName = !string.IsNullOrWhiteSpace(row[nameIndex]?.ToString());

            return isStartFromNumber && hasName;
        }

        private static string? GetLastName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            name.Trim();

            if (!name.Contains(' ')) return null;

            return name.Split(' ')[1];
        }
    }

    public struct TrainingDateInfo
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Number { get; set; }
        public bool hasValue { get; set; }
    }
}
